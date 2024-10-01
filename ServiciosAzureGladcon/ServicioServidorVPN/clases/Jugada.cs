using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.clases
{
    public class Jugada
    {
        public long JugadaId { get; set; }

        public long? Cod_Cont_OL { get; set; }

        public DateTime? Fecha { get; set; }

        public DateTime? Hora { get; set; }

        public string CodMaq { get; set; }

        public double? CoinOut { get; set; }

        public double? CurrentCredits { get; set; }

        public double? Monto { get; set; }

        public double? Token { get; set; }

        public double? CoinOutAnterior { get; set; }

        public int? Estado_Oln { get; set; }

        public DateTime? FechaRegistro { get; set; }

        public double? HandPay { get; set; }

        public double? JackPot { get; set; }

        public double? HandPayAnterior { get; set; }

        public double? JackPotAnterior { get; set; }

        public double? CoinIn { get; set; }

        public double? CoinInAnterior { get; set; }

        public long? Cod_Cont_OLAnterior { get; set; }
        public double? GamesPlayed { get; set; }
        public double? GamesPlayedAnterior { get; set; }
        public virtual ICollection<SesionSorteoSala> SesionSorteoSalas { get; set; } = new List<SesionSorteoSala>();
        public int CodSala { get; set; }
    }
}
