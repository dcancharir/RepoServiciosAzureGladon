using ServicioServidorVPN.clases;
using ServicioServidorVPN.DAL;
using ServicioServidorVPN.utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServicioServidorVPN
{
    public class ServicioController : ApiController
    {
        private readonly CMP_SesionDAL _sesionDAL = new CMP_SesionDAL();
        private readonly CMP_SesionSorteoSalaDAL _sesionSorteoSalaDAL = new CMP_SesionSorteoSalaDAL();
        private readonly CMP_JugadaDAL _jugadaDAL = new CMP_JugadaDAL();
        private readonly CMP_SorteoSalaDAL _sorteoSalaDAL = new CMP_SorteoSalaDAL();
        private readonly CMP_SesionClienteDAL _sesionClienteDAL=new CMP_SesionClienteDAL();

        //Excalibur 
        private readonly CMP_SesionExcaDAL _sesionExcaDAL = new CMP_SesionExcaDAL();
        private readonly CMP_SesionSorteoSalaExcaDAL _sesionSorteoSalaExcaDAL = new CMP_SesionSorteoSalaExcaDAL();
        private readonly CMP_JugadaExcaDAL _jugadaExcaDAL = new CMP_JugadaExcaDAL();
        private readonly CMP_SorteoSalaExcaDAL _sorteoSalaExcaDAL = new CMP_SorteoSalaExcaDAL();
        private readonly CMP_SesionClienteExcaDAL _sesionClienteExcaDAL = new CMP_SesionClienteExcaDAL();

        private readonly SalaDAL _salaDAL=new SalaDAL();
        [HttpPost]
        public IHttpActionResult DevolverDatos()
        {
            return Ok("datos");
        }
        [HttpPost]
        public IHttpActionResult RecepcionarDataMigracion(dynamic jsonData)
        {


            bool recepcionado = true;
            List<CMP_Sesion> listaSesiones = new List<CMP_Sesion>();
            List<CMP_SesionSorteoSala> listaDetalles = new List<CMP_SesionSorteoSala>();
            List<CMP_SorteoSala> listaSorteos = new List<CMP_SorteoSala>();
            int CodSala = 0;
            string NombreSala = string.Empty;
            int sesionesRegistradas = 0;
            try
            {
                dynamic items = jsonData;
                if (items.codSala != null)
                {
                    CodSala = items.codSala.ToObject<int>();
                }
                if (items.sesiones != null)
                {
                    listaSesiones= items.sesiones.ToObject<List<CMP_Sesion>>();
                }
                
                if (items.detalles != null)
                {
                    listaDetalles = items.detalles.ToObject<List<CMP_SesionSorteoSala>>();
                }
                if (items.nombreSala != null)
                {
                    NombreSala = items.nombreSala.ToObject<string>();
                }
                if (items.sorteosActivos != null)
                {
                    listaSorteos = items.sorteosActivos.ToObject<List<CMP_SorteoSala>>();
                }
                foreach (var item in listaSesiones)
                {
                    item.CodSala = CodSala;
                    int registrado = _sesionDAL.GuardarSesion(item);
                    if (registrado > 0)
                    {
                        sesionesRegistradas++;
                        var detallesSesion = listaDetalles.Where(x => x.SesionId == item.SesionId).ToList();
                        foreach (var det in detallesSesion)
                        {
                            det.CodSala = CodSala;
                            int detalleRegistrado = _sesionSorteoSalaDAL.GuardarSesionSorteSala(det);

                            var jugada = det.Jugada;
                            jugada.JugadaId = det.JugadaId;
                            jugada.CodSala = CodSala;
                            int jugadaRegistrada = _jugadaDAL.GuardarJugada(jugada);
                        }
                    }

                }
                foreach(var item in listaSorteos)
                {
                    item.CodSala = CodSala;
                    int registrado = _sorteoSalaDAL.GuardarSorteoSala(item);
                }

                if (sesionesRegistradas > 0)
                {
                    var cabeceras = from sesion in listaSesiones
                                    group sesion by new
                                    {
                                        sesion.NroDocumento,
                                        sesion.NombreTipoDocumento,
                                        sesion.TipoDocumentoId
                                    } into grupo
                                    select new
                                    {
                                        NroDocumento = grupo.Key.NroDocumento,
                                        nombretipodocumento = grupo.Key.NombreTipoDocumento,
                                        tipodocumentoid = grupo.Key.TipoDocumentoId,
                                        CantidadSesiones = grupo.Count(),
                                        NombreCliente = grupo.Max(s => s.NombreCliente),
                                        Mail = grupo.Max(s => s.Mail),
                                        ClienteIdIas = grupo.Max(s => s.ClienteIdIas),
                                        PrimeraSesion = grupo.Min(s => s.FechaInicio),
                                        UltimaSesion=grupo.Max(s=>s.FechaInicio)
                                    };

                    foreach (var cabecera in cabeceras)
                    {
                        CMP_SesionCliente sesionClienteInsertar = new CMP_SesionCliente()
                        {
                            NroDocumento = cabecera.NroDocumento,
                            NombreTipoDocumento = cabecera.nombretipodocumento,
                            TipoDocumentoId = cabecera.tipodocumentoid,
                            CantidadSesiones = cabecera.CantidadSesiones,
                            NombreCliente = cabecera.NombreCliente,
                            Mail = cabecera.Mail,
                            PrimeraSesion = cabecera.PrimeraSesion,
                            CodSala = CodSala,
                            UltimaSesion=cabecera.UltimaSesion
                        };
                        _sesionClienteDAL.GuardarSesionCliente(sesionClienteInsertar);
                    }
                }

                Sala sala = new Sala() { 
                    CodSala=CodSala,
                    Nombre=NombreSala
                };

                _salaDAL.GuardarSala(sala);

                return Json(new { respuesta = true });
            }
            catch (Exception ex)
            {
                funciones.logueo("ERROR  - RecepcionarDataMigracion " + ex.Message, "Error");
                return Json(new { respuesta = false });
            }
        }
        [HttpPost]
        public IHttpActionResult RecepcionarDataMigracionExcaliburAnt(dynamic jsonData)
        {
            bool recepcionado = true;
            List<CMP_SesionExca> listaSesiones = new List<CMP_SesionExca>();
            List<CMP_SesionSorteoSalaExca> listaDetalles = new List<CMP_SesionSorteoSalaExca>();
            int sesionesRegistradas = 0;
            try
            {
                dynamic items = jsonData;
                if (items.sesiones != null)
                {
                    listaSesiones = items.sesiones.ToObject<List<CMP_SesionExca>>();
                }

                if (items.detalles != null)
                {
                    listaDetalles = items.detalles.ToObject<List<CMP_SesionSorteoSalaExca>>();
                }
              
                foreach (var item in listaSesiones)
                {
                    int registrado = _sesionExcaDAL.GuardarSesion(item);
                    if (registrado > 0)
                    {
                        sesionesRegistradas++;
                        var detallesSesion = listaDetalles.Where(x => x.SesionId == item.SesionId).ToList();
                        foreach (var det in detallesSesion)
                        {
                            int detalleRegistrado = _sesionSorteoSalaExcaDAL.GuardarSesionSorteSala(det);

                            var jugada = det.Jugada;
                            jugada.JugadaId = det.JugadaId;
                            int jugadaRegistrada = _jugadaExcaDAL.GuardarJugada(jugada);
                        }
                    }

                }
                if (sesionesRegistradas > 0)
                {
                    var cabeceras = from sesion in listaSesiones
                                    group sesion by new
                                    {
                                        sesion.NroDocumento,
                                    } into grupo
                                    select new
                                    {
                                        NroDocumento = grupo.Key.NroDocumento,
                                        CantidadSesiones = grupo.Count(),
                                        NombreCliente = grupo.Max(s => s.NombreCliente),
                                        Mail = grupo.Max(s => s.Mail),
                                        ClienteIdIas = grupo.Max(s => s.ClienteIdIas),
                                        PrimeraSesion = grupo.Min(s => s.FechaInicio),
                                    };

                    foreach (var cabecera in cabeceras)
                    {
                        CMP_SesionClienteExca sesionClienteInsertar = new CMP_SesionClienteExca()
                        {
                            NroDocumento = cabecera.NroDocumento,
                            CantidadSesiones = cabecera.CantidadSesiones,
                            NombreCliente = cabecera.NombreCliente,
                            Mail = cabecera.Mail,
                            PrimeraSesion = cabecera.PrimeraSesion,
                        };
                        _sesionClienteExcaDAL.GuardarSesionCliente(sesionClienteInsertar);
                    }
                }
                return Json(new { respuesta = true });
            }
            catch (Exception ex)
            {
                funciones.logueo("ERROR  - RecepcionarDataMigracionExcaliburAnt " + ex.Message, "Error");
                return Json(new { respuesta = false });
            }
        }
    }
}
