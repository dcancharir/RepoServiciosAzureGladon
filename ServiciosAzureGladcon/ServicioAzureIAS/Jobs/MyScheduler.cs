using System;
using System.Threading.Tasks;
using System.Configuration;
using Quartz;
using Quartz.Impl;

namespace ServicioAzureIAS.Jobs.EstadoServicioSala
{
    public class MyScheduler
    {
        public static string nombre = "CONTADORES ONLINE HORA SCHEDULER...";
        public static int intervaloSegundos = Convert.ToInt32(ConfigurationManager.AppSettings["IntervaloSegundosJobConsultaEstadoServicioSala"]);
        public async Task StartEstadoEnvioSalaJob() {
            //Configurar el planificador(Scheduler)
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            //Crear el trabajo(job)
            IJobDetail job = JobBuilder.Create<JobEstadoServicioSala>().WithIdentity("JobEstadoServicioSala", "GrupoEstadoServicioSala").Build();
            //Crear el disparador(trigger)
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TriggerEstadoServicioSala", "GrupoTriggerEstadoServicioSala")
                .StartNow()
                .WithSimpleSchedule(x =>
                    x.WithIntervalInSeconds(intervaloSegundos)//Invervalo en segundos
                    .RepeatForever()//Repetir Infinitamente
                ).Build();
            //Programar el trabajo con el disparador
            await scheduler.ScheduleJob(job, trigger);
            ////Esperar
            //await Task.Delay(TimeSpan.FromSeconds(1));
            ////Detener el planificador
            //await scheduler.Shutdown();
        }
    }
}
