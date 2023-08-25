using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.IO;

namespace ServicioMigracionClientes.utilitarios
{
    public class funciones
    {
        public static ConsoleColor colorinicial = Console.ForegroundColor;
        public static string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectionBD"].ConnectionString;
        public static void logueo(string mensaje, string tipo = "Color", ConsoleColor color = System.ConsoleColor.Gray)
        {
#if (!DEBUG)
            switch(tipo){
                case "Debug": EventLog.WriteEntry(ServicioMigracionClientes.nombreservicio,mensaje,EventLogEntryType.Information) ; break;
                case "Warn": EventLog.WriteEntry(ServicioMigracionClientes.nombreservicio, mensaje, EventLogEntryType.Warning); break;
                case "Error": EventLog.WriteEntry(ServicioMigracionClientes.nombreservicio, mensaje, EventLogEntryType.Error); break;
                case "default": EventLog.WriteEntry(ServicioMigracionClientes.nombreservicio, mensaje, EventLogEntryType.Information); break;
                default: EventLog.WriteEntry(ServicioMigracionClientes.nombreservicio, mensaje, EventLogEntryType.Information); break;
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
        public static String consultaBDMigracion(string query)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            JsonWriter jsonWriter = new JsonTextWriter(sw);
            jsonWriter.WriteStartArray();

            /*  if (query.Length == 0)
              {
                  query = "SELECT * FROM " + tabla + "";
              }*/
            try
            {
                using (var connection = new SqlConnection(conexion))
                {
                    connection.Open();
                    using (var command = new SqlCommand(query, connection))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            jsonWriter.WriteStartObject();
                            int fields = reader.FieldCount;
                            for (int i = 0; i < fields; i++)
                            {
                                jsonWriter.WritePropertyName(reader.GetName(i));
                                jsonWriter.WriteValue(reader[i]);
                            }
                            jsonWriter.WriteEndObject();
                        }
                        jsonWriter.WriteEndArray();
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                return "ERROR SQL";// ex.Message;
            }
        }
    }
}
