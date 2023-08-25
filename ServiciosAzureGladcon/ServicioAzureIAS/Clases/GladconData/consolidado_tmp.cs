using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Clases.GladconData
{
    public class consolidado_tmp
    {
        public int consolidado_tmp_id { get; set; }
        public int id_consolidado_tmp { get; set; }
        public DateTime fecha { get; set; }
        public int sala { get; set; }
        public string cod_maquina { get; set; }
        public string serie { get; set; }
        public double coin_in { get; set; }
        public double net_win { get; set; }
        public double average_bet { get; set; }
        public int game_played { get; set; }
    }
}
