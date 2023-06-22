using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using ServicioAzureIAS.utilitarios;
using ServicioAzureIAS.Clases.Sala;
using System.Configuration;

namespace ServicioAzureIAS.Jobs.RegistroProgresivo
{
    public class RP_LimpiarHistorialJob : IJob
    {
        private readonly SalaDAL _salaDAL = new SalaDAL();
        private readonly CheckPortHelper _checkPortHelper = new CheckPortHelper();
        private readonly string _logMessage = "Limpiar Historial Registro Progresivo";

        public Task Execute(IJobExecutionContext context)
        {
            LimpiarHistorial();

            return Task.CompletedTask;
        }

        public void LimpiarHistorial()
        {
            funciones.logueo($"INICIO - {_logMessage}");

            try
            {
                int days = Convert.ToInt32(ConfigurationManager.AppSettings["DiasHistorialRegistroProgresivo"]);
                List<SalaEntidad> listaSalas = _salaDAL.ListarSalaActivas().Where(x => !string.IsNullOrEmpty(x.UrlProgresivo) && !string.IsNullOrEmpty(x.IpPrivada)).ToList();
                List<string> urls = listaSalas.Select(x => x.UrlProgresivo).ToList();

                foreach (SalaEntidad sala in listaSalas)
                {
                    CheckPortHelper.TcpConnection tcpConnection = _checkPortHelper.TcpUrlMultiple(sala.UrlProgresivo, urls);

                    if (tcpConnection.IsOpen)
                    {
                        string url = $"{sala.UrlProgresivo}/servicio/LimpiarHistorialRegistroProgresivo";

                        object arguments = new
                        {
                            dias = days
                        };

                        if (tcpConnection.IsVpn)
                        {
                            url = $"{tcpConnection.Url}/servicio/VPNGenericoPost";

                            arguments = new
                            {
                                dias = days,
                                ipPrivada = $"{sala.IpPrivada}:{sala.PuertoServicioWebOnline}/servicio/LimpiarHistorialRegistroProgresivo"
                            };
                        }

                        EnviarLimpiarHistorial(url, arguments, sala.Nombre);
                    }
                    else
                    {
                        funciones.logueo($"{sala.Nombre} => {sala.UrlProgresivo} => No responde", "Warn");
                    }
                }
            }
            catch (Exception exception)
            {
                funciones.logueo(exception.Message.ToString(), "Error");
            }

            funciones.logueo($"FIN - {_logMessage}");
        }

        public void EnviarLimpiarHistorial(string url, object parameters, string salaNombre)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    using (HttpResponseMessage httpResponse = httpClient.PostAsJsonAsync(url, parameters).Result)
                    {
                        if (httpResponse.IsSuccessStatusCode)
                        {
                            dynamic responseResult = httpResponse.Content.ReadAsAsync<dynamic>().Result;

                            funciones.logueo($"{salaNombre} => {url} => {httpResponse.ReasonPhrase}");
                        }
                        else
                        {
                            funciones.logueo($"{salaNombre} => {url} => {httpResponse.ReasonPhrase}", "Warn");
                        }
                    }
                }
            }
            catch(Exception exception)
            {
                funciones.logueo(exception.Message.ToString(), "Error");
            }
        }
    }
}
