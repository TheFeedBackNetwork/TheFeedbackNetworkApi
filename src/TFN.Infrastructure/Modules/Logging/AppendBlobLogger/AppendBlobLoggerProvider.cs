using Microsoft.Extensions.Logging;

namespace TFN.Infrastructure.Modules.Logging.AppendBlobLogger
{
    internal class AppendBlobLoggerProvider : ILoggerProvider
    {
        public LogLevel MinimumLevel { get; }

        public string StorageAccountConnectionString { get; }

        public AppendBlobLoggerProvider(string storageAccountConnectionString, LogLevel minimumLevel)
        {
            StorageAccountConnectionString = storageAccountConnectionString;
            MinimumLevel = minimumLevel;
        }

        public ILogger CreateLogger(string name)
        {
            return new AppendBlobLogger(StorageAccountConnectionString, MinimumLevel, name);
        }

        public void Dispose()
        {
        }
    }
}
