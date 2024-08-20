using System;

namespace ServicioAzureIAS.Clases.Email {
    public class Destinatario {
        public Int32 EmailID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }
    }
}
