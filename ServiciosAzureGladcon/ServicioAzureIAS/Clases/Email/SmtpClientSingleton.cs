using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace ServicioAzureIAS.Clases.Email {
    public sealed class SmtpClientSingleton {
        private static readonly Lazy<SmtpClient> _instance = new Lazy<SmtpClient>(() => CreateSmtpClient());

        private SmtpClientSingleton() { }

        public static SmtpClient Instance => _instance.Value;

        private static SmtpClient CreateSmtpClient() {
            string host = ConfigurationManager.AppSettings["host"];
            int port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            bool enableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["enableSSL"]);
            string user = ConfigurationManager.AppSettings["correo"];
            string password = ConfigurationManager.AppSettings["password"];

            Console.WriteLine("Creando instancia de SmtpClient");

            SmtpClient smtpClient = new SmtpClient(host) {
                Port = port,
                EnableSsl = enableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(user, password)
            };

            return smtpClient;
        }
    }
}
