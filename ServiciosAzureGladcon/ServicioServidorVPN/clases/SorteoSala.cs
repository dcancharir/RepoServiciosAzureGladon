using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.clases
{
    public class SorteoSala : AuditableEntity
    {
        public int SorteoId { get; set; }

        public int CodSala { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public int Estado { get; set; }

        public double CondicionWin { get; set; }

        public double CondicionBet { get; set; }

        public int EstadoCondicionBet { get; set; } = 0;

        public int TopeCuponesxJugada { get; set; }
        public virtual ICollection<SesionSorteoSala> SesionSorteoSalas { get; set; } = new List<SesionSorteoSala>();
    }
}
