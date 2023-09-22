using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.clases
{
    public class CMP_SesionSorteoSala
    {
        public int CodSala { get; set; }
        public long SesionId { get; set; }
        public long SorteoId { get; set; }
        public long JugadaId { get; set; }
        public int CantidadCupones { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string SerieIni { get; set; }
        public string SerieFin { get; set; }
        public string NombreSorteo { get; set; }
        public double CondicionWin { get; set; }
        public double CondicionBet { get; set; }
        public double EstadoCondicionBet { get; set; }
        public int TopeCuponesxJugada { get; set; }
        public double WinCalculado { get; set; }
        public double BetCalculado { get; set; }
        public CMP_Jugada Jugada { get; set; }
        public CMP_Sesion Sesion { get; set; }
        public string ParametrosImpresion { get; set; }
        public decimal Factor { get; set; }
        public decimal DescartePorFactor { get; set; }
    }
}
