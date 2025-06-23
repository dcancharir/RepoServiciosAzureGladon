using Quartz.Impl;
using Quartz;
using ServicioAzureIAS.Jobs.BUK;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicioAzureIAS.Jobs.GestionProyectos;
using ServicioAzureIAS.utilitarios;

namespace ServicioAzureIAS.Schedulers {
    public class SincronizarInformacionBukGestorProyectosScheduler {

        public async Task Start_SincronizarInformacionBukGestorProyectos() {
            string identity = "SincronizarInformacionBukGestorProyectos";
            string horaStr = ConfigurationManager.AppSettings["HoraSincronizarInfoBukGestorPoryectos"];
            DateTime hora = !string.IsNullOrEmpty(horaStr) ? Convert.ToDateTime(horaStr) : DateTime.Now.AddMinutes(1);

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<SincronizarInformacionBukGestorProyectos>()
                .WithIdentity($"JOB_{identity}", $"GROUP_JOB_{identity}")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"TRIGGER_{identity}", $"GROUP_TRIGGER_{identity}")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                .ForJob(jobDetail)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
            funciones.logueo($"JOB de sincronizacion de informacion de gestor de proyectos  iniciado a las {DateTime.Now:dd/MM/yyyy HH:mm:ss}, se ejecutará todos los dias a las {hora:HH:mm}.");
        }
    }
}
