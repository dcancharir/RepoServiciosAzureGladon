using Quartz.Impl;
using Quartz;
using System.Threading.Tasks;
using ServicioAzureIAS.Jobs.RegistroProgresivo;

namespace ServicioAzureIAS.Schedulers
{
    public class RegistroProgresivoScheduler
    {
        public async Task Start_RP_LimpiarHistorialJob()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<RP_LimpiarHistorialJob>().WithIdentity("JOB_RP_Limpiar_Historial", "GROUP_Registro_Progresivo").Build();
            
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TRIGGER_RP_Limpiar_Historial", "GROUP_Registro_Progresivo")
                .WithCronSchedule("0 0 * * *")
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
