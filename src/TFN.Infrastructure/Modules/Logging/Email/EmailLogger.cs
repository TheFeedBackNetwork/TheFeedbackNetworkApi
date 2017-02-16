using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TFN.Infrastructure.Modules.Logging.Email
{
    internal class EmailLogger : ILogger
    {
        public LogLevel MinimumLevel { get; private set; }
        public MailAddress Recipient { get; private set; }
        public MailAddress Sender { get; private set; }
        public NetworkCredential Credentials { get; private set; }
        public string SmtpHost { get; private set; }
        public int SmtpPort { get; private set; }
        public string EnvironmentName { get; private set; }

        public EmailLogger(string recipient, string sender, string smtpUsername, string smtpPassword, string smtpHost, int smtpPort, string environmentName, LogLevel minimumLevel)
        {
            MinimumLevel = minimumLevel;
            Recipient = new MailAddress(recipient);
            Sender = new MailAddress(sender);

            if (String.IsNullOrWhiteSpace(smtpUsername))
            {
                throw new ArgumentNullException(nameof(smtpUsername), "SMTP server username cannot be null or empty.");
            }

            if (String.IsNullOrWhiteSpace(smtpPassword))
            {
                throw new ArgumentNullException(nameof(smtpPassword), "SMTP server password cannot be null or empty.");
            }

            if (String.IsNullOrWhiteSpace(smtpHost))
            {
                throw new ArgumentNullException(nameof(smtpHost), "SMTP host cannot be null or empty.");
            }

            if (smtpPort == 0)
            {
                throw new ArgumentNullException(nameof(smtpPort), "SMTP port cannot be zero.");
            }

            if (String.IsNullOrWhiteSpace(environmentName))
            {
                throw new ArgumentNullException(nameof(environmentName), "Environment name cannot be null or empty.");
            }

            Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
            EnvironmentName = environmentName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= MinimumLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var email = new MailMessage();
            email.From = Sender;
            email.Sender = Sender;
            email.To.Add(Recipient);
            email.IsBodyHtml = true;

            email.Subject = $"The Feedback Network [{EnvironmentName}] {logLevel} occurred.";
            email.Body = $"<html><body><b>{logLevel} occurred in {EnvironmentName}.</b><br><br>{state}<br><br>{exception}</body></html>";

            Task.Factory.StartNew(() =>
            {
                using (var smtpClient = new SmtpClient(SmtpHost, SmtpPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = Credentials;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(email);
                }
            });
        }
    }
}
