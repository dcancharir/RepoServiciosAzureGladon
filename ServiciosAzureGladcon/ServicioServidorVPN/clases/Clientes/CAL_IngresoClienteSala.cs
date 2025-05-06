using Newtonsoft.Json;
using System;

namespace ServicioServidorVPN.clases.Clientes {
    public class CAL_IngresoClienteSala {
        [JsonProperty("idAuditoria")]
        public int Id { get; set; }
        [JsonProperty("sala")]
        public string NombreSala { get; set; } = string.Empty;
        [JsonProperty("dni")]
        public string NumeroDocumento { get; set; } = string.Empty;
        [JsonProperty("nombre")]
        public string NombreCliente { get; set; } = string.Empty;
        [JsonProperty("tipo")]
        public string Tipo { get; set; } = string.Empty;
        [JsonProperty("codigo")]
        public string Codigo { get; set; } = string.Empty;
        [JsonProperty("fecha")]
        //[JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime FechaRegistro { get; set; }
        //[JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime FechaMigracion { get; set; }
    }
}
