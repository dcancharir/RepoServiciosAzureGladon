using Quartz.Impl;
using Quartz;
using System.Threading.Tasks;
using ServicioAzureIAS.Jobs.RegistroProgresivo;
using System.Configuration;
using System;

namespace ServicioAzureIAS.Schedulers
{
    public class RegistroProgresivoScheduler
    {
        public async Task Start_RP_LimpiarHistorialJob()
        {
            string horaLimpiarHistorial = string.Empty;
            try
            {
                horaLimpiarHistorial = ConfigurationManager.AppSettings["HoraLimpiarHistorial"];
            }
            catch
            {
                horaLimpiarHistorial = "01:00";
            }

            var hora = Convert.ToDateTime(horaLimpiarHistorial);

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<RP_LimpiarHistorialJob>().WithIdentity("JOB_RP_Limpiar_Historial", "GROUP_Registro_Progresivo").Build();
            
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TRIGGER_RP_Limpiar_Historial", "GROUP_Registro_Progresivo")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                .ForJob(jobDetail)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
