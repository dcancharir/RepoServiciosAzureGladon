using System.Collections.Generic;
using System;
using System.Configuration;
using System.Data.SqlClient;
using ServicioAzureIAS.Clases.CampaniaCliente;
using ServicioAzureIAS.utilitarios;
using System.Net.Http;
using System.Management.Instrumentation;
using ServicioAzureIAS.Clases.Response;
using ServicioAzureIAS.Clases.WhatsApp;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace ServicioAzureIAS.DAL {
    public class CMP_ClienteDAL {
        private readonly int _diasExtraCodigoPromocional;
        private readonly string _conexion = string.Empty;
        private readonly HttpClient _httpClient;

        public CMP_ClienteDAL() {
            _diasExtraCodigoPromocional = Convert.ToInt32(ConfigurationManager.AppSettings["DiasExtraCodigoPromocional"]);
            _conexion = ConfigurationManager.ConnectionStrings["conectionBdSeguridad"].ConnectionString;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlIAS"]);
        }

        public List<CMP_ClienteEntidad> ObtenerCuponesExpirados() {
            List<CMP_ClienteEntidad> lista = new List<CMP_ClienteEntidad>();
            string consulta = @"
                SELECT
	                cc.id AS ID,
	                cc.cliente_id AS ClienteId,
	                cc.campania_id AS CampaniaId,
	                cc.fecha_reg AS FechaRegistro,
	                cc.codigo AS CodigoPromocional,
	                cc.codigoCanjeado AS CodigoCanjeado,
	                cc.fechaGeneracionCodigo AS FechaGeneracionCodigo,
	                cc.fechaExpiracionCodigo AS FechaExpiracionCodigo,
	                cc.fechaCanjeoCodigo AS FechaCanjeoCodigo,
	                cc.codigoExpirado AS CodigoExpirado,
	                s.CodSala AS CodSala,
	                COALESCE(NULLIF(cc.codigoPais, ''), '51') AS CodigoPais,
	                cc.NumeroCelular AS NumeroCelular,
	                s.Nombre AS NombreSala
                FROM
	                CMP_Cliente AS cc
				INNER JOIN
                    CMP_Campaña AS cca ON cca.id=cc.campania_id
                INNER JOIN
                    Sala AS s ON s.CodSala = cca.sala_id
                WHERE
	                codigoCanjeado = 0 AND
	                codigoExpirado = 0 AND
	                CONVERT(date, GETDATE()) > fechaExpiracionCodigo
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);

                    using(var dr = query.ExecuteReader()) {
                        if(dr.HasRows) {
                            while(dr.Read()) {
                                var detalle = new CMP_ClienteEntidad {
                                    id = ManejoNulos.ManageNullInteger64(dr["ID"]),
                                    cliente_id = ManejoNulos.ManageNullInteger64(dr["ClienteId"]),
                                    campania_id = ManejoNulos.ManageNullInteger64(dr["CampaniaId"]),
                                    fecha_reg = ManejoNulos.ManageNullDate(dr["FechaRegistro"]),
                                    Codigo = ManejoNulos.ManageNullStr(dr["CodigoPromocional"]),
                                    CodigoCanjeado = ManejoNulos.ManegeNullBool(dr["CodigoCanjeado"]),
                                    FechaGeneracionCodigo = ManejoNulos.ManageNullDate(dr["FechaGeneracionCodigo"]),
                                    FechaExpiracionCodigo = ManejoNulos.ManageNullDate(dr["FechaExpiracionCodigo"]),
                                    FechaCanjeoCodigo = ManejoNulos.ManageNullDate(dr["FechaCanjeoCodigo"]),
                                    CodigoExpirado = ManejoNulos.ManegeNullBool(dr["CodigoExpirado"]),
                                    CodSala = ManejoNulos.ManageNullInteger(dr["CodSala"]),
                                    CodigoPais = ManejoNulos.ManageNullStr(dr["CodigoPais"]),
                                    NumeroCelular = ManejoNulos.ManageNullStr(dr["NumeroCelular"]),
                                    NombreSala = ManejoNulos.ManageNullStr(dr["NombreSala"]),
                                };
                                lista.Add(detalle);
                            }
                        }
                    }
                }
                funciones.logueo($"{lista.Count} cupones expirados, listos para marcar como expirados y renovar fecha de expiración. {DateTime.Now}");
            } catch(Exception ex) {
                funciones.logueo($"Error al intentar obtener cupones expirados, {DateTime.Now}\n{ex.Message}", "Error");
                lista = new List<CMP_ClienteEntidad>();
            }
            return lista;
        }

        public int MarcarCuponesComoExpirado(List<long> ids) {
            if(ids.Count == 0) {
                funciones.logueo($"Intento de marcar cupones como expirados, pero no hay ni un cupon para actualizar, {DateTime.Now}", "Warn");
                return 0;
            }

            string idsStr = string.Join(",", ids);
            int filasAfectadas = 0;
            string consulta = $@"
                UPDATE
	                CMP_Cliente
                SET
	                codigoExpirado = 1,
	                fechaExpiracionCodigo = DATEADD(DAY, @diasExtraCodigoPromocional, fechaExpiracionCodigo)
                WHERE
	                codigoCanjeado = 0 AND
	                codigoExpirado = 0 AND
	                CONVERT(date, GETDATE()) > fechaExpiracionCodigo AND
	                id IN ({idsStr})

                SELECT @@ROWCOUNT
            ";
            try {
                using(var con = new SqlConnection(_conexion)) {
                    con.Open();
                    var query = new SqlCommand(consulta, con);
                    query.Parameters.AddWithValue("@diasExtraCodigoPromocional", _diasExtraCodigoPromocional);
                    filasAfectadas = Convert.ToInt32(query.ExecuteScalar());
                }
                funciones.logueo($"{filasAfectadas} cupones marcados como expirados y renovados: {idsStr}. {DateTime.Now}");
            } catch(Exception ex) {
                funciones.logueo($"Error al intentar marcar los cupones como expirados, {DateTime.Now}\n{ex.Message}", "Error");
                filasAfectadas = 0;
            }
            return filasAfectadas;
        }

        public async Task<ResponseEntidad<WSP_UltraMsgResponse>> SendMessageWhatsApp(WSP_SendMessage sendMessage) {
            ResponseEntidad<WSP_UltraMsgResponse> response = new ResponseEntidad<WSP_UltraMsgResponse>();
            string url = $"WhatsApp/SendMessage";
            try {
                // Convertir el objeto a formato JSON
                string jsonBody = JsonConvert.SerializeObject(sendMessage);
                HttpContent contentRequest = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                var responseServer = await _httpClient.PostAsync(url, contentRequest);
                var content = await responseServer.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<ResponseEntidad<WSP_UltraMsgResponse>>(content);
                string message = response.success ? $"Mensaje enviado correctamente por whatsapp a cliente con cupon expirado, {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}\nCodSala: {sendMessage.CodSala}\nNumero: {sendMessage.PhoneNumber}\nMensaje: {sendMessage.Message}" : response.displayMessage;
                funciones.logueo(message);
            } catch(Exception ex) {
                funciones.logueo($"Error al enviar mensaje por whatsapp a cliente con cupon expirado, {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}\nCodSala: {sendMessage.CodSala}\nNumero: {sendMessage.PhoneNumber}\nMensaje: {sendMessage.Message}\nError Message: {ex.Message}","Error");
            }
            return response;
        }
    }
}
