using Quartz;
using Quartz.Impl;
using ServicioServidorVPN.utilitarios;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ServicioServidorVPN.Jobs.Ludopatas {
    public class SincronizacionLudopatasMinceturScheduler {
        public async Task Start() {
            string identity = "sincronizar_ludopatas_mincetur";
            string horaStr = ConfigurationManager.AppSettings["HoraSincronizacionLudopatas"];
            DateTime hora = !string.IsNullOrEmpty(horaStr) ? Convert.ToDateTime(horaStr) : DateTime.Now.AddMinutes(1);

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<SincronizacionLudopatasMinceturJob>()
                .WithIdentity($"JOB_{identity}", $"GROUP_JOB_{identity}")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"TRIGGER_{identity}", $"GROUP_TRIGGER_{identity}")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                .ForJob(jobDetail)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
            funciones.logueo($"JOB de actualización de ludópatas iniciado a las {DateTime.Now:dd/MM/yyyy HH:mm:ss}, se ejecutará todos los días a las {hora:HH:mm}.");
        }
    }
}
