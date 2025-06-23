using Newtonsoft.Json;
using ServicioAzureIAS.Clases.GestioProyectos;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Service {
    public class GestorBukService {
        private readonly HttpClient HttpClient;
        private readonly string Uri;

        public GestorBukService() {
            HttpClient = new HttpClient();
            HttpClient.Timeout = Timeout.InfiniteTimeSpan;
            Uri = ConfigurationManager.AppSettings["BukApiUri"];
        }

        public async Task<List<EmpleadosApi>> ObtenerEmpleadosDesdeApi(long idbuk) {
            List<EmpleadosApi> empleados = new List<EmpleadosApi>();
           
            string url = $"{Uri}/api/EntradaSalida/companies/{idbuk}/employees";

            try {
                
                HttpResponseMessage response = await HttpClient.GetAsync(url);

                string jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<EmpleadosResponse>(jsonString);

                if(!response.IsSuccessStatusCode) {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"Http Code: {response.StatusCode}");
                    sb.AppendLine($"Empresa BUK: {idbuk}");
                    sb.AppendLine($"Display Message: {apiResponse.DisplayMessage}");
                    sb.AppendLine($"Error Messages: {string.Join("\n", apiResponse.ErrorMessage)}");
                    
                    funciones.logueo(sb.ToString());
                }

                //if(response.StatusCode == HttpStatusCode.NotFound) {
                //    // Registro opcional del 404
                //    funciones.logueo($"No se encontraron empleados para la empresa ID_BUK: {idbuk} (404)");
                //    return empleados;
                //}
                //if(response.StatusCode == HttpStatusCode.BadRequest) {
                //    // Registro opcional del 404
                //    funciones.logueo($"No se encontraron empleados para la empresa ID_BUK: {idbuk} (400)");
                //    return empleados;
                //}
                if(response.StatusCode == HttpStatusCode.OK) {
                    funciones.logueo($"se encontraron: {apiResponse.Data.Count} empleados para la empresa ID_BUK: {idbuk} (200)");
                }




                if(apiResponse?.Data != null) {
                    empleados = apiResponse.Data;
                }
            } catch(Exception ex) {
                string errorDetail = $"Error al obtener empleados desde API.\n" +
                                     $"URL: {url}\n" +
                                     $"Mensaje: {ex.Message}\n" +
                                     $"InnerException: {ex.InnerException?.Message}\n" +
                                     $"StackTrace: {ex.StackTrace}";
                funciones.logueo(errorDetail);
            }

            return empleados;
        }

        public async Task<List<EmpresaApi>> ObtenerEmpresaDesdeApi() {
            List<EmpresaApi> empresas = new List<EmpresaApi>();
            string url = $"{Uri}/ofisis/ListarEmpresas";

            try {
                var content = new StringContent("", System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await HttpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonConvert.DeserializeObject<EmpresaResponse>(jsonString);

                if(apiResponse?.data != null) {
                    empresas = apiResponse.data;
                }
            } catch(Exception ex) {
                string errorDetail = $"Error al obtener empresas desde API.\n" +
                                     $"URL: {url}\n" +
                                     $"Mensaje: {ex.Message}\n" +
                                     $"InnerException: {ex.InnerException?.Message}\n" +
                                     $"StackTrace: {ex.StackTrace}";
                funciones.logueo(errorDetail);
            }

            return empresas;
        }

    }
}
