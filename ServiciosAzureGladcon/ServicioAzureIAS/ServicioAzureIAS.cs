using Quartz;
using ServicioAzureIAS.Jobs.EstadoServicioSala;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServicioAzureIAS
{
    public partial class ServicioAzureIAS : ServiceBase
    {
        public static string nombreservicio = "ServicioAzureIAS";
        public static IScheduler scheduler_consulta_estado_servicio;
        public ServicioAzureIAS()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            funciones.logueo("El servicio se ha iniciado");
            var subprocesoContadoresOnlineHora = new Thread(new ThreadStart(IniciarConsultaEstadoServicioSala));
            subprocesoContadoresOnlineHora.Start();
        }
        protected override void OnStop()
        {
        }
        internal void Start()
        {
            OnStart(null);
        }
        public static void IniciarConsultaEstadoServicioSala()
        {
            JobEstadoServicioSalaScheduler.Start();
        }
    }
}
