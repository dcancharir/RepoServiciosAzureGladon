using Newtonsoft.Json;
using Quartz;
using ServicioMigracionClientes.Clases;
using ServicioMigracionClientes.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioMigracionClientes.Jobs.MigracionData
{
    public class JobMigracionData : IJob
    {
        public static string mensajeLog = "Job para migracion de info desde IAS";
        private readonly string URL_IAS = ConfigurationManager.AppSettings["UrlIASAzure"];
        private ClienteDAL clienteDAL = new ClienteDAL();
        public Task Execute(IJobExecutionContext context)
        {
            MigrarData();
            return Task.CompletedTask;
        }
        public void MigrarData()
        {
            try
            {
                var listaClientesIAS = ObtenerListaClientes();
                funciones.logueo($"{mensajeLog} Iniciado -- Cantidad Clientes a migrar : {listaClientesIAS.Count}");
                int count = 0;
                foreach (var cliente in listaClientesIAS)
                {
                    cliente.FechaRegistro = cliente.FechaRegistro.ToLocalTime();
                    bool respuesta=clienteDAL.GuardarCliente(cliente);
                    if (respuesta)
                    {
                        count++;
                    }
                }
                funciones.logueo($"{mensajeLog} Finalizado -- Cantidad Clientes migrados : {count}");
            }
            catch (Exception ex)
            {
                var response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO CLIENTES - " + response, "Error");
            }
        }
        public List<Cliente> ObtenerListaClientes()
        {
            var client = new System.Net.WebClient();
            var response = "";
            var jsonResponse = new List<Cliente>();
            try
            {
                int maximoId = clienteDAL.ObtenerMaximoIdIas();
                string urlIAS = $"{URL_IAS}/AsistenciaCliente/ListarClienteMigracion?Id={maximoId}";
                client.Headers.Add("content-type", "application/json; charset=utf-8");
                response = client.DownloadString(urlIAS);
                jsonResponse = JsonConvert.DeserializeObject<List<Cliente>>(response);
            }
            catch (Exception ex)
            {
                response = " - " + ex.Message.ToString() + "\n ----------- InnerException=\n" + ex.InnerException + "\n --------------- Stack: ------------\n" + ex.StackTrace.ToString();
                funciones.logueo("ERROR OBTENIENDO CLIENTES - " + response,"Error");
            }
            return jsonResponse;
        }
    }
}
