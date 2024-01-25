using Quartz;
using ServicioAzureIAS.Clases.CampaniaCliente;
using ServicioAzureIAS.Clases.WhatsApp;
using ServicioAzureIAS.DAL;
using ServicioAzureIAS.utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Jobs.CampaniaCliente {
    public class VerificarCuponesPromocionales : IJob {
        private readonly CMP_ClienteDAL cmpClienteDal;

        public VerificarCuponesPromocionales() {
            cmpClienteDal = new CMP_ClienteDAL();
        }

        public async Task Execute(IJobExecutionContext context) {
            List<CMP_ClienteEntidad> cuponesExpirados = cmpClienteDal.ObtenerCuponesExpirados();
            DateTime ahora = DateTime.Now;
            if(cuponesExpirados.Count == 0) {
                funciones.logueo($"No hay cupones expirados en la revision del dia {ahora.ToString("dd/MM/yyyy HH:mm:ss")}", "Warn");
                return;
            }

            int cuponesRenovados = cmpClienteDal.MarcarComoCuponesComoExpirado(cuponesExpirados.Select(x => x.id).ToList());

            if(cuponesRenovados > 0) {
                foreach(var cuponExpirado in cuponesExpirados) {
                    WSP_SendMessage sendMessage = new WSP_SendMessage() {
                        CodSala = cuponExpirado.CodSala,
                        PhoneNumber = $"{cuponExpirado.CodigoPais}{cuponExpirado.NumeroCelular}",
                        Message = $"Estimado Cliente, el bono promocional de bienvenida, *{cuponExpirado.Codigo}*, al que accediste como cliente nuevo ha vencido.\n\nEn *{cuponExpirado.NombreSala}* queremos dar la mejor experiencia de entrenamiento a nuestros clientes, por esa razón vamos a extender la vigencia para el canje de este bono promocional por única vez por 15 días calendario *(hasta el {cuponExpirado.FechaExpiracionCodigo.AddDays(15).ToString("dd/MM/yyyy")})*.\n\nTe esperamos!"
                    };
                    await cmpClienteDal.SendMessageWhatsApp(sendMessage);
                }
            }
            return;
        }
    }
}
