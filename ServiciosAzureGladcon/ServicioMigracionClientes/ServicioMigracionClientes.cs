using ServicioMigracionClientes.Jobs.MigracionData;
using ServicioMigracionClientes.utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes
{
    public partial class ServicioMigracionClientes : ServiceBase
    {
        public static string nombreservicio = "SevicioMigracionClientes";
        public ServicioMigracionClientes()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            funciones.logueo("El servicio se ha iniciado");
            Task.Run(async () =>
            {
                MyScheduler schedulerClass = new MyScheduler();
                await schedulerClass.StartMigracionData();
            });
            funciones.logueo("Jobs iniciados");
        }

        protected override void OnStop()
        {
        }
        internal void Start()
        {
            OnStart(null);
        }
    }
}
