using System;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

namespace ServicioAzureIAS.Clases.Ludopata {
    public class Ludopata {
        public int LudopataID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public DateTime FechaInscripcion { get; set; }
        public int TipoExclusion { get; set; }
        public string DNI { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public int ContactoID { get; set; }
        public string Telefono { get; set; } = string.Empty;
        public string CodRegistro { get; set; } = string.Empty;
        public bool Estado { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public int TipoDoiID { get; set; }
        public int CodUbigeo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaEnvioCorreo { get; set; }
        public int UsuarioRegistro { get; set; }

        
        private string NombreCompleto {
            get {
                string nombre = string.IsNullOrEmpty(Nombre) ? string.Empty : Nombre;
                string apellidoPaterno = string.IsNullOrEmpty(ApellidoPaterno) ? string.Empty : ApellidoPaterno;
                string apellidoMaterno = string.IsNullOrEmpty(ApellidoMaterno) ? string.Empty : ApellidoMaterno;

                if(string.IsNullOrEmpty(apellidoPaterno)) {
                    return $"{nombre} {apellidoMaterno}".Trim();
                }

                return $"{nombre} {apellidoPaterno} {apellidoMaterno}".Trim();
            }
        }

        public ContactoLudopata Contacto { get; set; } = new ContactoLudopata();

        public bool Existe() {
            return LudopataID > 0;
        }

        public bool TieneContacto() {
            return Contacto.ContactoID > 0;
        }

        public bool EstaActivo() {
            return Estado;
        }

        public string CrearFilaParaTabla(int index) {
            return $@"
                <tr>
                    <td style='border: 1px solid black; text-align:center;'>{index}</td>
                    <td style='border: 1px solid black; text-align:center;'>{DNI}</td>
                    <td style='border: 1px solid black; padding-left:8px;'>{NombreCompleto}</td>
                </tr>
            ";
        }
    }
}
