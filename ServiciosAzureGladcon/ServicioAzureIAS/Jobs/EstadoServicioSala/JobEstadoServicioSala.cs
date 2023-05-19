using Quartz;
using ServicioAzureIAS.utilitarios;
using System;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Jobs.EstadoServicioSala
{
    public class JobEstadoServicioSala:IJob
    {
        public static string mensajeLog = "CONSULTA ESTADO SERVICIOS EN SALA ....";
      

        public Task Execute(IJobExecutionContext context)
        {
            ConsultaServicioEnSala();
            return Task.CompletedTask;
        }
        public bool ConsultaServicioEnSala()
        {
            funciones.logueo("METODO CONSULTA SERVICIO EN SALA CORRIENDO" + DateTime.Now);
            return true;
        }
    }
}
