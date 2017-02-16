using Microsoft.Extensions.Logging;

namespace TFN.Infrastructure.Modules.Logging.Email
{
    internal class EmailLoggerProvider : ILoggerProvider
    {
        public LogLevel MinimumLevel { get; private set; }
        public string Recipient { get; private set; }
        public string Sender { get; private set; }
        public string SmtpUsername { get; private set; }
        public string SmtpPassword { get; private set; }
        public string SmtpHost { get; private set; }
        public int SmtpPort { get; private set; }
        public string EnvironmentName { get; private set; }

        public EmailLoggerProvider(string recipient, string sender, string smtpUsername, string smtpPassword, string smtpHost, int smtpPort, string environmentName, LogLevel minimumLevel)
        {
            MinimumLevel = minimumLevel;
            Recipient = recipient;
            Sender = sender;
            SmtpUsername = smtpUsername;
            SmtpPassword = smtpPassword;
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
            EnvironmentName = environmentName;
        }

        public ILogger CreateLogger(string name)
        {
            return new EmailLogger(Recipient, Sender, SmtpUsername, SmtpPassword, SmtpHost, SmtpPort, EnvironmentName, MinimumLevel);
        }

        public void Dispose()
        {
        }
    }
}
