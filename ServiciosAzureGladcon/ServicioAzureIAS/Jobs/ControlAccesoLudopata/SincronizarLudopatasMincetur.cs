using Quartz;
using ServicioAzureIAS.Clases.Email;
using ServicioAzureIAS.Clases.Ludopata;
using ServicioAzureIAS.Clases.Mincetur;
using ServicioAzureIAS.ControlAcceso.CAL;
using ServicioAzureIAS.DAL.ControlAcceso;
using ServicioAzureIAS.DAL.Email;
using ServicioAzureIAS.DAL.Mincetur;
using ServicioAzureIAS.Service;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Jobs.ControlAccesoLudopata {
    public class SincronizarLudopatasMincetur : IJob {
        private readonly LudopataDAL ludopataDAL;
        private readonly HistorialLudopataDAL historialLudopataDAL;
        private readonly MinceturDAL minceturDAL;
        private readonly DestinatarioDAL destinatarioDAL;
        private readonly MinceturService minceturService;
        private readonly EmailService emailService;

        public SincronizarLudopatasMincetur() {
            ludopataDAL = new LudopataDAL();
            minceturDAL = new MinceturDAL();
            destinatarioDAL = new DestinatarioDAL();
            minceturService = new MinceturService();
            emailService = new EmailService();
            historialLudopataDAL = new HistorialLudopataDAL();
        }

        public async Task Execute(IJobExecutionContext context) {
            List<CredencialMincetur> credenciales = minceturDAL.ObtenerCredencialesMinceturActivas();
            if(credenciales.Count == 0) {
                funciones.logueo("No hay credenciales para la búsqueda de ludópatas en MINCETUR.");
                return;
            }

            ResponseMinceturLudopata responseMincetur = new ResponseMinceturLudopata();

            foreach(CredencialMincetur credencial in credenciales) {
                ResponseMinceturLudopata responseMinceturAux = await minceturService.ObtenerLudopataMincetur(credencial.token);
                //ResponseMinceturLudopata responseMinceturAux = minceturService.ObtenerLudopataMinceturLocal();
                if(responseMinceturAux.TieneLudopatas()) {
                    responseMincetur = responseMinceturAux;
                }
            }

            if(!responseMincetur.TieneLudopatas()) {
                funciones.logueo("No hay ludopatas en el padrón proporcionado por la API de MINCETUR");
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
            List<Destinatario> listaDestinatarios = destinatarioDAL.ObtenerDestinatariosPorTipo(3).Where(x => x.Estado).ToList();
            string destinatarios = string.Join(",", listaDestinatarios.Select(x => x.Email));
            foreach(Ludopata ludopata in ludopatasInsertar) {
                int idInsertado = ludopataDAL.InsertarLudopataConContacto(ludopata);
                insertadosCorrectamente += idInsertado > 0 ? 1 : 0;
                if(idInsertado > 0) {
                    //verifico si esta en ast_cliente
                    bool existe = ludopataDAL.ExisteLudopataEnClientes(ludopata.TipoDoiID, ludopata.DNI);
                    if(existe) {
                        ludopataDAL.ModificarFechaEnvioCorreo(idInsertado, fechaActual);
                        ludopatasEnvioCorreo.Add(ludopata);
                    }
                    HistorialLudopata historialLudopata = new HistorialLudopata {
                        IdLudopata = idInsertado,
                        TipoMovimiento = TipomovimientoHistorialLudopata.Entra,
                        TipoRegistro = TipoRegistroHistorialLudopata.Automatico
                    };
                    historialLudopataDAL.InsertarHistorialLudopata(historialLudopata);
                }
            }
            await emailService.SendEmailAsync(destinatarios, "Servicio de Sincronización de Ludopatas - IAS - MINCETUR", CrearBodyParaCorreoLudopata(ludopatasEnvioCorreo, fechaActual), true);

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
                bool seModifico = idModificado > 0;
                activadosCorrectamente += seModifico ? 1 : 0;
                if(seModifico) {
                    HistorialLudopata historialLudopata = new HistorialLudopata {
                        IdLudopata = idModificado,
                        TipoMovimiento = TipomovimientoHistorialLudopata.Entra,
                        TipoRegistro = TipoRegistroHistorialLudopata.Automatico
                    };
                    historialLudopataDAL.InsertarHistorialLudopata(historialLudopata);
                }
            }

            Console.WriteLine("Desactivando ...");
            int desactivadosCorrectamente = 0;
            foreach(Ludopata ludopata in ludopatasDesactivar) {
                int idModificado = ludopataDAL.ModificarEstadoLudopata(false, ludopata.LudopataID);
                bool seModifico = idModificado > 0;
                desactivadosCorrectamente += seModifico ? 1 : 0;
                if(seModifico) {
                    HistorialLudopata historialLudopata = new HistorialLudopata {
                        IdLudopata = idModificado,
                        TipoMovimiento = TipomovimientoHistorialLudopata.Sale,
                        TipoRegistro = TipoRegistroHistorialLudopata.Automatico
                    };
                    historialLudopataDAL.InsertarHistorialLudopata(historialLudopata);
                }
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

        public string CrearBodyParaCorreoLudopata(List<Ludopata> ludopatas, DateTime fecha) {
            StringBuilder rows = new StringBuilder();
            int cantidadColumnas = 3;
            if(ludopatas.Count > 0) {
                int i = 1;
                foreach(Ludopata ludopata in ludopatas) {
                    rows.Append(ludopata.CrearFilaParaTabla(i));
                    i++;
                }
            } else {
                rows.Append("<tr>");
                rows.Append($"<tr><td colspan='{cantidadColumnas}' style='border: 1px solid black;text-align: center;font-family: Helvetica, Arial, sans-serif'>Sin nuevos registros</td>");
                rows.Append("</tr>");
            }

            return $@"
                  <div style='background: rgb(250,251,63); background-image: linear-gradient(to top, #0c2c5c, #053a84, #0f48ac, #2955d6, #4960ff);width: 100%;padding:25px;'>
                    <table style='border-radius:5px; display: table;margin:0 auto; background:#fff;padding:20px;'>
                        <tbody style='width:100%'>
                            <tr>
                                <td colspan='{cantidadColumnas}'>
                                    <div style='border-radius:5px;text-align: center;font-family: Helvetica, Arial, sans-serif;  color: #fff; width:100%;background:#0C2C5C;padding:5px;'>
                                        <h1>Sincronización de Ludopatas</h1>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='{cantidadColumnas}'>
                                    <div style='font-family: Helvetica, Arial, sans-serif;color: #000000;'>
                                        <h3 style='font-weight: lighter;'>Buen día, se ha sincronizado el padrón de ludópatas de MINCETUR y se han encontrado las siguientes personas como clientes.</h3>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th style='background-color: #ccc; font-weight: bold; text-align: center; border: 1px solid black; padding: 5px;'>N°</th>
                                <th style='background-color: #ccc; font-weight: bold; text-align: center; border: 1px solid black; padding: 5px;'>Nro. Documento</th>
                                <th style='background-color: #ccc; font-weight: bold; text-align: center; border: 1px solid black; padding: 5px;'>Nombre</th>
                            </tr>
                            {rows}
                            <tr>
                                <td colspan='{cantidadColumnas}'>
                                    <div style='height:20px;'></div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='{cantidadColumnas}'>
                                    <div style='text-align: right;font-family: Helvetica, Arial, sans-serif;color: #000000;'>
                                        <h3>Cantidad: {ludopatas.Count}</h3>
                                    </div>
                                    <div style='text-align: right;font-family: Helvetica, Arial, sans-serif;color: #000000;'>
                                        <h3>Fecha: {fecha.ToString("dd/MM/yyyy hh:mm tt")}</h3>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='{cantidadColumnas}'>
                                    <div style='text-align: left;font-family: Helvetica, Arial, sans-serif;color: #000000;'>
                                        <small>* Correo enviado automaticamente desde el servicio de sincronización de ludópatas del padrón de MINCETUR.</small>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            ";
        }
    }
}
