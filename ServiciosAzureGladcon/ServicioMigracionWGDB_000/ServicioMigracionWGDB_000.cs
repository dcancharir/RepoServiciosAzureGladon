using ServicioMigracionWGDB_000.jobs;
using ServicioMigracionWGDB_000.utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionWGDB_000
{
    partial class ServicioMigracionWGDB_000 : ServiceBase
    {
        public static string nombreservicio = "ServicioMigracionWGDB_000";
        public ServicioMigracionWGDB_000()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: agregar código aquí para iniciar el servicio.
            funciones.logueo("el servicio se ha iniciado");
            Task.Run(async () =>
            {
                funciones.logueo("jobs iniciados");
                MyScheduler schedulerClass = new MyScheduler();
                await schedulerClass.StartMigrationData();
            });
        }

        protected override void OnStop()
        {
            // TODO: agregar código aquí para realizar cualquier anulación necesaria para detener el servicio.
        }
        internal void Start()
        {
            OnStart(null);
        }
    }
}
