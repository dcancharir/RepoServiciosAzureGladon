using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes.Clases
{
    public class CMP_SesionClienteMigracion
    {
        public long Id { get; set; }

        public string NroDocumento { get; set; }

        public string NombreTipoDocumento { get; set; }

        public int TipoDocumentoId { get; set; }

        public int CantidadSesiones { get; set; }

        public string NombreCliente { get; set; }

        public string Mail { get; set; }

        public DateTime PrimeraSesion { get; set; }
    }
}
