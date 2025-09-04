using System;

namespace ServicioServidorVPN.clases.Clientes.ControlAcceso {
    public class ClienteControlAccesoEntidad {
        public int IdCliente { get; set; }
        public int CodSala { get; set; }
        public string NombreSala { get; set; } = string.Empty;
        public int IdTipoDocumento { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public string TipoRegistro { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public DateTime FechaMigracion { get; set; }
    }
}
