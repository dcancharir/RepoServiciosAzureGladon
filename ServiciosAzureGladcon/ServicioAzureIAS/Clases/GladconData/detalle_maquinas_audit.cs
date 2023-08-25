using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Clases.GladconData
{
    public class detalle_maquinas_audit
    {
        public int detalle_maquinas_audit_id { get; set; }
        public int id_audit { get; set; }
        public DateTime fecha_hora { get; set; }
        public string marca_modelo { get; set; }
        public string cod_maquina { get; set; }
        public string serie { get; set; }
        public string modelo_comercial { get; set; }
        public string tipo_maquina { get; set; }
        public string progresivo { get; set; }
        public string juego { get; set; }
        public string propietario { get; set; }
        public string tipo_contrato { get; set; }
        public string tipo_sistema { get; set; }
        public string propiedad { get; set; }
        public string operacion { get; set; }
    }
}
