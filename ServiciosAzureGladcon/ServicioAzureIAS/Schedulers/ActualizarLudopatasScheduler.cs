﻿using Quartz;
using Quartz.Impl;
using ServicioAzureIAS.Jobs.ControlAccesoLudopata;
using ServicioAzureIAS.utilitarios;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Schedulers {
    public class ActualizarLudopatasScheduler {
        public async Task Start_ActualizarLudopatas() {
            string identity = "actualizar_ludopatas";
            string horaStr = ConfigurationManager.AppSettings["HoraActualizarLudopatas"];
            DateTime hora = !string.IsNullOrEmpty(horaStr) ? Convert.ToDateTime(horaStr) : DateTime.Now.AddMinutes(1);

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<SincronizarLudopatasMincetur>()
                .WithIdentity($"JOB_{identity}", $"GROUP_JOB_{identity}")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"TRIGGER_{identity}", $"GROUP_TRIGGER_{identity}")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(hora.Hour, hora.Minute))
                .ForJob(jobDetail)
                .StartNow()
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
            funciones.logueo($"JOB de actualizacion de ludopatas iniciado a las {DateTime.Now:dd/MM/yyyy HH:mm:ss}, se ejecutará todos los dias a las {hora:HH:mm}.");
        }
    }
}