using Microsoft.Extensions.Logging;
using TFN.Infrastructure.Modules.Logging.AppendBlobLogger;

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
    }
}
