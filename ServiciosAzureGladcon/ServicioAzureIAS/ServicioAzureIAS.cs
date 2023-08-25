using ServicioAzureIAS.Jobs.EstadoServicioSala;
using ServicioAzureIAS.Schedulers;
using ServicioAzureIAS.utilitarios;
using System;
using System.Configuration;
using System.Net.Sockets;
using System.Net;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http.Cors;
using System.Web.Http;
using System.Timers;

namespace ServicioAzureIAS
{
    public partial class ServicioAzureIAS : ServiceBase
    {


        public static string nombreservicio = "ServicioAzureIAS";
        public static string Sala = "";

        public static int codSala = 0;
        public static string nombreSala = string.Empty;
        public static string Empresa = "";
        public static string conectionOnline = "";
        public static string conection = "";

        public static string pozosparametros = "/api/home/getpozos?flag=true";
        //public static string ganadoresparametros = "/api/PremiosProgresivo/Obtener_Premios_Progresivo_Dia?maquina=''&TipoPozo=-1&cantidad=400&";
        public static string ganadoresparametros = "/api/PremiosProgresivo/Obtener_Premios_Progresivo_Dia?maquina=''&TipoPozo=-1&cantidad=40&";


        public static string puertoserviciowindows = "";
        public static string urlserviciowindows = "";
        public static string iplocal = "";
        public static int intervaloreconexion = 10000;


        public static string correo = "";
        public static string password = "";

        public static string urlias = "";
        public static string urlApiERP = "";
        public static int urlenvio = 0;
        public static System.Timers.Timer temporizadorServer = new System.Timers.Timer();

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



            //temporizador Servers
            temporizadorServer.Elapsed += new ElapsedEventHandler(timerfuncionServer);
            temporizadorServer.Interval = intervaloreconexion;
            temporizadorServer.Enabled = true;
            temporizadorServer.Stop();

            IniciarServidor();

            funciones.logueo("Jobs iniciados");
        }
        protected override void OnStop()
        {
        }
        internal void Start()
        {
            OnStart(null);
        }
        public static string GetLocalIPAddress()
        {
            string ipad = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipad = ipad + " " + ip.ToString();
                }
            }
            return ipad;
            throw new Exception("Local IP Address Not Found!");
        }
        public void declararvariables()
        {
            puertoserviciowindows = ConfigurationManager.AppSettings["puertoserviciowindows"];
            //conectionOnline = ConfigurationManager.ConnectionStrings["conectionOnline"].ConnectionString;
            //conection = ConfigurationManager.ConnectionStrings["conection"].ConnectionString;

            //correo = ConfigurationManager.AppSettings["correo"];
            //password = ConfigurationManager.AppSettings["password"];

            iplocal = GetLocalIPAddress();
            urlserviciowindows = "http://+:" + puertoserviciowindows;
            //funciones.logueo("")

            //urlias = ConfigurationManager.AppSettings["urlIAS"];
            //urlenvio = Convert.ToInt32(ConfigurationManager.AppSettings["urlIASEnvio"]);
            //urlApiERP = ConfigurationManager.AppSettings["urlApiERP"];
        }
        public void IniciarServidor()
        {
            declararvariables();
            var config = new HttpSelfHostConfiguration(urlserviciowindows);
            HttpSelfHostServer server = null;

            try
            {
                var cors = new EnableCorsAttribute("*", "*", "*");
                config.MaxReceivedMessageSize = 2147483647; // use config for this value
                config.EnableCors(cors);
                config.Routes.MapHttpRoute(
                   name: "API",
                   routeTemplate: "{controller}/{action}/{id}/{id2}/{id3}/{id4}",
                   defaults: new
                   {

                       id = System.Web.Http.RouteParameter.Optional,
                       id2 = System.Web.Http.RouteParameter.Optional,
                       id3 = System.Web.Http.RouteParameter.Optional,
                       id4 = System.Web.Http.RouteParameter.Optional

                   }
                );

                funciones.logueo("...Iniciando Servicio en " + urlserviciowindows + " - LocalIP: " + iplocal);
                server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();
                funciones.logueo("**** SERVIDOR HTTP INICIADO en " + urlserviciowindows + " - LocalIP: " + iplocal, "Warn");
                temporizadorServer.Stop();

            }
            catch (Exception ex)
            {
                if (server != null)
                {
                    server.CloseAsync().Wait();
                    server.Dispose();
                    server = null;
                }

                temporizadorServer.Start();

                string msg = "";
                if (ex.InnerException == null)
                {
                    msg = ex.Message;
                }
                else
                {
                    msg = ex.InnerException.GetBaseException().Message;
                }
                funciones.logueo(" - " + ex.Message.ToString() + "\n -- " + msg, "Error");
                funciones.logueo("Error al Iniciar Servidor. ReIntentando en " + (intervaloreconexion / 1000), "Error");
            }
        }

        private void timerfuncionServer(object source, ElapsedEventArgs e)
        {
            try
            {
                IniciarServidor();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
