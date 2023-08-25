using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioGladconData.Clases
{
    public class consolidado_delete
    {
        public DateTime fecha { get; set; }
        public int id_sala_consolidado { get; set; }
        public int id_maquina { get; set; }
        public string juego { get; set; }
        public string cod_maquina { get; set; }
        public string serie { get; set; }
        public double coin_in { get; set; }
        public double net_win { get; set; }
        public double average_bet { get; set; }
        public int game_played { get; set; }
        public string isla { get; set; }
        public string zona { get; set; }
        public string tipo_maquina { get; set; }
        public DateTime fecha_ultimo_ingre { get; set; }
        public string marca_modelo { get; set; }
        public string posicion { get; set; }
    }
}
