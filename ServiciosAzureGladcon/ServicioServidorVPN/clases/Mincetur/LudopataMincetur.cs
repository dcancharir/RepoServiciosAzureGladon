using Newtonsoft.Json;

namespace ServicioServidorVPN.clases.Mincetur {
    public class LudopataMincetur {
        [JsonProperty("codRegistro")]
        private string _codigoRegistro { get; set; } = string.Empty;
        public string CodigoRegistro {
            get { return !string.IsNullOrEmpty(_codigoRegistro) ? _codigoRegistro.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("nombres")]
        private string _nombres { get; set; } = string.Empty;
        public string Nombres {
            get { return !string.IsNullOrEmpty(_nombres) ? _nombres.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("apePater")]
        private string _apellidoPaterno { get; set; } = string.Empty;
        public string ApellidoPaterno {
            get { return !string.IsNullOrEmpty(_apellidoPaterno) ? _apellidoPaterno.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("apeMater")]
        private string _apellidoMaterno { get; set; } = string.Empty;
        public string ApellidoMaterno {
            get { return !string.IsNullOrEmpty(_apellidoMaterno) ? _apellidoMaterno.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("idTipoDocu")]
        private int? _idTipoDocumento { get; set; }
        public int IdTipoDocumento {
            get { return ObtenerTipoDocumentoId(); }
        }

        [JsonProperty("desTipoDocu")]
        private string _tipoDocumento { get; set; } = string.Empty;
        public string TipoDocumento {
            get { return !string.IsNullOrEmpty(_tipoDocumento) ? _tipoDocumento.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("numDocu")]
        private string _numeroDocumento { get; set; } = string.Empty;
        public string NumeroDocumento {
            get { return !string.IsNullOrEmpty(_numeroDocumento) ? _numeroDocumento.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("nombresContacto")]
        private string _nombresContacto { get; set; } = string.Empty;
        public string NombresContacto {
            get { return !string.IsNullOrEmpty(_nombresContacto) ? _nombresContacto.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("apePaterContacto")]
        private string _apellidoPaternoContacto { get; set; } = string.Empty;
        public string ApellidoPaternoContacto {
            get { return !string.IsNullOrEmpty(_apellidoPaternoContacto) ? _apellidoPaternoContacto.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("apeMaterContacto")]
        private string _apellidoMaternoContacto { get; set; } = string.Empty;
        public string ApellidoMaternoContacto {
            get { return !string.IsNullOrEmpty(_apellidoMaternoContacto) ? _apellidoMaternoContacto.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("numeroTelfContacto")]
        private string _telefonoContacto { get; set; } = string.Empty;
        public string TelefonoContacto {
            get { return !string.IsNullOrEmpty(_telefonoContacto) ? _telefonoContacto.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("numeroTelf1Contacto")]
        private string _celularContacto { get; set; } = string.Empty;
        public string CelularContacto {
            get { return !string.IsNullOrEmpty(_celularContacto) ? _celularContacto.Trim().ToUpper() : string.Empty; }
        }

        [JsonProperty("fecPublicacion")]
        private string _fechaInscripcion { get; set; } = string.Empty;
        public string FechaInscripcion {
            get { return !string.IsNullOrEmpty(_fechaInscripcion) ? _fechaInscripcion.Trim().ToUpper() : string.Empty; }
        }

        public bool Existe() {
            return !string.IsNullOrEmpty(NumeroDocumento);
        }

        public int ObtenerTipoDocumentoId() {
            if(_idTipoDocumento == null) {
                return 0;
            }

            if(_idTipoDocumento == 1 || _idTipoDocumento == 2 || _idTipoDocumento == 3) {
                return (int)_idTipoDocumento;
            } else {
                return 3;
            }
        }
    }
}
