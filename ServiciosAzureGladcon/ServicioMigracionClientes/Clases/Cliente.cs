using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes.Clases
{
    public class Cliente
    {
        public int Id { get; set; }
        public int IdIas { get; set; }
        public string NroDoc { get; set; }
        public string Nombre { get; set; }
        public string ApelPat { get; set; }
        public string ApelMat { get; set; }
        public string Celular1 { get; set; }
        public string Celular2 { get; set; }
        public string Mail { get; set; }
        public DateTime FechaNacimiento { get; set; }
        //Relaciones
        public DateTime FechaRegistro { get; set; }
        public string NombreCompleto { get; set; }
        public string NombreUbigeo { get; set; }
        public string NombreTipoDocumento { get; set; }
        public string NombreGenero { get; set; }
        public string NombreSala { get; set; }
    }
}
