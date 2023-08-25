using Quartz;
using ServicioMigracionClientes.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes.Jobs.MigracionData
{
    public class JobMigracionGladonData
    {
        public static string mensajeLog = "Job para migracion de info desde IAS";
        private readonly string URL_IAS = ConfigurationManager.AppSettings["UrlIASAzure"];
        private ClienteDAL clienteDAL = new ClienteDAL();
        public Task Execute(IJobExecutionContext context)
        {
            MigrarData();
            return Task.CompletedTask;
        }
        private void MigrarData()
        {

        }
    }
}
