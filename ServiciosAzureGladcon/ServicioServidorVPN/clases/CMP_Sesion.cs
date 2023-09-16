using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.clases
{
    public class CMP_Sesion
    {
        public int CodSala { get; set; }
        public Int64 SesionId { get; set; }

        public string CodMaquina { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaTermino { get; set; }

        public int ClienteIdIas { get; set; }

        public string NombreCliente { get; set; }

        public string NroDocumento { get; set; }

        public int UsuarioIas { get; set; }

        public int Terminado { get; set; }

        public string MotivoTermino { get; set; }

        public int UsuarioTermino { get; set; }

        public int EstadoEnvio { get; set; }
        public int CantidadCupones { get; set; }
        public int CantidadJugadas { get; set; }
        public string SerieIni { get; set; }
        public string SerieFin { get; set; }
        public string Mail { get; set; }
        //
        public int CantidadSesiones { get; set; }
        public CMP_Sesion()
        {
        }
        public int TipoDocumentoId { get; set; }
        public string NombreTipoDocumento { get; set; }
        public int TipoSesion { get; set; }
    }
}
