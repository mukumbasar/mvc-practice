using KitchenApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace KitchenApp.Services.Concrete
{
    public class GmailMailSender : IMailService
    {
        private readonly IConfiguration _configuration;
        private PartialViewResult _partialViewResult;
        public GmailMailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SetMailView(PartialViewResult partialViewResult)
        {
            _partialViewResult = partialViewResult;
        }

        public void SendEmail(string toMail, string subject, string content, bool isHtml = false)
        {
            var emailServicesConf = _configuration.GetSection("EmailService");

            ///string view = _partialViewResult.ToString();

            string username = emailServicesConf["UserName"];
            string password = emailServicesConf["Password"];

            MailAddress from = new MailAddress(username, "Kazım Göksel Kalyoncu");
            MailAddress to = new MailAddress(toMail, "Burak Ozan");

            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;

            message.Body = content;
            message.IsBodyHtml = isHtml;

            using (SmtpClient smtp = new SmtpClient())
            {

                smtp.Host = emailServicesConf["Host"];
                smtp.EnableSsl = Convert.ToBoolean(emailServicesConf["EnableSsl"]);
                NetworkCredential NetworkCred = new NetworkCredential(username, password);
                smtp.UseDefaultCredentials = Convert.ToBoolean(emailServicesConf["UseDefaultCredentials"]);
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt32(emailServicesConf["Port"]);
                try
                {
                    smtp.Send(message);

                }
                catch (Exception ex)
                {
                    //Exception loglaması yapılacak
                }

            }
        }
    }
}
