using Microsoft.Extensions.Logging;
using TFN.Infrastructure.Modules.Logging.AppendBlob;
using TFN.Infrastructure.Modules.Logging.Email;

namespace TFN.Infrastructure.Modules
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddAppendBlob(
            this ILoggerFactory factory,
            string connectionString,
            LogLevel minimumLevel)
        {
            factory.AddProvider(new AppendBlobLoggerProvider(connectionString, minimumLevel));
            return factory;
        }

        public static ILoggerFactory AddEmail(
            this ILoggerFactory factory,
            string recipient,
            string sender,
            string smtpUsername,
            string smtpPassword,
            string smtpHost,
            int smtpPort,
            string environmentName,
            LogLevel minimumLevel)
        {
            factory.AddProvider(new EmailLoggerProvider(recipient, sender, smtpUsername, smtpPassword, smtpHost,
                smtpPort, environmentName, minimumLevel));
            return factory;
        }
    }
}
