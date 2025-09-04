using Newtonsoft.Json;
using ServicioServidorVPN.clases.Clientes.ControlAcceso;
using ServicioServidorVPN.clases.Response.Ias;
using ServicioServidorVPN.Helpers.Http;
using ServicioServidorVPN.utilitarios;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.Service.Ias.Clientes.ControlAcceso {
    public class ClientesControlAccesoService {
        private readonly HttpClientIas _httpClientIas;

        public ClientesControlAccesoService() {
            _httpClientIas = new HttpClientIas();
        }

        public async Task<bool> MarcarComoMigrados(List<ClienteControlAccesoIds> ids, DateTime fechaMigracionDwh) {
            ResponseServiceIas response = new ResponseServiceIas();
            string url = "AsistenciaCliente/MarcarComoMigrados";
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
                funciones.logueo($"Error al marcar clientes de control de acceso como migrados del IAS. {ex.Message}", "Error");
                throw;
            }
            return response.Success;
        }

        public async Task<List<ClienteControlAccesoEntidad>> ObtenerClientesControlAccesoIas(int cantidadRegistros) {
            ResponseServiceIas<List<ClienteControlAccesoEntidad>> response = new ResponseServiceIas<List<ClienteControlAccesoEntidad>>();
            string url = "AsistenciaCliente/ObtenerClientesControlAccesoParaDwh";
            try {
                object payload = new {
                    CantidadRegistros = cantidadRegistros
                };
                string json = JsonConvert.SerializeObject(payload);
                HttpResponseMessage responseServer = await _httpClientIas.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                string content = await responseServer.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ResponseServiceIas<List<ClienteControlAccesoEntidad>>>(content) ?? new ResponseServiceIas<List<ClienteControlAccesoEntidad>>();
            } catch(Exception ex) {
                funciones.logueo($"Error al obtener clientes de control de acceso del IAS. {ex.Message}", "Error");
                throw;
            }
            return response.Data;
        }

        public async Task<bool> RevertirEstadoMigracion(List<ClienteControlAccesoIds> ids) {
            ResponseServiceIas response = new ResponseServiceIas();
            string url = "AsistenciaCliente/RevertirEstadoMigracion";
            try {
                object payload = new {
                    ids
                };
                string json = JsonConvert.SerializeObject(payload);
                HttpResponseMessage responseServer = await _httpClientIas.PostAsync(url, new StringContent(json, Encoding.UTF8, "application/json"));
                string content = await responseServer.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ResponseServiceIas>(content) ?? new ResponseServiceIas();
            } catch(Exception ex) {
                funciones.logueo($"Error al revertir estado de migración de clientes de control de acceso del IAS. {ex.Message}", "Error");
                throw;
            }
            return response.Success;
        }
    }
}
