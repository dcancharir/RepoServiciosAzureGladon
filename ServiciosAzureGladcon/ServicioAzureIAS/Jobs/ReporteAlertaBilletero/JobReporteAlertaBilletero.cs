using Quartz;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Jobs.ReporteAlertaBilletero
{
    public class JobReporteAlertaBilletero : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            funciones.logueo("Enviando reporte alerta billeteros");
            await ConsultarAlertasDiarias();

        }

        public async Task ConsultarAlertasDiarias()
        {
            string url = "http://40.122.134.6/IAS/Revisiones/GenerarReportealertaBilletero";
            //string url = "http://192.168.1.200:801/ias/Revisiones/GenerarReportealertaBilletero";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpContent content = null;

                    HttpResponseMessage response = await httpClient.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        funciones.logueo("El reporte alerta billeteros se envio exitosamente.");
                    }
                    else
                    {
                        funciones.logueo("Error al enviar el reporte alerta billeteros");
                    }
                }
                catch (HttpRequestException e)
                {
                    funciones.logueo($"Error en la solicitud HTTP: {e.Message}");
                }

            }

        }
    }
}
