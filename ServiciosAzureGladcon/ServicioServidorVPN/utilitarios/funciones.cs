using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.utilitarios
{
    public class funciones
    {
        public static ConsoleColor colorinicial = Console.ForegroundColor;
        public static void logueo(string mensaje, string tipo = "Color", ConsoleColor color = System.ConsoleColor.Gray)
        {
#if (!DEBUG)
            switch(tipo){
                case "Debug": EventLog.WriteEntry(ServicioServidorVPN.nombreservicio,mensaje,EventLogEntryType.Information) ; break;
                case "Warn": EventLog.WriteEntry(ServicioServidorVPN.nombreservicio, mensaje, EventLogEntryType.Warning); break;
                case "Error": EventLog.WriteEntry(ServicioServidorVPN.nombreservicio, mensaje, EventLogEntryType.Error); break;
                case "default": EventLog.WriteEntry(ServicioServidorVPN.nombreservicio, mensaje, EventLogEntryType.Information); break;
                default: EventLog.WriteEntry(ServicioServidorVPN.nombreservicio, mensaje, EventLogEntryType.Information); break;
            }
                 //   Service1.log.Debug(mensaje);
#else

            switch (tipo)
            {
                case "Debug": Console.ForegroundColor = colorinicial; break;
                case "Warn": Console.ForegroundColor = System.ConsoleColor.Green; break;
                case "Error": Console.ForegroundColor = System.ConsoleColor.Red; break;
                case "Color": Console.ForegroundColor = color; break;
                default: Console.ForegroundColor = colorinicial; break;

            }

            Console.WriteLine(mensaje);
            Console.ForegroundColor = colorinicial;

#endif

        }
    }
}
