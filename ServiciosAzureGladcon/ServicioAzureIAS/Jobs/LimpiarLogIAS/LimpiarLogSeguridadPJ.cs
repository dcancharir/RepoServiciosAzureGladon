using Quartz;
using ServicioAzureIAS.Clases.Sala;
using ServicioAzureIAS.DAL;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServicioAzureIAS.DAL.limpiarLogDAL;

namespace ServicioAzureIAS.Jobs.LimpiarLogIAS {
    public class LimpiarLogSeguridadPJ: IJob {
        private readonly string _logMessage = "Limpiar Log BD SeguridadPJ";

        public Task Execute(IJobExecutionContext context) {

            LimpiarHistorial();
            return Task.CompletedTask;
        }



        public void LimpiarHistorial() {
            funciones.logueo($"INICIO - {_logMessage}");
            DateTime horaActual = DateTime.Now;
            try {
                
                limpiarLogDAL ter = new limpiarLogDAL();
                bool respuesta = ter.LimpiarLogDBSeguridadPJ();

                if(respuesta) {
                    funciones.logueo($"Exito - Tamaño de log reducido");
                    //List<FileDetail> raa = ter.ObtenerPesoBaseDeDatos();
                    //nombre = IAS
                    //fecha = horaActual.ToShortDateString();
                } else {
                    funciones.logueo($"Error - No se pudo reducir log");
                }

            } catch(Exception exception) {
                funciones.logueo(exception.Message.ToString(), "Error");
            }

            funciones.logueo($"FIN - {_logMessage}");
        }

    }
}
