using ServicioAzureIAS.Jobs.EstadoServicioSala;
using ServicioAzureIAS.Schedulers;
using ServicioAzureIAS.utilitarios;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServicioAzureIAS
{
    public partial class ServicioAzureIAS : ServiceBase
    {
        public static string nombreservicio = "ServicioAzureIAS";
        public ServicioAzureIAS()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            funciones.logueo("El servicio se ha iniciado");

            Task.Run(async () =>
            {
                MyScheduler schedulerClass = new MyScheduler();
                await schedulerClass.StartEstadoEnvioSalaJob();

                await new RegistroProgresivoScheduler().Start_RP_LimpiarHistorialJob();
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
