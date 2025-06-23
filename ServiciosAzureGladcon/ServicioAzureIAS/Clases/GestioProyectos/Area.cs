using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Clases.GestioProyectos {
    public class Area {
        
        public long IdArea { get; set; }
        public long IdAreaBuk { get; set; }
        public string area { get; set; }
        public bool estado { get; set; }

        public bool IsDifferentAreaApi(Area api) {
            return this.IdAreaBuk != api.IdAreaBuk ||
                   Normalizar(this.area) != Normalizar(api.area);
        }

        private string Normalizar(string valor) {
            return string.IsNullOrWhiteSpace(valor) ? "" : valor.Trim();
        }

        public override string ToString() {
            return $"area Id: {IdArea}\n" + $"IdAreaBuk: {IdAreaBuk}\n" +

                               $"Area: {area}\n";
                              
        }
    }
}
    