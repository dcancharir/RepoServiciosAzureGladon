using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.clases
{
    public class SesionSorteoSala
    {
        public long SesionId { get; set; }

        public int SorteoId { get; set; }

        public long JugadaId { get; set; }

        public int CantidadCupones { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public string SerieIni { get; set; }

        public string SerieFin { get; set; }

        public string NombreSorteo { get; set; }

        public double CondicionWin { get; set; }

        public double WinCalculado { get; set; }

        public double CondicionBet { get; set; }

        public double BetCalculado { get; set; }

        public int? TopeCuponesxJugada { get; set; }

        public string ParametrosImpresion { get; set; }

        public double Factor { get; set; }

        public double DescartePorFactor { get; set; }
        public virtual Sesion Sesion { get; set; }
        public virtual SorteoSala SorteoSala { get; set; }
        public virtual Jugada Jugada { get; set; } 
        public int CodSala { get; set; }
    }
}
