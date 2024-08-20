using ServicioAzureIAS.Clases.Email;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ServicioAzureIAS.Service {
    public class EmailService {
        private readonly string userEmail;

        public EmailService() {
            userEmail = ConfigurationManager.AppSettings["correo"] ?? string.Empty;
        }

        public async Task SendEmailAsync(string destinatarios, string asunto, string body, bool isHtml) {
            try {
                using(MailMessage mail = new MailMessage()) {
                    mail.From = new MailAddress(userEmail);
                    mail.To.Add(destinatarios);
                    mail.Subject = asunto;
                    mail.Body = body;
                    mail.IsBodyHtml = isHtml;

                    SmtpClient smtpClient = SmtpClientSingleton.Instance;

                    await smtpClient.SendMailAsync(mail);
                }
            } catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
