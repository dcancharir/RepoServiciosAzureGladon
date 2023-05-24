using Common.Logging;
using Newtonsoft.Json;
using Quartz;
using ServicioAzureIAS.Clases.EstadoServicios;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Jobs.EstadoServicioSala
{
    public class JobEstadoServicioSala:IJob
    {
        public static string mensajeLog = "CONSULTANDO ESTADO SERVICIOS POR SALA";
        public EstadoServiciosDAL estadoServiciosDAL = new EstadoServiciosDAL();
        private string FirebaseKey = ConfigurationManager.AppSettings["firebaseServiceKey"];


        public Task Execute(IJobExecutionContext context)
        {
            ConsultaServicioEnSala();
            return Task.CompletedTask;
        }
        public bool ConsultaServicioEnSala()
        {
            funciones.logueo("INICIO - "+mensajeLog);

            List<EstadoServiciosEntidad> lista = new List<EstadoServiciosEntidad>();
            string urlWebOnline = string.Empty;
            //urlWebOnline = ConfigurationManager.AppSettings["UrlWebOnline"]; //Descomentar para pruebas 

            try
            {
                EstadoServiciosEntidad entidad = new EstadoServiciosEntidad();
                List<SalaEntidad> listaSalas = estadoServiciosDAL.ListadoSalaActivas();

                foreach(var item in listaSalas)
                {
                    urlWebOnline = item.UrlSalaOnline; //Comentar para pruebas 
                    var client = new System.Net.WebClient();
                    string ruta = "/EstadoServicios/ConsultarServicios";
                    string url = urlWebOnline + ruta;
                    client.Headers.Add("content-type", "application/json; charset=utf-8");
                    client.Encoding = Encoding.UTF8;
                    var response = client.UploadString(url, "POST");
                    ResponseEstadoServicios jsonResponse = JsonConvert.DeserializeObject<ResponseEstadoServicios>(response);
                    if (Convert.ToBoolean(jsonResponse.respuesta))
                    {
                        var data = jsonResponse.data;
                        entidad.EstadoWebOnline = Convert.ToBoolean(data.estadoWebOnline);
                        entidad.EstadoGladconServices = Convert.ToBoolean(data.estadoGladconServices);
                    }
                    entidad.CodSala = item.CodSala;
                    entidad.FechaRegistro = DateTime.Now;
                    estadoServiciosDAL.InsertEstadoServicios(entidad);
                    funciones.logueo("NWO:"+entidad.EstadoWebOnline+" - GS:"+entidad.EstadoGladconServices+ " - Sala:" + item.Nombre + " - Fecha:" + DateTime.Now);



                    var errormensaje = "Revise el servicio en la sala "+item.Nombre;
                    var titulo = "ALERTA";
                    var servidorKey = FirebaseKey;
                    List<NotificacionDispositivo> devices = new List<NotificacionDispositivo>();
                    //item.CodSala = 7; //Descomentar para pruebas 
                    devices = estadoServiciosDAL.GetDevicesToNotification(item.CodSala);

                    string[] dispositivos_ = devices.Select(x => x.id).ToArray();
                    if (!entidad.EstadoWebOnline)
                    {

                        if (dispositivos_.Count() > 0)
                        {
                            titulo = "¡Servicio Web Online no funcionando!";
                            EnvioFirebase(servidorKey, dispositivos_, errormensaje, titulo);
                        }
                    }
                    if (!entidad.EstadoGladconServices)
                    {

                        if (dispositivos_.Count() > 0)
                        {
                            titulo = "¡Servicio Gladcon Services no funcionando!";
                            EnvioFirebase(servidorKey, dispositivos_, errormensaje, titulo);
                        }
                    }

                }




            }
            catch (Exception ex)
            {
                funciones.logueo(ex.Message,"Error");
            }

            funciones.logueo("TERMINADO - "+mensajeLog);

            return true;
        }

        public void EnvioFirebase(string servidorKey, String[] DeviceToken, string msg, string title)
        {

            try
            {
                //var serverKey = "AAAAuNqaZi0:APA91bHevFUNteMjQNHBNIC6I2WvlvwLv7thv92a1WPKfiA-dxMiMZ3YaVsf2aZ2PFN5ytBM1JNQIWevFjB5mH3FgZeIrRWGjHKQcXnPvYwuujd8dD16CISrid5XE1-MjyaO01wQFvWQ";
                var serverKey = servidorKey;
                //DeviceToken = "eiXYDwYt7_w:APA91bHJLSV2CmV5BdkTHZagnvLTuSJ7PbpI-zuLb5vaBhY3bytyD0tenGA0L-aRjOgNZsugUS6uS6RB_wPkD7LGIeY5FlbNZI5XuSpmvhXQNguzio8hWLYEwi3hRamitqFWqE7p72VB";
                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                //serverKey - Key from Firebase cloud messaging server  
                tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
                //Sender Id - From firebase project setting  
                tRequest.Headers.Add(string.Format("Sender: id={0}", String.Join(",", DeviceToken)));
                tRequest.ContentType = "application/json";
                var payload = new
                {
                    //to = "fcd2PSqhBGc:APA91bHA8z7G22s0cEWxsuPmMPXmJptMJ2S5-dToF-BtZxyHpo50sskedHiZliox6CJy1vDRZk6zlNFHsiosUdX62D4mhqMuOG3GnI4O96xxH0CJvtcodR8PVsoUh7DGVQUVN-mu5BpW",
                    registration_ids = DeviceToken,
                    priority = "high",
                    content_available = true,
                    data = new
                    {
                        body = msg,
                        title = title,
                        badge = 1
                    },
                };
                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    funciones.logueo("Respuesta Firebase" + sResponseFromServer);
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                funciones.logueo("Respuesta Firebase" + ex.Message);
            }
        }
    }
}
