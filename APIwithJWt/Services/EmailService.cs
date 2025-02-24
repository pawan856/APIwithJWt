using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Reflection;

namespace APIwithJWt.Services
{
    public class EmailService : IEmailService
    {
       
        private readonly IConfiguration _configuration;

        public EmailService (IConfiguration configuration)
        {
            _configuration = configuration;
        }
         public async Task sendEmailasync (string email ,string subject , string body)
        {
            var smtpClient = new Smtpclient(_configuration["smtp:host"]);
            {
                Port = int.Parse(smtpClient(_configuration["smtp:Port"]),

                credentails = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"])),
                    EnableSsl = true;
            };
            var mailmessage = new MailMessaage
            {
                from = new MailAddress(_configuration["Smtp:username"]),

                subject = subject,
                body = body,
                IsBodyHtml = true
            };

            mailmessage.To.Add(toEmail);
            smtpClient.Send(mailmessage);
        }
    }
}
