using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.clases
{
    public class Asignacion_M_T
    {
		public string COD_EMPRESA { get; set; } = string.Empty;

		public string COD_SALA { get; set; } = string.Empty;

		public int nro { get; set; }

		public string CodTarjeta { get; set; } = string.Empty;

		public int CodTarjeta_Seg { get; set; }

		public string CodMaq { get; set; }

		public string CodMaqMin { get; set; } = string.Empty;

		public string Modelo { get; set; } = string.Empty;

		public int? COD_TIPOMAQUINA { get; set; }

		public string Posicion { get; set; } = string.Empty;

		public int Tpro { get; set; }

		public int? Estado { get; set; }

		public int CodFicha { get; set; }

		public int CodMarca { get; set; }

		public int CodModelo { get; set; }

		public int? Hopper { get; set; }

		public int? CredOtor { get; set; }

		public decimal? Token { get; set; }

		public int? NroContradores { get; set; }

		public decimal? Precio_Credito { get; set; }

		public string NUM_SERIE { get; set; } = string.Empty;

		public int? idTipoMoneda { get; set; }

		public int? MODALIDAD { get; set; }

		public int? MAQ_X { get; set; }

		public int? MAQ_Y { get; set; }

		public string MAQ_ASIGLAYOUT { get; set; } = string.Empty;

		public int? MAQ_POSILAYOUT { get; set; }

		public int? CIn { get; set; }

		public int? COut { get; set; }

		public int? TipoTranSac { get; set; }

		public int? cod_caja { get; set; }

		public int? TopeCreditos { get; set; }

		public int? S_ONLINE { get; set; }

		public string FORMULA_MAQ { get; set; } = string.Empty;

		public decimal? MAQ_DEVPORCENTAJE { get; set; }

		public string FormulaFinal { get; set; } = string.Empty;	

		public string Dbase { get; set; } = string.Empty;

		public bool? EnviaDbase { get; set; }

		public int? cod_servidor { get; set; }

		public int? Cod_Socio { get; set; }

		public int? Status_Online { get; set; }

		public int? COD_MODELO { get; set; }

		public int? Sistema { get; set; }

		public string PosicionBilletero { get; set; } = string.Empty;

		public int? con_sorteo { get; set; }

		public int? Block { get; set; }

		public string codigo_extra { get; set; } = string.Empty;

		public int? PromoBonus { get; set; }

		public int? PromoTicket { get; set; }

		public int? estadoT { get; set; }
		public int retiroTemporal { get; set; } = 0;
	}
}
