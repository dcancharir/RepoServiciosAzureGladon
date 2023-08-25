using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Clases.GladconData
{
    public class maquina
    {
        public int id_maquina { get; set; }
        public DateTime fecha_ultimo_ingreso { get; set; }
        public string marca { get; set; }
        public string cod_maquina { get; set; }
        public string serie { get; set; }
        public string marca_modelo { get; set; }
        public string isla { get; set; }
        public string zona { get; set; }
        public string tipo_maquina { get; set; }
        public int id_sala { get; set; }
        public string juego { get; set; }
        public string estado_maquina { get; set; }
        public string posicion { get; set; }

    }
}
