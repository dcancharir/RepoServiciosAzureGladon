namespace ServicioAzureIAS.Clases.Mincetur {
    public class CredencialMincetur {
        public int idCredencialMincetur { get; set; }
        public int codEmpresa { get; set; }
        public string token { get; set; } = string.Empty;
        public bool estado { get; set; }
    }
}
