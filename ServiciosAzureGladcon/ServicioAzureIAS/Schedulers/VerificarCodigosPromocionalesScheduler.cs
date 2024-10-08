﻿using Quartz;
using Quartz.Impl;
using ServicioAzureIAS.Jobs.CampaniaCliente;
using ServicioAzureIAS.utilitarios;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Schedulers {
    public class VerificarCodigosPromocionalesScheduler {

        public async Task Start_VerificacionCodigos() {
            string identity = "verificar_codigos_promocionales";
            string horaStr = ConfigurationManager.AppSettings["HoraVerificarCodigos"];
            DateTime hora = !string.IsNullOrEmpty(horaStr) ? Convert.ToDateTime(horaStr) : DateTime.Now.AddMinutes(1);

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<VerificarCodigosPromocionales>()
                .WithIdentity($"JOB_{identity}", $"GROUP_JOB_{identity}")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"TRIGGER_{identity}", $"GROUP_TRIGGER_{identity}")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                .ForJob(jobDetail)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
            funciones.logueo($"JOB de revision de codigos promocionales iniciado a las {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}, se ejecutara todos los dias a las {hora.ToString("HH:mm")}.");
        }
    }
}
