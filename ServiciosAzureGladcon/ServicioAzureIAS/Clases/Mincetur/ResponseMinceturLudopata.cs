using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ServicioAzureIAS.Clases.Mincetur {
    public class ResponseMinceturLudopata {
        [JsonProperty("fecSincronizacion")]
        private string _fechaSincronizacion { get; set; } = string.Empty;
        public DateTime FechaSincronizacion {
            get {
                if(DateTime.TryParseExact(_fechaSincronizacion, "dd/MM/yyyy HH:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture,
                                           System.Globalization.DateTimeStyles.None,
                                           out DateTime fecha)) {
                    return fecha;
                } else {
                    return DateTime.MinValue;
                }
            }
        }

        [JsonProperty("IdTipo")]
        private int? _idTipo { get; set; }
        public int TipoId {
            get { return _idTipo ?? 0; }
        }

        [JsonProperty("IdError")]
        private int? _errorId { get; set; }
        public int ErrorId {
            get { return _errorId ?? 0; }
        }

        [JsonProperty("DesError")]
        private string _descripcionError { get; set; } = string.Empty;
        public string DescripcionError {
            get { return _descripcionError ?? string.Empty; }
        }

        [JsonProperty("Valor")]
        private string _valor { get; set; } = string.Empty;
        public string ValorRespuesta {
            get { return _valor ?? string.Empty; }
        }

        [JsonProperty("mensaje")]
        private string _mensaje { get; set; } = string.Empty;
        public string Mensaje {
            get { return _mensaje ?? string.Empty; }
        }

        [JsonProperty("lstEnLudopatia")]
        private List<LudopataMincetur> _ludopatas { get; set; } = new List<LudopataMincetur>();
        public List<LudopataMincetur> Ludopatas {
            get { return _ludopatas ?? new List<LudopataMincetur>(); }
        }

        public bool TieneLudopatas() {
            return Ludopatas.Count > 0;
        }
    }
}
