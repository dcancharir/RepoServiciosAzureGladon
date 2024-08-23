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
    public class VerificarCodigosPromocionales : IJob {
        private readonly CMP_ClienteDAL cmpClienteDal;

        public VerificarCodigosPromocionales() {
            cmpClienteDal = new CMP_ClienteDAL();
        }

        public async Task Execute(IJobExecutionContext context) {
            List<CMP_ClienteEntidad> codigosExpirados = cmpClienteDal.ObtenerCodigosExpirados();
            DateTime ahora = DateTime.Now;
            if(codigosExpirados.Count == 0) {
                funciones.logueo($"No hay códigos promocionales expirados en la revisión del día {ahora.ToString("dd/MM/yyyy HH:mm:ss")}", "Warn");
                return;
            }

            List<CMP_ClienteEntidad> codigosQueSeMarcanComoExpirados = codigosExpirados.Where(x => !x.CodigoSeReactiva).ToList();
			List<CMP_ClienteEntidad> codigosQueSeMarcanComoExpiradosReactivados = codigosExpirados.Where(x => x.CodigoSeReactiva).ToList();

            int codigosMarcadosComoExpirados = cmpClienteDal.MarcarCodigosComoExpirado(codigosQueSeMarcanComoExpirados.Select(x => x.id).ToList());

            //aca empizan los que se reactivan, agrupando por campaña
            int codigosMarcadosComoExpiradosReactivados = 0;
            var codigoReactivarAgrupados = codigosQueSeMarcanComoExpiradosReactivados.GroupBy(x => x.campania_id);
            foreach(var codigoCampania in codigoReactivarAgrupados) {
                var primero = codigoCampania.FirstOrDefault();
                int diasExtraCodigoPromocional = primero.DuracionReactivacionCodigoDias;
                int horasExtraCodigoPromocional = primero.DuracionReactivacionCodigoHoras;
                codigosMarcadosComoExpiradosReactivados += cmpClienteDal.MarcarCodigosComoExpiradoConReactivacion(codigoCampania.Select(x => x.id).ToList(), diasExtraCodigoPromocional, horasExtraCodigoPromocional);
            }

            if(codigosMarcadosComoExpiradosReactivados > 0) {
                foreach(CMP_ClienteEntidad codigoReactivar in codigosQueSeMarcanComoExpiradosReactivados) {
                    WSP_SendMessage sendMessage = new WSP_SendMessage() {
                        CodSala = codigoReactivar.CodSala,
                        PhoneNumber = $"{codigoReactivar.CodigoPais}{codigoReactivar.NumeroCelular}",
                        Message = codigoReactivar.ObtenerMensajeFormateadoParaEnvio(codigoReactivar.MensajeWhatsAppReactivacion, codigoReactivar),
                        //Message = $"Estimado Cliente, el bono promocional de bienvenida, *{codigoRenovar.Codigo}*, al que accediste como cliente nuevo ha vencido.\n\nEn *{codigoRenovar.NombreSala}* queremos dar la mejor experiencia de entrenamiento a nuestros clientes, por esa razón vamos a extender la vigencia para el canje de este bono promocional por única vez por 15 días calendario *(hasta el {codigoRenovar.FechaExpiracionCodigo.AddDays(15).ToString("dd/MM/yyyy")})*.\n\nTe esperamos!"
                    };
                    await cmpClienteDal.SendMessageWhatsApp(sendMessage);
                }
            }
            return;
        }
    }
}
