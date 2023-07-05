using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Clases.EstadoServicios
{
    public class EstadoServiciosEntidad
    {
        public int Id { get; set; }
        public int CodSala { get; set; }
        public bool EstadoWebOnline { get; set; }
        public bool EstadoGladconServices { get; set; }
        public DateTime FechaRegistro { get; set; }
    }

    public class ResponseEstadoServicios
    {
        public bool respuesta { get; set; }
        public string mensaje { get; set; }
        public dynamic data { get; set; }
    }

    //public class SalaEntidad
    //{
    //    public int CodSala { get; set; }
    //    public int CodEmpresa { get; set; }
    //    public int CodUbigeo { get; set; }
    //    public string Nombre { get; set; }
    //    public string UrlProgresivo { get; set; }
    //    public string IpPublica { get; set; }
    //    public string UrlSalaOnline { get; set; }
    //}

    public class NotificacionDispositivo
    {
        public int emd_id { get; set; }
        public string emd_imei { get; set; }
        public int emp_id { get; set; }
        public string id { get; set; }
        public int CargoID { get; set; }
        public int sala_id { get; set; }

    }

}
