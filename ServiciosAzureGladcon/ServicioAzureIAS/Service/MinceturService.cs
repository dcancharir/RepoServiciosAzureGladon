using Newtonsoft.Json;
using ServicioAzureIAS.Clases.Mincetur;
using ServicioAzureIAS.utilitarios;
using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Service {
    public class MinceturService {
        private readonly HttpClient HttpClient;
        private readonly string UriLudopatasMincetur;

        public MinceturService() {
            HttpClient = new HttpClient();
            HttpClient.Timeout = Timeout.InfiniteTimeSpan;
            UriLudopatasMincetur = ConfigurationManager.AppSettings["UriLudopatasMincetur"];
        }

        public async Task<ResponseMinceturLudopata> ObtenerLudopataMincetur(string credencial) {
            ResponseMinceturLudopata response;
            string url = $"{UriLudopatasMincetur}/{credencial}/consultarRegistroLudopatia";

            try {
                HttpResponseMessage responseServer = await HttpClient.GetAsync(url);
                string contentResponse = await responseServer.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ResponseMinceturLudopata>(contentResponse) ?? new ResponseMinceturLudopata();
            } catch(Exception ex) {
                string errorDetail = $"Error al obtener padrón de ludópatas de MINCETUR.\n" +
                                     $"URL: {url}\n" +
                                     $"Credencial: {credencial}\n" +
                                     $"Mensaje: {ex.Message}\n" +
                                     $"InnerException: {ex.InnerException?.Message}\n" +
                                     $"StackTrace: {ex.StackTrace}";
                funciones.logueo(errorDetail);
                response = new ResponseMinceturLudopata();
            }

            return response;
        }

        public ResponseMinceturLudopata ObtenerLudopataMinceturLocal() {
            ResponseMinceturLudopata response = new ResponseMinceturLudopata();
            try {
                string path = "../../Jobs/ControlAccesoLudopata/ludopatas.json";
                using(StreamReader sr = new StreamReader(path))
                using(JsonTextReader reader = new JsonTextReader(sr)) {
                    JsonSerializer serializer = new JsonSerializer();
                    response = serializer.Deserialize<ResponseMinceturLudopata>(reader) ?? new ResponseMinceturLudopata();
                }
            } catch(JsonReaderException ex) {
                // Maneja errores relacionados con el parsing del JSON
                Console.WriteLine($"Error de lectura del JSON: {ex.Message}");
            } catch(JsonSerializationException ex) {
                // Maneja errores relacionados con la serialización/deserialización
                Console.WriteLine($"Error de serialización: {ex.Message}");
            } catch(IOException e) {
                Console.WriteLine("Error al leer el archivo: " + e.Message);
            } catch(Exception ex) {
                // Otros errores generales
                Console.WriteLine($"Error: {ex.Message}");
            }
            return response;
        }
    }
}
