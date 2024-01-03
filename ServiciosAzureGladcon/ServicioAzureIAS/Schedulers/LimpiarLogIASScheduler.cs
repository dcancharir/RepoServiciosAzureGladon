using Quartz.Impl;
using Quartz;
using ServicioAzureIAS.Jobs.RegistroProgresivo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioAzureIAS.Jobs.LimpiarLogIAS;

namespace ServicioAzureIAS.Schedulers {
    public class LimpiarLogIASScheduler {

        public async Task Start_LimpiarLogIAS() {

            string intervalo = string.Empty;
            try {
                intervalo = ConfigurationManager.AppSettings["IntervaloLimpiarLogIAS"];
            } catch {
                intervalo = "72";
            }

            int intervaloNumero = Convert.ToInt32(intervalo);
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<LimpiarLogSeguridadPJ>().WithIdentity("JobLimpiarLogIAS", "GrupoLimpiarLogIAS").Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TRIGGER_limpiar_log", "GROUP_trigger_LimpiarLogIAS")
                .WithSimpleSchedule(x => x
                .WithIntervalInHours(intervaloNumero)  // Ejecutar cada 3 días(72 horas)
                .RepeatForever())
                .ForJob(jobDetail)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
