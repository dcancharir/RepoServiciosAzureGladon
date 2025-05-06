using Newtonsoft.Json;
using ServicioServidorVPN.clases.Clientes;
using ServicioServidorVPN.clases.Response.Ias;
using ServicioServidorVPN.Helpers.Http;
using ServicioServidorVPN.utilitarios;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.Service.Ias.Clientes {
    public class IngresoClienteSalaService {
        private readonly HttpClientIas _httpClientIas;

        public IngresoClienteSalaService() {
            _httpClientIas = new HttpClientIas();
        }

        public async Task<bool> MarcarComoMigrados(List<int> ids, DateTime fechaMigracionDwh) {
            ResponseServiceIas response = new ResponseServiceIas();
            string url = "CALAuditoria/MarcarComoMigrados";
            try {
                object payload = new {
                    ids,
                    fechaMigracionDwh
                };
                string json = JsonConvert.SerializeObject(payload);
                HttpResponseMessage responseServer = await _httpClientIas.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                string content = await responseServer.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ResponseServiceIas>(content) ?? new ResponseServiceIas();
            } catch(Exception ex) {
                funciones.logueo($"Error al marcar ingresos de clientes a sala como migradas del ias. {ex.Message}", "Error");
                throw;
            }
            return response.Success;
        }

        public async Task<List<CAL_IngresoClienteSala>> ObtenerIngresosClientesIas(int cantidadRegistros) {
            ResponseServiceIas<List<CAL_IngresoClienteSala>> response = new ResponseServiceIas<List<CAL_IngresoClienteSala>>();
            string url = "CALAuditoria/ObtenerIngresosClientesSalasParaDwh";
            try {
                object payload = new {
                    CantidadRegistros = cantidadRegistros
                };
                string json = JsonConvert.SerializeObject(payload);
                HttpResponseMessage responseServer = await _httpClientIas.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                string content = await responseServer.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ResponseServiceIas<List<CAL_IngresoClienteSala>>>(content) ?? new ResponseServiceIas<List<CAL_IngresoClienteSala>>();
            } catch(Exception ex) {
                funciones.logueo($"Error al obtener ingresos de clientes del ias. {ex.Message}", "Error");
                throw;
            }
            return response.Data;
        }

        public async Task<bool> RevertirEstadoMigracion(List<int> ids) {
            ResponseServiceIas response = new ResponseServiceIas();
            string url = "CALAuditoria/RevertirEstadoMigracion";
            try {
                object payload = new {
                    ids
                };
                string json = JsonConvert.SerializeObject(payload);
                HttpResponseMessage responseServer = await _httpClientIas.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                string content = await responseServer.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ResponseServiceIas>(content) ?? new ResponseServiceIas();
            } catch(Exception ex) {
                funciones.logueo($"Error al revertir ingreso de clientes del ias. {ex.Message}", "Error");
                throw;
            }
            return response.Success;
        }
    }
}
