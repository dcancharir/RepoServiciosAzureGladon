using Quartz;
using ServicioServidorVPN.clases.Ludopata;
using ServicioServidorVPN.clases.Mincetur;
using ServicioServidorVPN.DAL.Ludopatas;
using ServicioServidorVPN.Service.Mincetur;
using ServicioServidorVPN.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ServicioServidorVPN.Jobs.Ludopatas {
    public class SincronizacionLudopatasMinceturJob : IJob {
        private readonly LudopataDAL ludopataDAL;
        private readonly MinceturService minceturService;
        private readonly string tokenMincetur;

        public SincronizacionLudopatasMinceturJob() {
            ludopataDAL = new LudopataDAL();
            minceturService = new MinceturService();
            tokenMincetur = ConfigurationManager.AppSettings["TokenLudopatasMincetur"];
        }

        public async Task Execute(IJobExecutionContext context) {
            if(string.IsNullOrWhiteSpace(tokenMincetur)) {
                funciones.logueo("No hay credenciales para la búsqueda de ludópatas en MINCETUR.");
                return;
            }

            ResponseMinceturLudopata responseMincetur = await minceturService.ObtenerLudopataAzure(tokenMincetur);
            //ResponseMinceturLudopata responseMincetur = await minceturService.ObtenerLudopataMincetur(tokenMincetur);
            //ResponseMinceturLudopata responseMincetur = minceturService.ObtenerLudopataMinceturLocal();

            if(!responseMincetur.TieneLudopatas()) {
                funciones.logueo("No hay ludópatas en el padrón proporcionado por la API de MINCETUR");
                return;
            }

            List<Ludopata> ludopatasLocal = ludopataDAL.ObtenerLudopatas();

            List<Ludopata> ludopataCrearContactoModificar = new List<Ludopata>();
            List<Ludopata> ludopatasInsertar = new List<Ludopata>();
            List<Ludopata> ludopatasEnvioCorreo = new List<Ludopata>();
            List<Ludopata> ludopatasModificar = new List<Ludopata>();
            List<Ludopata> ludopatasActivar = new List<Ludopata>();
            List<Ludopata> ludopatasDesactivar = new List<Ludopata>();
            List<Ludopata> ludopatasSinAccion = new List<Ludopata>();

            DateTime fechaActual = DateTime.Now;

            var ludopatasCombinados = ludopatasLocal
                .Select(x => new {
                    tipoDocumentoId = x.TipoDoiID,
                    numeroDocumento = x.DNI
                })
                .Union(responseMincetur.Ludopatas.Select(x => new {
                    tipoDocumentoId = x.ObtenerTipoDocumentoId(),
                    numeroDocumento = x.NumeroDocumento
                }))
                .GroupBy(x => new { x.tipoDocumentoId, x.numeroDocumento })
                .Select(x => x.First())
                .ToList();

            foreach(var item in ludopatasCombinados) {
                Ludopata ludopataLocalBuscado = ludopatasLocal.FirstOrDefault(x => x.TipoDoiID == item.tipoDocumentoId && x.DNI.Equals(item.numeroDocumento)) ?? new Ludopata();
                Ludopata ludopataLocalBuscadoSoloDni = ludopatasLocal.FirstOrDefault(x => x.DNI.Equals(item.numeroDocumento)) ?? new Ludopata();
                LudopataMincetur ludopataMinceturBuscado = responseMincetur.Ludopatas.FirstOrDefault(x => x.IdTipoDocumento == item.tipoDocumentoId && x.NumeroDocumento.Equals(item.numeroDocumento)) ?? new LudopataMincetur();
                Ludopata ludopataCreado = CrearLudopata(ludopataMinceturBuscado, ludopataLocalBuscado, fechaActual);
                if(ludopataMinceturBuscado.Existe()) {
                    if(ludopataLocalBuscado.Existe()) {
                        //verifico existencia de contacto de ludopata
                        if(!ludopataLocalBuscado.TieneContacto()) {
                            ludopataCrearContactoModificar.Add(ludopataCreado);
                            continue;
                        }
                        //comparo valores de todos los campos
                        bool sonIguales = CompararLudopataConMincetur(ludopataLocalBuscado, ludopataMinceturBuscado);
                        if(!sonIguales) {
                            //actualizo el registro del ludopata mas su contacto
                            ludopatasModificar.Add(ludopataCreado);
                            continue;
                        }
                        if(!ludopataLocalBuscado.EstaActivo()) {
                            //cambio el estado a 1
                            ludopatasActivar.Add(ludopataCreado);
                            continue;
                        }
                    } else {
                        //verifico si existe algun deno con solo dni que tenga la imegn y foto
                        if(ludopataLocalBuscadoSoloDni.Existe()) {
                            ludopataCreado.Imagen = ludopataLocalBuscadoSoloDni.Imagen;
                            ludopataCreado.Foto = ludopataLocalBuscadoSoloDni.Foto;
                            ludopataCreado.Telefono = ludopataLocalBuscadoSoloDni.Telefono;
                            ludopataCreado.CodUbigeo = ludopataLocalBuscadoSoloDni.CodUbigeo;
                        }
                        //inserto en local
                        ludopatasInsertar.Add(ludopataCreado);
                        continue;
                    }
                } else {
                    if(ludopataLocalBuscado.Existe() && ludopataLocalBuscado.EstaActivo()) {
                        //cambio el estado a 0
                        ludopatasDesactivar.Add(ludopataCreado);
                        continue;
                    }
                }
                ludopatasSinAccion.Add(ludopataCreado);
            }

            Console.WriteLine("Insertando ...");
            int insertadosCorrectamente = 0;
            foreach(Ludopata ludopata in ludopatasInsertar) {
                int idInsertado = ludopataDAL.InsertarLudopataConContacto(ludopata);
                insertadosCorrectamente += idInsertado > 0 ? 1 : 0;
            }

            Console.WriteLine("CrearContacto - Modificando ...");
            int contactoCreadoModificadoCorrectamente = 0;
            foreach(Ludopata ludopata in ludopataCrearContactoModificar) {
                int idInsertadoContactoLudopata = ludopataDAL.InsertarContactoLudopata(ludopata.Contacto);
                if(idInsertadoContactoLudopata > 0) {
                    ludopata.ContactoID = idInsertadoContactoLudopata;
                    ludopata.Contacto.ContactoID = idInsertadoContactoLudopata;
                    int idModificado = ludopataDAL.ModificarLudopataContacto(ludopata);
                    contactoCreadoModificadoCorrectamente += idModificado > 0 ? 1 : 0;
                }
            }

            Console.WriteLine("Modificando ...");
            int modificadosCorrectamente = 0;
            foreach(Ludopata ludopata in ludopatasModificar) {
                int idModificado = ludopataDAL.ModificarLudopataContacto(ludopata);
                modificadosCorrectamente += idModificado > 0 ? 1 : 0;
            }

            Console.WriteLine("Activando ...");
            int activadosCorrectamente = 0;
            foreach(Ludopata ludopata in ludopatasActivar) {
                int idModificado = ludopataDAL.ModificarEstadoLudopata(true, ludopata.LudopataID);
                activadosCorrectamente += idModificado > 0 ? 1 : 0;
            }

            Console.WriteLine("Desactivando ...");
            int desactivadosCorrectamente = 0;
            foreach(Ludopata ludopata in ludopatasDesactivar) {
                int idModificado = ludopataDAL.ModificarEstadoLudopata(false, ludopata.LudopataID);
                desactivadosCorrectamente += idModificado > 0 ? 1 : 0;
            }

            funciones.logueo($@"
                Ludópatas Crear Contacto: {contactoCreadoModificadoCorrectamente}/{ludopataCrearContactoModificar.Count}
                Ludópatas Insertados: {insertadosCorrectamente}/{ludopatasInsertar.Count}
                Correos Enviados de los insertados: {ludopatasEnvioCorreo.Count}
                Ludópatas Modificados: {modificadosCorrectamente}/{ludopatasModificar.Count}
                Ludópatas Activados: {activadosCorrectamente}/{ludopatasActivar.Count}
                Ludópatas Desactivados: {desactivadosCorrectamente}/{ludopatasDesactivar.Count}
                Ludópatas sin Acción: {ludopatasSinAccion.Count}
            ");
        }

        public Ludopata CrearLudopata(LudopataMincetur ludopataMincetur, Ludopata ludopataLocal, DateTime fechaActual) {
            return new Ludopata {
                LudopataID = ludopataLocal.LudopataID,
                Nombre = ludopataMincetur.Nombres,
                ApellidoPaterno = ludopataMincetur.ApellidoPaterno,
                ApellidoMaterno = ludopataMincetur.ApellidoMaterno,
                FechaInscripcion = !string.IsNullOrEmpty(ludopataMincetur.FechaInscripcion) ? DateTime.ParseExact(ludopataMincetur.FechaInscripcion, "yyyyMMdd", null) : ludopataLocal.FechaInscripcion.Equals(new DateTime(1, 1, 1)) ? fechaActual : ludopataLocal.FechaInscripcion,
                TipoExclusion = ludopataLocal.TipoExclusion == 0 ? 1 : ludopataLocal.TipoExclusion,
                DNI = ludopataMincetur.NumeroDocumento ?? string.Empty,
                Foto = ludopataLocal.Foto,
                Telefono = ludopataLocal.Telefono,
                CodRegistro = ludopataMincetur.CodigoRegistro ?? string.Empty,
                Estado = true,
                Imagen = ludopataLocal.Imagen,
                TipoDoiID = ludopataMincetur.IdTipoDocumento,
                CodUbigeo = ludopataLocal.CodUbigeo,
                ContactoID = ludopataLocal.ContactoID,
                FechaRegistro = fechaActual,
                UsuarioRegistro = 0,
                Contacto = new ContactoLudopata {
                    ContactoID = ludopataLocal.ContactoID,
                    Nombre = ludopataMincetur.NombresContacto,
                    ApellidoPaterno = ludopataMincetur.ApellidoPaternoContacto,
                    ApellidoMaterno = ludopataMincetur.ApellidoMaternoContacto,
                    Telefono = ludopataMincetur.TelefonoContacto,
                    Celular = ludopataMincetur.CelularContacto,
                }
            };
        }

        public bool CompararLudopataConMincetur(Ludopata ludopata, LudopataMincetur ludopataMincetur) {
            // Comparación de campos individuales ignorando mayúsculas y minúsculas
            if(!ludopata.CodRegistro.Equals(ludopataMincetur.CodigoRegistro, StringComparison.OrdinalIgnoreCase))
                return false;
            if(!ludopata.Nombre.Equals(ludopataMincetur.Nombres, StringComparison.OrdinalIgnoreCase))
                return false;
            if(!ludopata.ApellidoPaterno.Equals(ludopataMincetur.ApellidoPaterno, StringComparison.OrdinalIgnoreCase))
                return false;
            if(!ludopata.ApellidoMaterno.Equals(ludopataMincetur.ApellidoMaterno, StringComparison.OrdinalIgnoreCase))
                return false;
            if(ludopata.TipoDoiID != ludopataMincetur.IdTipoDocumento)
                return false;
            if(!ludopata.DNI.Equals(ludopataMincetur.NumeroDocumento, StringComparison.OrdinalIgnoreCase))
                return false;
            if(!string.IsNullOrEmpty(ludopataMincetur.FechaInscripcion) && !ludopata.FechaInscripcion.ToString("yyyyMMdd").Equals(ludopataMincetur.FechaInscripcion, StringComparison.OrdinalIgnoreCase))
                return false;

            // Comparación de contacto, ignorando mayúsculas y minúsculas
            if(!ludopata.Contacto.Nombre.Equals(ludopataMincetur.NombresContacto, StringComparison.OrdinalIgnoreCase))
                return false;
            if(!ludopata.Contacto.ApellidoPaterno.Equals(ludopataMincetur.ApellidoPaternoContacto, StringComparison.OrdinalIgnoreCase))
                return false;
            if(!ludopata.Contacto.ApellidoMaterno.Equals(ludopataMincetur.ApellidoMaternoContacto, StringComparison.OrdinalIgnoreCase))
                return false;
            if(!ludopata.Contacto.Telefono.Equals(ludopataMincetur.TelefonoContacto, StringComparison.OrdinalIgnoreCase))
                return false;
            if(!ludopata.Contacto.Celular.Equals(ludopataMincetur.CelularContacto, StringComparison.OrdinalIgnoreCase))
                return false;

            // Si todos los campos son iguales, retorna true
            return true;
        }
    }
}
