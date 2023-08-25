using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioGladconData.Jobs.MigracionPostgres;

namespace ServicioGladconData.Jobs
{
    public class MyScheduler
    {
        public async Task StartMigracionData()
        {
            //Configurar el planificador(Scheduler)
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();
            //Crear el trabajo(job)
            IJobDetail job = JobBuilder.Create<JobMigracionPostgres>().WithIdentity("JobMigracionPostgres", "GrupoMigracionPostgres").Build();
            //Crear el disparador(trigger)
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TriggerMigracionPostgres", "GrupoTriggerMigracionPostgres")
                //.WithSimpleSchedule(a => a.WithIntervalInMinutes(minutos).RepeatForever())
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
    }
}
