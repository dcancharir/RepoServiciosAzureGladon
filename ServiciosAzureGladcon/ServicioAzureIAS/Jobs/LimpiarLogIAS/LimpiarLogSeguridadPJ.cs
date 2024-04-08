using Quartz;
using ServicioAzureIAS.Clases.EstadoServicios;
using ServicioAzureIAS.Clases.Sala;
using ServicioAzureIAS.DAL;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static ServicioAzureIAS.DAL.limpiarLogDAL;

namespace ServicioAzureIAS.Jobs.LimpiarLogIAS {
    public class LimpiarLogSeguridadPJ: IJob {
        private readonly string _logMessage = "Limpiar Log BDs";

        public limpiarLogDAL limpiarLogDAL = new limpiarLogDAL();
        public SalaDAL salaDAL = new SalaDAL();
        private readonly CheckPortHelper _checkPortHelper = new CheckPortHelper();

        public Task Execute(IJobExecutionContext context) {

            funciones.logueo($"INICIO - {_logMessage}");

            //LimpiarHistorial();
            RevisarBDsAzure();
            RevisarBDsSalas();

            funciones.logueo($"FIN - {_logMessage}");
            return Task.CompletedTask;
        }



        public void LimpiarHistorial() {
            funciones.logueo($"INICIO - {_logMessage}");
            DateTime horaActual = DateTime.Now;
            try {
                
                limpiarLogDAL ter = new limpiarLogDAL();
                bool respuesta = ter.LimpiarLogDBSeguridadPJ();
                bool respuestaGesasis = ter.LimpiarLogDBRegadis();

                if(respuestaGesasis) {
                    funciones.logueo($"Exito - Tamaño de log Regadis reducido");
                    //List<FileDetail> raa = ter.ObtenerPesoBaseDeDatos();
                    //nombre = IAS
                    //fecha = horaActual.ToShortDateString();
                } else {
                    funciones.logueo($"Error - No se pudo reducir log Regadis");
                }

                if(respuesta) {
                    funciones.logueo($"Exito - Tamaño de log BD_SeguridadPJ reducido");
                    //List<FileDetail> raa = ter.ObtenerPesoBaseDeDatos();
                    //nombre = IAS
                    //fecha = horaActual.ToShortDateString();
                } else {
                    funciones.logueo($"Error - No se pudo reducir log BD_SeguridadPJ");
                }

            } catch(Exception exception) {
                funciones.logueo(exception.Message.ToString(), "Error");
            }

            funciones.logueo($"FIN - {_logMessage}");
        }
        public void RevisarBDsAzure() {
            try {

                List<EspacioDiscoBD> lista = limpiarLogDAL.ListadoBDsAzure();

                foreach(var bd in lista) {

                    try {

                        bool limpio = limpiarLogDAL.LimpiarLogBDAzure(bd.NombreBD, bd.NombreLog);

                        if(limpio) {
                            funciones.logueo($"Exito - Tamaño de log " + bd.NombreBD + " reducido");
                        } else {
                            funciones.logueo($"Error - No se pudo reducir log " + bd.NombreBD);
                        }

                    } catch(Exception ex) {

                        funciones.logueo(bd.NombreBD+": "+ex.Message.ToString(), "Error");
                    }

                }

            } catch(Exception exception) {
                funciones.logueo(exception.Message.ToString(), "Error");
            }

        }

        public class ResponseLimpiarLog {
            public string message { get; set; }
            public List<EspacioDiscoBD> data { get; set; }
        }
        public void RevisarBDsSalas() {
            try {

                string urlWebOnline = string.Empty;

                List<SalaEntidad> listaSalas = salaDAL.ListarSalaActivas().Where(x => !string.IsNullOrEmpty(x.UrlProgresivo) && !string.IsNullOrEmpty(x.IpPrivada)).ToList();

                List<string> urls = listaSalas.Select(x => x.UrlProgresivo).ToList();

                foreach(SalaEntidad sala in listaSalas) {
                    try {
                        CheckPortHelper.TcpConnection tcpConnection = _checkPortHelper.TcpUrlMultiple(sala.UrlProgresivo, urls);

                        if(tcpConnection.IsOpen) {
                            string url = $"{sala.UrlProgresivo}/servicio/EspacioLogsConsulta";

                            object parameters = new
                            {
                            };

                            //tcpConnection.IsVpn = true;

                            if(tcpConnection.IsVpn) {
                                url = $"{tcpConnection.Url}/servicio/VPNGenericoPost";

                                parameters = new
                                {
                                    //dias = days,
                                    ipPrivada = $"{sala.IpPrivada}/servicio/EspacioLogsConsulta"
                                };
                            }

                            try {
                                using(HttpClient httpClient = new HttpClient()) {
                                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                    using(HttpResponseMessage httpResponse = httpClient.PostAsJsonAsync(url, parameters).Result) {
                                        if(httpResponse.IsSuccessStatusCode) {
                                            ResponseLimpiarLog jsonResponse = httpResponse.Content.ReadAsAsync<ResponseLimpiarLog>().Result;

                                            List<EspacioDiscoBD> bds = jsonResponse.data;

                                            if(bds.Count>0) {

                                                bds = bds.Where(x=>x.NombreBD == "BD_ONLINE"|| x.NombreBD == "BD_TECNOLOGIAS" || x.NombreBD == "BD_GladconTask").ToList();

                                                foreach(var bd in bds) {

                                                    try {

                                                        string url2 = $"{sala.UrlProgresivo}/servicio/LimpiarLogsConsulta";

                                                        object parameters2 = new
                                                        {
                                                            nombreBD = bd.NombreBD,
                                                            nombreLog = bd.NombreLog
                                                        };

                                                        //tcpConnection.IsVpn = true;

                                                        if(tcpConnection.IsVpn) {
                                                            url2 = $"{tcpConnection.Url}/servicio/VPNGenericoPost";

                                                            parameters2 = new
                                                            {
                                                                ipPrivada = $"{sala.IpPrivada}/servicio/LimpiarLogsConsulta",
                                                                nombreBD = bd.NombreBD,
                                                                nombreLog = bd.NombreLog
                                                            };
                                                        }

                                                        using(HttpClient httpClient2 = new HttpClient()) {
                                                            httpClient2.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                                            using(HttpResponseMessage httpResponse2 = httpClient2.PostAsJsonAsync(url2, parameters2).Result) {
                                                                if(httpResponse2.IsSuccessStatusCode) {
                                                                    ResponseLimpiarLog jsonResponse2 = httpResponse2.Content.ReadAsAsync<ResponseLimpiarLog>().Result;

                                                                    string mensaje = jsonResponse2.message;

                                                                    if(mensaje == "Consulta correcta") {
                                                                        funciones.logueo($"{sala.Nombre} => Exito - Tamaño de log " + bd.NombreBD + " reducido");
                                                                    } else {
                                                                        funciones.logueo($"{sala.Nombre} => Error - No se pudo reducir log " + bd.NombreBD);
                                                                    }

                                                                } else {
                                                                    funciones.logueo($"{sala.Nombre} => LimpiarLogsConsulta => {url} => {httpResponse.ReasonPhrase}", "Warn");
                                                                }
                                                            }
                                                        }

                                                    } catch(Exception exception) {
                                                        funciones.logueo(exception.Message.ToString(), "Error");
                                                    }

                                                }

                                            } else {
                                                funciones.logueo($"Sala:{sala.Nombre}. No existen BDs.", "Warn");
                                            }

                                        } else {
                                            funciones.logueo($"{sala.Nombre} => EspacioLogsConsulta => {url} => {httpResponse.ReasonPhrase}", "Warn");
                                        }
                                    }
                                }
                            } catch(Exception exception) {
                                funciones.logueo(exception.Message.ToString(), "Error");
                            }


                        } else {
                            funciones.logueo($"{sala.Nombre} => {sala.UrlProgresivo} => No responde", "Warn");
                        }
                    } catch(Exception exception) {
                        funciones.logueo(exception.Message.ToString(), "Error");
                    }

                }

            } catch(Exception exception) {
                funciones.logueo(exception.Message.ToString(), "Error");
            }

        }

    }
}
