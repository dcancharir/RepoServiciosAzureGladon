using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace ServicioMigracionClientes.Jobs.MigracionData
{
    public class MyScheduler
    {
        public static string nombre = "MIGRACION DATA SCHEDULER...";
        public async Task StartMigracionData()
        {
            int minutos = ObtenerMinutos();
            //Configurar el planificador(Scheduler)
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            //Crear el trabajo(job)
            IJobDetail job = JobBuilder.Create<JobMigracionData>().WithIdentity("JobMigracionData", "GrupoMigracionData").Build();
            //Crear el disparador(trigger)
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TriggerMigracionData", "GrupoTriggerMigracionData")
                .WithSimpleSchedule(a=>a.WithIntervalInMinutes(minutos).RepeatForever())
                .StartNow()
                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(horar.Hour, horar.Minute))
                .ForJob(job)
                .Build();
            //Programar el trabajo con el disparador
            await scheduler.ScheduleJob(job, trigger);
            ////Esperar
            //await Task.Delay(TimeSpan.FromSeconds(1));
            ////Detener el planificador
            //await scheduler.Shutdown();
        }
        public async Task StartMigracionSesiones()
        {
            int minutos = ObtenerMinutos();
            //var horar = Convert.ToDateTime(hora_activacion);
            //Configurar el planificador(Scheduler)
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            //Crear el trabajo(job)
            IJobDetail job = JobBuilder.Create<JobMigracionSesiones>().WithIdentity("JobMigracionSesiones", "GrupoMigracionSesiones").Build();
            //Crear el disparador(trigger)
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TriggerMigracionSesiones", "GrupoTriggerMigracionSesiones")
                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(horar.Hour, horar.Minute))
                .WithSimpleSchedule(a => a.WithIntervalInMinutes(minutos).RepeatForever())
                .StartNow()
                .ForJob(job)
                .Build();
            //Programar el trabajo con el disparador
            await scheduler.ScheduleJob(job, trigger);
            ////Esperar
            //await Task.Delay(TimeSpan.FromSeconds(1));
            ////Detener el planificador
            //await scheduler.Shutdown();
        }
        public int ObtenerMinutos()
        {
            try
            {
                int minutos= Convert.ToInt32(ConfigurationManager.AppSettings["RangoMinutosActivacionJob"]);
                if (minutos <= 0)
                {
                    minutos = 60;
                }
                return minutos;
            }
            catch (Exception)
            {
                return 60;
            }
        }
    }
}
