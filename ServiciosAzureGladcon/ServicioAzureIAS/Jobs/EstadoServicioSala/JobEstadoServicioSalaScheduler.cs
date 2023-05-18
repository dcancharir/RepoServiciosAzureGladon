using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;
using System.Configuration;

namespace ServicioAzureIAS.Jobs.EstadoServicioSala
{
    public class JobEstadoServicioSalaScheduler
    {
        public static string nombre = "CONTADORES ONLINE HORA SCHEDULER...";
        public static int intervaloSegundos = Convert.ToInt32(ConfigurationManager.AppSettings["IntervaloSegundosJobConsultaEstadoServicioSala"]); 
        public static void Start()
        {
            var scheduler_contadores_hora = ServicioAzureIAS.scheduler_consulta_estado_servicio;
            if (scheduler_contadores_hora != null)
            {
                scheduler_contadores_hora.Shutdown();
                funciones.logueo(nombre + " Iniciando Scheduler Contadores Online Hora");
            }
            NameValueCollection properties = new NameValueCollection
            {
                [StdSchedulerFactory.PropertySchedulerInstanceName] = "SchedulerConsulaEstadoServicio"
            };
            scheduler_contadores_hora = new StdSchedulerFactory(properties).GetScheduler();//StdSchedulerFactory.GetDefaultScheduler();
            scheduler_contadores_hora.Start();

            /////////////////INICIO JOBS PARA ACTUALIZACIÒN TICKETS
            IJobDetail job;
            ITrigger trigger;
            try
            {
                job = JobBuilder.Create<EstadoServicioSalaJobs>()
                      .WithIdentity("ConsultaEstadoServicio_JOB", "grupo_consultaestadoservicio")
                      .Build();
                trigger = TriggerBuilder.Create()
                   .StartNow()
                   .WithSimpleSchedule(a => a.WithIntervalInSeconds(intervaloSegundos).RepeatForever())
                   .ForJob(job)
                   .Build();

                //fin trigger
                scheduler_contadores_hora.ScheduleJob(job, trigger);
                var aver = scheduler_contadores_hora.GetCurrentlyExecutingJobs();
            }
            catch (Exception ex)
            {
                funciones.logueo(" - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString(), "Error");
            }





        }
    }
}
