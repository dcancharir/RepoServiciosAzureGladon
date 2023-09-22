using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.clases
{
    public class CMP_SesionSorteoSalaExca
    {
        public long SesionId { get; set; }

        public long SorteoId { get; set; }

        public long JugadaId { get; set; }

        public int CantidadCupones { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string SerieIni { get; set; }

        public string SerieFin { get; set; }

        public string NombreSorteo { get; set; }

        public decimal CondicionWin { get; set; }

        public decimal WinCalculado { get; set; }

        public decimal CondicionBet { get; set; }

        public decimal BetCalculado { get; set; }

        public int TopeCuponesxJugada { get; set; }
        public CMP_JugadaExca Jugada { get; set; }
        public CMP_SesionExca Sesion { get; set; }
    }
}
