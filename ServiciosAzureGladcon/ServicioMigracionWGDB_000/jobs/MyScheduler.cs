using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000.jobs
{
    public class MyScheduler
    {
        public async Task StartMigrationData()
        {
            //Configurar el planificador(Scheduler)
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            //Crear el trabajo(job)
            IJobDetail job = JobBuilder.Create<JobMigracionData>().WithIdentity("JobMigracionData", "GrupoMigracionData").Build();
            //Crear el disparador(trigger)
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TriggerMigracionData", "GrupoTriggerMigracionData")
                .StartNow()
                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(horar.Hour, horar.Minute))
                .ForJob(job)
                .Build();
            //Programar el trabajo con el disparador
            await scheduler.ScheduleJob(job, trigger);
        }
        
    }
}
