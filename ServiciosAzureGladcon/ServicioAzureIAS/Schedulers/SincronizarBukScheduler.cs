using Quartz.Impl;
using Quartz;
using ServicioAzureIAS.Jobs.BUK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ServicioAzureIAS.Schedulers {
    public class SincronizarBukScheduler {
        public async Task Start_SincronizarBuk() {

            string horaLimpiarHistorial = string.Empty;
            //horaLimpiarHistorial = "11:44";
            try {
                horaLimpiarHistorial = ConfigurationManager.AppSettings["HoraMigracionEmpleadosBuk"];
            }
            catch {
                horaLimpiarHistorial = "08:00";
            }

            var hora = Convert.ToDateTime(horaLimpiarHistorial);

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<JobSincronizarBuk>().WithIdentity("JOB_SincronizarBuk", "GROUP_SincronizarBuk").Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TRIGGER_SincronizarBuk", "GROUP_SincronizarBuk")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                .ForJob(jobDetail)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
