using Quartz;
using ServicioGladconData.Clases;
using ServicioGladconData.DAL;
using ServicioGladconData.utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

namespace ServicioGladconData.Jobs.MigracionPostgres
{
    public class JobMigracionPostgres:IJob
    {
        private readonly consolidadoDAL _consolidadoDAL= new consolidadoDAL();
        private readonly consolidado_deleteDAL _consolidadoDeleteDAL= new consolidado_deleteDAL();
        private readonly consolidado_tmpDAL _consolidadoTmpDAL= new consolidado_tmpDAL();
        private readonly detalle_maquinaDAL _detalleMaquinaDAL = new detalle_maquinaDAL();
        private readonly detalle_maquinas_auditDAL _detalleMaquinasAuditDAL = new detalle_maquinas_auditDAL();
        private readonly maquinaDAL _maquinaDAL=new maquinaDAL();
        private readonly maquinas_auditDAL _maquinasAuditDAL=new maquinas_auditDAL();
        private readonly salaDAL _salaDAL = new salaDAL();
        private readonly string _urlIAS = string.Empty;
        public JobMigracionPostgres()
        {
            _urlIAS = ConfigurationManager.AppSettings["urlIAS"];
        }
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Job Iniciado Ahora");
            DateTime fechaOperacion = DateTime.Now.Date;
            MigrarData(fechaOperacion);
            return Task.CompletedTask;
        }
        public void MigrarData(DateTime fechaOperacion)
        {
            var consolidados = _consolidadoDAL.ListarConsolidadoTodo();
            var consolidados_delete = _consolidadoDeleteDAL.ListarConsolidadoDeleteTodo();
            var consolidados_tmp = _consolidadoTmpDAL.ListarConsolidadoTMPTodo();
            var detalles_maquinas = _detalleMaquinaDAL.ListarDetalleMaquina();
            var maquinas = _maquinaDAL.ListarMaquina();
            var maquinas_audit = _maquinasAuditDAL.ListarMaquinasAudit();
            var salas = _salaDAL.ListarSala();
            funciones.logueo($@"
                    Total Consolidados=${consolidados.Count}        
                    Total Consolidados Delete=${consolidados_delete.Count}        
                    Total Consolidados Tmp=${consolidados_tmp.Count}        
                    Total Detalles Maquina=${detalles_maquinas.Count}        
                    Total Maquinas=${maquinas.Count}        
                    Total Maquinas Audit=${maquinas_audit.Count}        
                    Total Salas=${salas.Count}        
                    ");
            object oEnvio = new {
                consolidados= consolidados,
                consolidados_delete= consolidados_delete,
                consolidados_tmp= consolidados_tmp,
                detalles_maquinas= detalles_maquinas,
                maquinas= maquinas,
                maquinas_audit= maquinas_audit,
                salas= salas
            };
            string uri = "Servicio/RecepcionarGladconData";
            EnviarPostIASAsync(oEnvio,uri);
        }
        private async Task<bool> EnviarPostIASAsync(object oEnvio, string uri)
        {
            bool response = false;
            string url = $"{_urlIAS}{uri}";

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string jsonData = JsonConvert.SerializeObject(oEnvio);
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    using (HttpResponseMessage httpResponse = await httpClient.PostAsync(url, content))
                    {
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            string result = await httpResponse.Content.ReadAsStringAsync();
                            // Aquí puedes trabajar con el contenido de la respuesta
                            response = true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                funciones.logueo($"{ErrorLinea.Linea(exception)} : {exception.Message}", "Error");
            }

            return response;
        }
    }
}
