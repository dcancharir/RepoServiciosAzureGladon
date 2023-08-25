using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Quartz;
using ServicioMigracionClientes.Clases;
using ServicioMigracionClientes.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioMigracionClientes.utilitarios;
using System.ComponentModel;

namespace ServicioMigracionClientes.Jobs.MigracionData
{
    public class JobMigracionGladonData : IJob
    {
        public static string mensajeLog = "Job para migracion de info desde IAS";
        private readonly string URL_SERVICIO_IAS = ConfigurationManager.AppSettings["UrlServicioIAS"];
        private readonly consolidadoDAL _consolidadoDAL=new consolidadoDAL();
        private readonly consolidado_deleteDAL _consolidadoDeleteDAL = new consolidado_deleteDAL();
        private readonly consolidado_tmpDAL _consolidadoTmpDAL = new consolidado_tmpDAL();
        private readonly detalle_maquinaDAL _detalleMaquinaDAL=new detalle_maquinaDAL();
        private readonly detalle_maquinas_auditDAL _detalleMaquinaAuditDAL = new detalle_maquinas_auditDAL();
        private readonly maquinaDAL _maquinaDAL=new maquinaDAL();
        private readonly maquinas_auditDAL _maquinasAuditDAL=new maquinas_auditDAL();
        private readonly salaDAL _salaDAL=new salaDAL();
        public Task Execute(IJobExecutionContext context)
        {
            MigrarData();
            return Task.CompletedTask;
        }
        public void MigrarData()
        {
            funciones.logueo("Job Migracion Informacion de GladconData Iniciado");
            try
            {
                var ultimoConsolidado = ObtenerUltimoId("consolidado","consolidado_id_ias");
                var ultimoConsolidadoDelete = ObtenerUltimoId("consolidado_delete","consolidado_delete_id_ias");
                var ultimoConsolidadoTmp = ObtenerUltimoId("consolidado_tmp", "consolidado_tmp_id_ias");
                var ultimoDetalleMaquina = ObtenerUltimoId("detalle_maquina", "detalle_maquina_id_ias");
                var ultimoDetalleMaquinasAudit = ObtenerUltimoId("detalle_maquinas_audit", "detalle_maquinas_audit_id_ias");
                var ultimoMaquina = ObtenerUltimoId("maquina", "maquina_id_ias");
                var ultimoMaquinasAudit = ObtenerUltimoId("maquinas_audit", "maquinas_audit_id_ias");

                var listaConsolidado = ObtenerListaConsolidado(ultimoConsolidado);
                var listaConsolidadoDelete = ObtenerListaConsolidadoDelete(ultimoConsolidadoDelete);
                var listaConsolidadoTmp = ObtenerListaConsolidadoTmp(ultimoConsolidadoTmp);
                var listaDetalleMaquina = ObtenerListaDetalleMaquina(ultimoDetalleMaquina);
                var listaDetalleMaquinaAudit = ObtenerListaDetalleMaquinasAudit(ultimoDetalleMaquinasAudit);
                var listaMaquina = ObtenerListaMaquina(ultimoMaquina);
                var listaMaquinasAudit = ObtenerListaMaquinasAudit(ultimoMaquinasAudit);
                var listaSalas = ObtenerListaSala();

                foreach (var item in listaConsolidado)
                {
                    item.consolidado_id_ias = item.consolidado_id;
                    item.consolidado_id = 0;
                    _consolidadoDAL.ConsolidadoInsertar(item);
                }
                funciones.logueo("Tabla consolidado migrada - "+listaConsolidado.Count + " - registros");
                foreach (var item in listaConsolidadoDelete)
                {
                    item.consolidado_delete_id_ias = item.consolidado_delete_id;
                    item.consolidado_delete_id = 0;
                    _consolidadoDeleteDAL.InsertarConsolidadoDelete(item);
                }
                funciones.logueo("Tabla consolidado_delete migrada" + listaConsolidadoDelete.Count+" - registros");
                foreach (var item in listaConsolidadoTmp)
                {
                    item.consolidado_tmp_id_ias = item.consolidado_tmp_id;
                    item.consolidado_tmp_id = 0;
                    _consolidadoTmpDAL.InsertarConsolidadoTMP(item);
                }
                funciones.logueo("Tabla consolidado_tmp migrada" + listaConsolidadoTmp.Count+" - registros");
                foreach (var item in listaDetalleMaquina)
                {
                    item.detalle_maquina_id_ias = item.detalle_maquina_id;
                    item.detalle_maquina_id = 0;
                    _detalleMaquinaDAL.InsertarDetalleMaquina(item);
                }
                funciones.logueo("Tabla detalle_maquina migrada" + listaDetalleMaquina+" - registros");
                foreach (var item in listaDetalleMaquinaAudit)
                {
                    item.detalle_maquinas_audit_id_ias = item.detalle_maquinas_audit_id;
                    item.detalle_maquinas_audit_id = 0;
                    _detalleMaquinaAuditDAL.InsertarDetalleMaquinasAudit(item);
                }
                funciones.logueo("Tabla detalle_maquinas_audit migrada" + listaDetalleMaquinaAudit + " - registros");
                foreach (var item in listaMaquina)
                {
                    item.maquina_id_ias=item.maquina_id;
                    item.maquina_id=0;
                    _maquinaDAL.InsertarMaquina(item);  
                }
                funciones.logueo("Tabla maquina migrada" +listaMaquina.Count() + " - registros");
                foreach (var item in listaMaquinasAudit)
                {
                    item.maquinas_audit_id_ias = item.maquinas_audit_id;
                    item.maquinas_audit_id=0;
                    _maquinasAuditDAL.InsertarMaquinasAudit(item);
                }
                funciones.logueo("Tabla maquinas_audit migrada" + listaMaquinasAudit.Count()+" - registros");
                foreach (var item in listaSalas)
                {
                    _salaDAL.InsertarSala(item);
                }
                funciones.logueo("Tabla sala migrada"+listaSalas.Count()+" - registros");
            }
            catch (Exception ex)
            {
                var response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR Job Migracion GladonData - " + response, "Error");
            }
            funciones.logueo("Job Migracion Informacion de GladconData Terminado");
        }
        public List<consolidado> ObtenerListaConsolidado(int maximoId)
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<consolidado>();
            try
            {
                string urlIAS = $"{URL_SERVICIO_IAS}/Servicio/ListarConsolidado?Id={maximoId}";
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<consolidado>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO Consolidado - " + response, "Error");
            }
            return jsonResponse;
        }
        public List<consolidado_delete> ObtenerListaConsolidadoDelete(int maximoId)
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<consolidado_delete>();
            try
            {
                string urlIAS = $"{URL_SERVICIO_IAS}/Servicio/ListarConsolidadoDelete?Id={maximoId}";
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<consolidado_delete>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO ConsolidadoDelete - " + response, "Error");
            }
            return jsonResponse;
        }
        public List<consolidado_tmp> ObtenerListaConsolidadoTmp(int maximoId)
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<consolidado_tmp>();
            try
            {
                string urlIAS = $"{URL_SERVICIO_IAS}/Servicio/ListarConsolidadoTmp?Id={maximoId}";
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<consolidado_tmp>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO ConsolidadoTmp - " + response, "Error");
            }
            return jsonResponse;
        }
        public List<detalle_maquina> ObtenerListaDetalleMaquina(int maximoId)
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<detalle_maquina>();
            try
            {
                string urlIAS = $"{URL_SERVICIO_IAS}/Servicio/ListarDetalleMaquina?Id={maximoId}";
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<detalle_maquina>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO Detalle Maquina - " + response, "Error");
            }
            return jsonResponse;
        }
        public List<detalle_maquinas_audit> ObtenerListaDetalleMaquinasAudit(int maximoId)
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<detalle_maquinas_audit>();
            try
            {
                string urlIAS = $"{URL_SERVICIO_IAS}/Servicio/ListarDetalleMaquinasAudit?Id={maximoId}";
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<detalle_maquinas_audit>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO Detalle Maquina Audit - " + response, "Error");
            }
            return jsonResponse;
        }
        public List<maquina> ObtenerListaMaquina(int maximoId)
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<maquina>();
            try
            {
                string urlIAS = $"{URL_SERVICIO_IAS}/Servicio/ListarMaquina?Id={maximoId}";
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<maquina>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO Maquina - " + response, "Error");
            }
            return jsonResponse;
        }
        public List<maquinas_audit> ObtenerListaMaquinasAudit(int maximoId)
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<maquinas_audit>();
            try
            {
                string urlIAS = $"{URL_SERVICIO_IAS}/Servicio/ListarMaquinasAudit?Id={maximoId}";
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<maquinas_audit>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO MaquinasAudit - " + response, "Error");
            }
            return jsonResponse;
        }
        public List<sala> ObtenerListaSala()
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<sala>();
            try
            {
                string urlIAS = $"{URL_SERVICIO_IAS}/Servicio/ListarSala";
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<sala>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO Sala --" + response, "Error");
            }
            return jsonResponse;
        }
        private int ObtenerUltimoId(string tabla, string campo)
        {
            int UltimoId = 0;//Por defecto es 0
            string result = string.Empty;
            try
            {
                string consulta = $"Select max({campo}) as maximoId from dbo.{tabla}";
                result = funciones.consultaBDMigracion(consulta);
                JArray Salaobj = JArray.Parse(result);
                UltimoId = Convert.ToInt32(Salaobj.First()["maximoId"]);
            }
            catch (Exception)
            {
                UltimoId = 0;
            }
            return UltimoId;
        }
    }
}
