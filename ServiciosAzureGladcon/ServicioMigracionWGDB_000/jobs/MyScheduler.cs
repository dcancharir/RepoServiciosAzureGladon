using Quartz;
using Quartz.Impl;
using ServicioMigracionWGDB_000.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.jobs
{
    public class MyScheduler
    {
        public async Task StartMigrationData()
        {
            //DateTime hora = DateTime.Now;
            string horaEjecucionJob = string.Empty;
            try
            {
                horaEjecucionJob = ConfigurationManager.AppSettings["HoraMigracionWGDB_000"];
            }
            catch
            {
                horaEjecucionJob = "07:00";
            }

            var hora = Convert.ToDateTime(horaEjecucionJob);
            funciones.logueo(hora.ToString());
            //Configurar el planificador(Scheduler)
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            //Crear el trabajo(job)
            IJobDetail job = JobBuilder.Create<JobMigracionData>().WithIdentity("JobMigracionData", "GrupoMigracionData").Build();
            //Crear el disparador(trigger)
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TriggerMigracionData", "GrupoTriggerMigracionData")
                .StartNow()
                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                .ForJob(job)
                .Build();
            //Programar el trabajo con el disparador
            await scheduler.ScheduleJob(job, trigger);
        }
        
    }
}
