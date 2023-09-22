using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ServicioGladconData.Jobs;
using ServicioGladconData.utilitarios;

namespace ServicioGladconData
{
    public partial class ServicioGladconData : ServiceBase
    {
        public static string nombreservicio = "ServicioGladconData";
        public ServicioGladconData()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            funciones.logueo("El servicio esta iniciando los jobs");
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
