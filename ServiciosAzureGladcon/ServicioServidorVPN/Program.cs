using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["CultureInfo"]);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new ServicioAzureIAS()
            //};
            //ServiceBase.Run(ServicesToRun);
            var serviceToRun = new ServicioServidorVPN();

            ///condicion si en modo DEBUG abrir consola
#if (!DEBUG)
                            ServiceBase.Run(serviceToRun);
#else
            serviceToRun.Start();
            Console.ReadLine();
#endif
        }
    }
}
