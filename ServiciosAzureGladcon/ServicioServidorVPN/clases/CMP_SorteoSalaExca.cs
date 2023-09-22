using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.clases
{
    public class CMP_SorteoSalaExca
    {
        public int SorteoId { get; set; }

        public int CodSala { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaRegistro { get; set; }

        public DateTime FechaModificacion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int UsuarioCreacion { get; set; }

        public int Estado { get; set; }

        public decimal CondicionWin { get; set; }

        public decimal CondicionBet { get; set; }

        public int EstadoCondicionBet { get; set; }

        public int TopeCuponesxJugada { get; set; }
    }
}
