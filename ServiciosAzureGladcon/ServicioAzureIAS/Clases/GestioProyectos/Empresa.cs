using System.Collections.Generic;

namespace ServicioAzureIAS.Clases.GestioProyectos {
    public class Empresa {
        public long EmpresaId { get; set; }
        public long CO_EMPR { get; set; }
        public string DE_NOMB { get; set; }
        public long ID_BUK { get; set; }
        public string NU_RUCS { get; set; }
        public string DE_DIRE { get; set; }

        public bool isDifferentEmpresaAPi(EmpresaApi empresa) {
            return this.CO_EMPR != empresa.CO_EMPR ||
                   Normalizar(this.DE_NOMB) != Normalizar(empresa.DE_NOMB) ||
                   Normalizar(this.NU_RUCS) != Normalizar(empresa.NU_RUCS) ||
                   Normalizar(this.DE_DIRE) != Normalizar(empresa.DE_DIRE);
        }

        public bool isDifferentEmpresa(Empresa empresa) {
            return this.CO_EMPR != empresa.CO_EMPR ||
                   Normalizar(this.DE_NOMB) != Normalizar(empresa.DE_NOMB) ||
                   Normalizar(this.NU_RUCS) != Normalizar(empresa.NU_RUCS) ||
                   Normalizar(this.DE_DIRE) != Normalizar(empresa.DE_DIRE);
        }

        private string Normalizar(string valor) {
            return string.IsNullOrWhiteSpace(valor) ? "" : valor.Trim().ToLower();
        }

        public override string ToString() {

            return $"Empresa ID: {EmpresaId}\n" +
                                $"CO_EMPR: {CO_EMPR}\n" +
                                $"DE_NOMB: {DE_NOMB}\n" +
                                $"NU_RUCS: {NU_RUCS}\n" +
                                $"DE_DIRE: {DE_DIRE}\n"
                               ;

        }
    }

    public class EmpresaResponse {
        public bool Success { get; set; }
        public List<EmpresaApi> data { get; set; }
    }

    public class EmpresaApi {

        public long ID_BUK { get; set; }
        public long CO_EMPR { get; set; }
        public string DE_NOMB { get; set; }
        public string NU_RUCS { get; set; }
        public string DE_DIRE { get; set; }
    }
}
