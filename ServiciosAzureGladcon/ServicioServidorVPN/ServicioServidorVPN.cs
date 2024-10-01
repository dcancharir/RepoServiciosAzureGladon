using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Http.SelfHost;
using ServicioServidorVPN.utilitarios;
using System.Web.Http.Cors;
using System.Web.Http;
using ServicioServidorVPN.Jobs;

namespace ServicioServidorVPN
{
    partial class ServicioServidorVPN : ServiceBase
    {
        public static string nombreservicio = "ServicioServidorVPN";

        public static string conectionOnline = "";
        public static string conection = "";

        public static string puertoserviciowindows = "";
        public static string urlserviciowindows = "";
        public static string iplocal = "";
        public static int intervaloreconexion = 10000;


        public static string urlias = "";
        public static string urlApiERP = "";
        public static int urlenvio = 0;
        public static System.Timers.Timer temporizadorServer = new System.Timers.Timer();
        public ServicioServidorVPN()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            funciones.logueo("El servicio se ha iniciado");

            Task.Run(async () => {
                MyScheduler schedulerClass = new MyScheduler();
                await schedulerClass.StartMigracionData();
            });
            funciones.logueo("Jobs iniciados");

            //temporizador Servers
            temporizadorServer.Elapsed += new ElapsedEventHandler(timerfuncionServer);
            temporizadorServer.Interval = intervaloreconexion;
            temporizadorServer.Enabled = true;
            temporizadorServer.Stop();

            IniciarServidor();

        }

        protected override void OnStop()
        {
            //enviar correo
            string mensajeEnviar = "Se Detuvo el Servicio VPN a las : " + string.Format("{0:dd/MM/yyyy hh: mm: ss tt}", DateTime.Now);
            Correo correo = new Correo();
            string listaCorreosEnviar = "intranet.corporacionpj@gmail.com";
            correo.EnviarCorreo(listaCorreosEnviar, "Servicio VPN(Data Warehouse)", mensajeEnviar);

            // TODO: agregar código aquí para realizar cualquier anulación necesaria para detener el servicio.
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
            urlserviciowindows = "http://localhost:" + puertoserviciowindows;
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

                //enviar correo
                string mensajeEnviar = "Se Inicio el Servicio VPN a las : " + string.Format("{0:dd/MM/yyyy hh: mm: ss tt}", DateTime.Now);
                Correo correo = new Correo();
                string listaCorreosEnviar = "intranet.corporacionpj@gmail.com";
                correo.EnviarCorreo(listaCorreosEnviar, "Servicio VPN(Data Warehouse)", mensajeEnviar);
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
