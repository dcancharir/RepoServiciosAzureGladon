
namespace ServicioAzureIAS.Clases.Sala
{
    public class SalaEntidad
    {
        public int CodSala { get; set; }
        public int CodEmpresa { get; set; }
        public int CodUbigeo { get; set; }
        public string Nombre { get; set; }
        public string UrlProgresivo { get; set; }
        public string IpPublica { get; set; }
        public string UrlCuadre { get; set; }
        public string UrlPlayerTracking { get; set; }
        public string UrlBoveda { get; set; }
        public string UrlSalaOnline { get; set; }
        public int PuertoSignalr { get; set; }
        public string IpPrivada { get; set; }
        public int PuertoServicioWebOnline { get; set; }
        public int PuertoWebOnline { get; set; }
        public string CarpetaOnline { get; set; }
    }
}
