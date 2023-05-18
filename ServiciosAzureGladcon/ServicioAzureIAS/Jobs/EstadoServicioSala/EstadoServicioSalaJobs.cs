using Quartz;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Jobs.EstadoServicioSala
{
    public class EstadoServicioSalaJobs:IJob
    {
        public static string mensajeLog = "CONSULTA ESTADO SERVICIOS EN SALA ....";

        public bool ConsultaServicioEnSala()
        {
            funciones.logueo("METODO CONSULTA SERVICIO EN SALA CORRIENDO");
            return true;
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                string nombrejob = context.JobDetail.Key.ToString();
                string solonombrejob = context.JobDetail.Key.Name.ToString();
                var horatrigger = (context.FireTimeUtc.Value.ToLocalTime()).ToString("hh:mm:ss tt");
                var horaplanificada = (context.ScheduledFireTimeUtc.Value.ToLocalTime()).ToString("hh:mm:ss tt");
                var siguientehorario = (context.NextFireTimeUtc.Value.ToLocalTime()).ToString("hh:mm:ss tt");
                funciones.logueo(mensajeLog + " " + solonombrejob + " Horario:" + horaplanificada + " EJECUTANDO " + "-ejecución:" + horatrigger, "Color");
                DateTime fecha_hora = context.FireTimeUtc.Value.ToLocalTime().DateTime;
                //Ejecutar metodo
                ConsultaServicioEnSala();
                //Termino de ejecucion

                funciones.logueo(mensajeLog + " " + solonombrejob + " Horario=" + horaplanificada + " FINALIZADO ****** ", "Color");
            }
            catch (Exception ex)
            {
                funciones.logueo(ErrorLinea.Linea(ex) + " :" + ex.Message, "Error");
            }
        }
    }
}
