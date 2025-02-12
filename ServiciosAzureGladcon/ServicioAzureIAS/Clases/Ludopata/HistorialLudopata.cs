using System;

namespace ServicioAzureIAS.Clases.Ludopata {
    public class HistorialLudopata {
        public int Id { get; set; }
        public int IdLudopata { get; set; }
        public TipomovimientoHistorialLudopata TipoMovimiento { get; set; }
        public TipoRegistroHistorialLudopata TipoRegistro { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    public enum TipomovimientoHistorialLudopata {
        Entra = 1,
        Sale = 2
    }

    public enum TipoRegistroHistorialLudopata {
        Automatico = 1,
        Manual = 2
    }
}
