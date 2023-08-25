using Newtonsoft.Json;
using Quartz;
using ServicioMigracionClientes.Clases;
using ServicioMigracionClientes.DAL;
using ServicioMigracionClientes.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes.Jobs.MigracionData
{
    public class JobMigracionSesiones : IJob
    {
        public static string mensajeLog = "Job para migracion de sesiones desde IAS";
        private readonly string URL_IAS = ConfigurationManager.AppSettings["UrlIASAzure"];
        private readonly CMP_SesionMigracionDAL _sesionMigracionDAL = new CMP_SesionMigracionDAL();
        private readonly CMP_SesionSorteoSalaMigracionDAL _sesionSorteoSalaDAL= new CMP_SesionSorteoSalaMigracionDAL();
        private readonly CMP_SesionClienteMigracionDAL _sesionClienteMigracionDAL = new CMP_SesionClienteMigracionDAL();
        private readonly CMP_JugadasClienteDAL _jugadasClienteDAL = new CMP_JugadasClienteDAL();
        public Task Execute(IJobExecutionContext context)
        {
            funciones.logueo($"{mensajeLog} Iniciado");
            MigrarData();
            MigrarJugadasCliente();
            funciones.logueo($"{mensajeLog} Terminado");
            return Task.CompletedTask;
        }
        public void MigrarData()
        {
            try
            {
                
                var listaSesiones = ObtenerSesionesIasv2();
                //string idsDetalles = String.Join(",", listaSesiones.Select(x => x.Id));

                //var detalles = ObtenerDetalles(idsDetalles);

                //foreach (var item in listaSesiones)
                //{
                //    item.IdIas = item.Id;
                //    item.Id = 0;
                //    item.FechaInicio = item.FechaInicio.ToLocalTime();
                //    item.FechaTermino = item.FechaTermino.ToLocalTime();
                //    int registrado = _sesionMigracionDAL.GuardarSesion(item);
                //    var listaDetalle = detalles.Where(x => x.SesionMigracionId == item.IdIas).ToList();
                //    foreach (var det in listaDetalle)
                //    {
                //        det.Id = registrado;
                //        item.FechaRegistro = det.FechaRegistro.ToLocalTime();
                //        det.Fecha = det.Fecha.ToLocalTime();
                //        det.Hora = det.Hora.ToLocalTime();
                //        int detalleRegistrado = _sesionSorteoSalaDAL.GuardarSesionSorteoSalaMigracion(det);
                //    }
                //}
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
                        PrimeraSesion = grupo.Min(s => s.FechaInicio)
                    };
                foreach(var cabecera in cabeceras)
                {
                    CMP_SesionClienteMigracion sesionClienteInsertar = new CMP_SesionClienteMigracion() {
                        NroDocumento=cabecera.NroDocumento,
                        NombreTipoDocumento=cabecera.nombretipodocumento,
                        TipoDocumentoId=cabecera.tipodocumentoid,
                        CantidadSesiones=cabecera.CantidadSesiones,
                        NombreCliente=cabecera.NombreCliente,
                        Mail=cabecera.Mail,
                        PrimeraSesion=cabecera.PrimeraSesion
                    };
                    _sesionClienteMigracionDAL.GuardarSesionCliente(sesionClienteInsertar);
                }
            }
            catch (Exception ex)
            {
                var response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR TASK - Metodo MigrarData" + response, "Error");
            }
        }
        public void MigrarJugadasCliente()
        {
            try
            {
                var listaJugadas = ObtenerJugadasCliente();
                foreach (var item in listaJugadas)
                {
                    item.SesionMigracionId = item.Id;
                    item.Id = 0;
                    item.FechaInicio = item.FechaInicio.ToLocalTime();
                    item.FechaTermino = item.FechaTermino.ToLocalTime();
                    item.Fecha = item.Fecha.ToLocalTime();
                    item.Hora = item.Fecha.ToLocalTime();
                    int registrado = _jugadasClienteDAL.GuardarJugadaCliente(item);
                }
            }
            catch (Exception ex)
            {
                var response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR TASK Metodo MigrarJugadasCliente - " + response, "Error");
            }
        }
        public List<CMP_SesionMigracion> ObtenerSesionesIas()
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<CMP_SesionMigracion>();
            try
            {
                int maximoId = _sesionMigracionDAL.ObtenerMaximoIdIas();
                string urlIAS = $"{URL_IAS}/CampaniaSorteo/ObtenerDataSesiones?Id={maximoId}";
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<CMP_SesionMigracion>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO SESIONES - " + response, "Error");
            }
            return jsonResponse;
        }
        public List<CMP_SesionMigracion> ObtenerSesionesIasv2()
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<CMP_SesionMigracion>();
            List<CMP_SesionMigracion> result = new List<CMP_SesionMigracion>();
            try
            {
                int maximoId = _jugadasClienteDAL.ObtenerMaximoIdIas();
                string urlIAS = $"{URL_IAS}/CampaniaSorteo/ObtenerDataSesiones?Id={maximoId}";
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage httpResponse = httpClient.GetAsync(urlIAS).Result)
                    {
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            result = httpResponse.Content.ReadAsAsync<List<CMP_SesionMigracion>>().Result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO SESIONES - " + response, "Error");
            }
            return result;
        }
        public List<CMP_SesionSorteoSalaMigracion> ObtenerDetalles(string ids)
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<CMP_SesionSorteoSalaMigracion>();
            try
            {
                string urlIAS = $"{URL_IAS}/CampaniaSorteo/ObtenerDetallesSesiones?ids=" +ids;
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<CMP_SesionSorteoSalaMigracion>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO SESIONES - " + response, "Error");
            }
            return jsonResponse;
        }
        public List<CMP_JugadasCliente> ObtenerJugadasCliente()
        {
            string response = "";
            List<CMP_JugadasCliente> result = new List<CMP_JugadasCliente>();
            try
            {
                int maximoId = _jugadasClienteDAL.ObtenerMaximoIdIas();
                string urlIAS = $"{URL_IAS}/CampaniaSorteo/ObtenerJugadasCliente?maximoId={maximoId}";
                using (HttpClient httpClient = new HttpClient())
                {
                    using (HttpResponseMessage httpResponse = httpClient.GetAsync(urlIAS).Result)
                    {
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            result = httpResponse.Content.ReadAsAsync<List<CMP_JugadasCliente>>().Result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO JUGADAS DE CLIENTE - " + response, "Error");
            }
            return result;

        }
    }
}
