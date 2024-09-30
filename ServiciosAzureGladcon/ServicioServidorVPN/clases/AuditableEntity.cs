using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.clases
{
    public class AuditableEntity
    {
        public virtual string UsuarioCreacion { get; set; }
        public virtual DateTime FechaRegistro { get; set; }
        public virtual string UsuarioModificacion { get; set; }
        public virtual DateTime FechaModificacion { get; set; }
    }
}
