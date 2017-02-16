using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using TFN.Domain.Interfaces.Services;
using TFN.Domain.Models.Options;

namespace TFN.Domain.Services
{
    public class EmailService : IEmailService
    {
        private IOptions<EmailServiceOptions> Options { get; set; }

        public EmailService(IOptions<EmailServiceOptions> options)
        {
            Options = options;
        }
        public Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email = new MailMessage();
            email.From = Options.Value.Sender;
            email.Sender = Options.Value.Sender;
            email.To.Add(new MailAddress(toEmail));
            email.IsBodyHtml = true;
            email.Subject = subject;
            email.Body = body;

            Task.Factory.StartNew(() =>
            {
                using (var smtpClient = new SmtpClient(Options.Value.SmtpHost, Options.Value.SmtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = Options.Value.Credentials;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(email);
                }
            });

            return Task.CompletedTask;
        }
    }
}
