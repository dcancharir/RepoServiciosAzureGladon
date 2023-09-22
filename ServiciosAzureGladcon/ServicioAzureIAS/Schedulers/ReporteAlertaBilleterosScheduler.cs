using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioAzureIAS.Jobs.ReporteAlertaBilletero;

namespace ServicioAzureIAS.Schedulers
{
    public class ReporteAlertaBilleterosScheduler
    {
        public async Task Start_RP_ReporteAlertaBilleteros()
        {
            string horaReporteAlertaBilletero = string.Empty;
            try
            {
                horaReporteAlertaBilletero = ConfigurationManager.AppSettings["HoraReporteBilletros"];
            }
            catch
            {
                horaReporteAlertaBilletero = "05:30";
            }

            var hora = Convert.ToDateTime(horaReporteAlertaBilletero);

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<JobReporteAlertaBilletero>().WithIdentity("JobReporteBilleteros", "GrupoReporteBilleteros").Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TriggerReporteBilleteros", "GrupoTriggerReporteBilleteros")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                .ForJob(jobDetail)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
