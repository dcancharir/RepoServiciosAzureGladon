using Quartz;
using Quartz.Impl;
using Quartz.Util;
using ServicioServidorVPN.utilitarios;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ServicioServidorVPN.Jobs.Clientes {
    public class MigracionIngresoClientesSalaScheduler {
        public async Task Start() {
            string identity = "migracion_ingreso_clientes_sala";
            string intervaloJobStr = ConfigurationManager.AppSettings["IntervaloEjecucionJobIngresoClienteSala"];
            if(!int.TryParse(intervaloJobStr, out int intervaloJob)) {
                intervaloJob = 10;
            }

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<MigracionIngresoClientesSalaJob>()
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
            funciones.logueo($"JOB de migración de ingresos de clientes a sala iniciado a las {DateTime.Now:dd/MM/yyyy HH:mm:ss}, se ejecutará este momento con un intervalo de {intervaloJob} minutos.");
        }
    }
}
