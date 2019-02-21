using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using bopis_api.Models.Bopis;
using bopis_api.Services.Bopis;

namespace bopis_api.Services.Others
{
    public class EmailServiceImpl : IEmailService
    {
        private ConfigurationServiceImpl configurationServiceImpl = new ConfigurationServiceImpl();

        private string key = "EMAIL";

        public void sendRecoveryPassword(User user)
        {
            List<Configuration> configurations = configurationServiceImpl.findByKeyAndStatusEqualToOne(key);

            string emailAddress = configurations[0].Value;
            string emailPassword = configurations[1].Value;
            string emailName = configurations[2].Value;
            int emailPort = Convert.ToInt32(configurations[3].Value);
            string emailSmtp = configurations[4].Value;
            string pathTemplate = configurations[5].Value;

            MailAddress from = new MailAddress(emailAddress, emailName);
            MailAddress to = new MailAddress(user.Email, user.FullName);

            SmtpClient smtpClient = new SmtpClient(emailSmtp, emailPort);

            smtpClient.EnableSsl = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailAddress, emailPassword);

            MailMessage message = new MailMessage(from, to);
            message.Subject = "Restablecer Contraseña.";
            message.Body = bodyRecoveryPassword(user, pathTemplate);
            message.IsBodyHtml = true;

            smtpClient.Send(message);
        }

        private string bodyRecoveryPassword(User user, string pathTemplate)
        {
            string body = string.Empty;

            StreamReader streamReader = new StreamReader(@pathTemplate);

            body = streamReader.ReadToEnd();
            body = body.Replace("{to}", user.FullName);
            body = body.Replace("{password}", user.Password);
            body = body.Replace("{date}", DateTime.Now.ToString("yyyy-MM-dd"));

            return body;
        }
    }
}
