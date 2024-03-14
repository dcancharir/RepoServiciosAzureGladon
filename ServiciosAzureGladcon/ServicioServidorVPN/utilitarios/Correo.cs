using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServicioServidorVPN.utilitarios
{
    public class Correo
    {
        private SmtpClient cliente;
        private MailMessage email;
       
        private string _HOST = "smtp.gmail.com";
        private string _PORT = "587";
        private string _ENABLESSL = "true";
        private string _USER = "pruebaprogra2@gmail.com";
        private string _PASWWORD = "vidtebjlyfqrlbpp";
        public Correo()
        {

            cliente = new SmtpClient(_HOST, Int32.Parse(_PORT))
            {
                EnableSsl = Boolean.Parse(_ENABLESSL),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_USER, _PASWWORD)
            };
        }
        public void EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtlm = false, List<string> adjuntos = null)
        {
            try
            {
                email = new MailMessage(_USER, destinatario, asunto, mensaje);
                email.IsBodyHtml = esHtlm;
                email.BodyEncoding = System.Text.Encoding.UTF8;
                email.SubjectEncoding = System.Text.Encoding.Default;
                if(adjuntos != null)
                {
                    foreach(var adjunto in adjuntos)
                    {
                        Attachment attachment = new Attachment(adjunto, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = attachment.ContentDisposition;
                        disposition.FileName = Path.GetFileName(adjunto);
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        email.Attachments.Add(attachment);
                    }
                }
                cliente.Send(email);
            } catch(Exception ex)
            {
                funciones.logueo("No se pudo enviar el mail sobre estado de servicio - " + ex.Message, "Error");
            }

        }
        public void EnviarCorreo(MailMessage message)
        {
            cliente.Send(message);
        }
    }
}
