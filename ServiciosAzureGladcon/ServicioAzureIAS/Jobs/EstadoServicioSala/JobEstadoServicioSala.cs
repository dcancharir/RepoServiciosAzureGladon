using Common.Logging;
using Newtonsoft.Json;
using Quartz;
using ServicioAzureIAS.Clases.EstadoServicios;
using ServicioAzureIAS.Clases.Sala;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Jobs.EstadoServicioSala
{
    public class JobEstadoServicioSala:IJob
    {
        public static string mensajeLog = "CONSULTANDO ESTADO SERVICIOS POR SALA";
        public EstadoServiciosDAL estadoServiciosDAL = new EstadoServiciosDAL();
        public SalaDAL salaDAL = new SalaDAL();
        private string FirebaseKey = ConfigurationManager.AppSettings["firebaseServiceKey"];
        private readonly CheckPortHelper _checkPortHelper = new CheckPortHelper();


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

            try
            {

                EstadoServiciosEntidad entidad = new EstadoServiciosEntidad();
                List<SalaEntidad> listaSalas = new List<SalaEntidad>();
                List<SalaEntidad> listaAuxSalas = salaDAL.ListarSalaActivas().Where(x => !string.IsNullOrEmpty(x.UrlSalaOnline) && !string.IsNullOrEmpty(x.IpPrivada)).ToList();

                string cadenaSalas = string.Empty;


                try
                {
                    cadenaSalas = ConfigurationManager.AppSettings["SalasSeleccionadas"];
                }
                catch
                {
                    cadenaSalas = "";
                }

                if(cadenaSalas == null)
                {
                    cadenaSalas = "0";
                }

                string[] filtroSalas = cadenaSalas.Split(',');

                if(filtroSalas.Length > 0 && !filtroSalas.Contains(""))
                {

                    if (!filtroSalas.Contains("0")) {

                        foreach (var item in filtroSalas)
                        {
                            var salaFiltrada = listaAuxSalas.Where(x => x.CodSala == Convert.ToInt32(item)).FirstOrDefault();
                            if (salaFiltrada != null)
                            {
                                listaSalas.Add(salaFiltrada);
                            }
                        }
                    }                     
                } else
                {
                    listaSalas.AddRange(listaAuxSalas);
                }


                List<string> urls = listaSalas.Select(x => x.UrlSalaOnline).ToList();

                foreach (SalaEntidad sala in listaSalas)
                {
                    CheckPortHelper.TcpConnection tcpConnection = _checkPortHelper.TcpUrlMultiple(sala.UrlSalaOnline, urls);

                    if (tcpConnection.IsOpen)
                    {
                        string url = $"{sala.UrlSalaOnline}/EstadoServicios/ConsultarServicios";

                        object parameters = new
                        {
                            //dias = days
                        };

                        //tcpConnection.IsVpn = true;

                        if (tcpConnection.IsVpn)
                        {
                            url = $"{tcpConnection.Url}/servicio/VPNGenericoPost";

                            parameters = new
                            {
                                //dias = days,
                                ipPrivada = $"{sala.IpPrivada}/online/EstadoServicios/ConsultarServicios"
                            };
                        }

                        try
                        {
                            using (HttpClient httpClient = new HttpClient())
                            {
                                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                using (HttpResponseMessage httpResponse = httpClient.PostAsJsonAsync(url, parameters).Result)
                                {
                                    if (httpResponse.IsSuccessStatusCode)
                                    {
                                        ResponseEstadoServicios jsonResponse = httpResponse.Content.ReadAsAsync<ResponseEstadoServicios>().Result;

                                        var data = jsonResponse.data;

                                        if (Convert.ToBoolean(jsonResponse.respuesta))
                                        {

                                            entidad.EstadoWebOnline = Convert.ToBoolean(data.estadoWebOnline);
                                            entidad.EstadoGladconServices = Convert.ToBoolean(data.estadoGladconServices);
                                            entidad.CodSala = sala.CodSala;
                                            entidad.FechaRegistro = DateTime.Now;
                                            estadoServiciosDAL.InsertEstadoServicios(entidad);
                                            funciones.logueo("NWO:" + entidad.EstadoWebOnline + " - GS:" + entidad.EstadoGladconServices + " - Sala:" + sala.Nombre + " - Fecha:" + DateTime.Now);

                                            if (!entidad.EstadoWebOnline || !entidad.EstadoGladconServices)
                                            {

                                                var errormensaje = "Revise el servicio en la sala " + sala.Nombre;
                                                var titulo = "ALERTA";
                                                var servidorKey = FirebaseKey;
                                                List<NotificacionDispositivo> devices = new List<NotificacionDispositivo>();
                                                devices = estadoServiciosDAL.GetDevicesToNotification(sala.CodSala);

                                                string[] dispositivos_ = devices.Select(x => x.id).ToArray();

                                                if (!entidad.EstadoWebOnline && !entidad.EstadoGladconServices)
                                                {

                                                    if (dispositivos_.Count() > 0)
                                                    {
                                                        titulo = "¡Servicio Web Online y Servicio Gladcon Services no funcionando!";
                                                        EnvioFirebase(servidorKey, dispositivos_, errormensaje, titulo);
                                                    }
                                                }
                                                else
                                                if (!entidad.EstadoWebOnline)
                                                {

                                                    if (dispositivos_.Count() > 0)
                                                    {
                                                        titulo = "¡Servicio Web Online no funcionando!";
                                                        EnvioFirebase(servidorKey, dispositivos_, errormensaje, titulo);
                                                    }
                                                }
                                                else
                                                if (!entidad.EstadoGladconServices)
                                                {

                                                    if (dispositivos_.Count() > 0)
                                                    {
                                                        titulo = "¡Servicio Gladcon Services no funcionando!";
                                                        EnvioFirebase(servidorKey, dispositivos_, errormensaje, titulo);
                                                    }
                                                }
                                            }

                                        } else
                                        {
                                            funciones.logueo(jsonResponse.mensaje, "Error");
                                            funciones.logueo($"{sala.Nombre} => {url} => {httpResponse.ReasonPhrase}");
                                            funciones.logueo("TERMINADO - " + mensajeLog);
                                            return true;
                                        }


                                        funciones.logueo($"{sala.Nombre} => {url} => {httpResponse.ReasonPhrase}");
                                    }
                                    else
                                    {
                                        funciones.logueo($"{sala.Nombre} => {url} => {httpResponse.ReasonPhrase}", "Warn");
                                    }
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            funciones.logueo(exception.Message.ToString(), "Error");
                        }
       

                    }
                    else
                    {
                        funciones.logueo($"{sala.Nombre} => {sala.UrlProgresivo} => No responde", "Warn");
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
