using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.clases
{
    public class TCM_Modelo
    {
        public int CodSala { get; set; }
        public int COD_MODELO { get; set; }

        public int COD_MARCA { get; set; }

        public string DESC_MODELO { get; set; }

        public string S_ESTADO { get; set; }

        public int CE_ENTRADA { get; set; }

        public int CE_SALIDA { get; set; }

        public int CE_PAGOMANUAL { get; set; }

        public int CM_ENTRADA { get; set; }

        public int CM_SALIDA { get; set; }

        public int CM_PAGOMANUAL { get; set; }

        public int CB_BILLETERO { get; set; }

        public int estadoT { get; set; }
    }
}
