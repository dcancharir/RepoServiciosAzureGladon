using Quartz;
using Quartz.Impl;
using ServicioServidorVPN.utilitarios;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ServicioServidorVPN.Jobs.ClientesControlAcceso {
    public class MigracionClientesControlAccesoScheduler {
        public async Task Start() {
            string identity = "migracion_clientes_control_acceso";
            string intervaloJobStr = ConfigurationManager.AppSettings["IntervaloEjecucionJobClientesControlAcceso"];
            if(!int.TryParse(intervaloJobStr, out int intervaloJob)) {
                intervaloJob = 10;
            }

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<MigracionClientesControlAccesoJob>()
                .WithIdentity($"JOB_{identity}", $"GROUP_JOB_{identity}")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"TRIGGER_{identity}", $"GROUP_TRIGGER_{identity}")
                .ForJob(jobDetail)
                .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(intervaloJob)
                    .RepeatForever().Build())
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
            funciones.logueo($"JOB de migración de clientes de control de acceso iniciado a las {DateTime.Now:dd/MM/yyyy HH:mm:ss}, se ejecutará este momento con un intervalo de {intervaloJob} minutos.");
        }
    }
}
