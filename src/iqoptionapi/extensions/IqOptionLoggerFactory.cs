using System.IO;

#if NETCOREAPP
using Microsoft.Extensions.Configuration;
#endif

using ILogger = Serilog.ILogger;

namespace iqoptionapi {
    internal static class IqOptionLoggerFactory {
        private static ILogger _loggerInstance;

        public static void SetLogger(ILogger logger) {
            _loggerInstance = logger;
        }

        public static ILogger CreateLogger() {

#if NETCOREAPP

            if (_loggerInstance == null) {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.json", optional: true)
                    .Build();


                var loggerFactory = new LoggerFactory()
                    .AddConsole(configuration.GetSection("Logging"))
                    .AddFile("Logs/iqoptionapi-{Date}.txt", LogLevel.Warning)
                    .AddDebug(LogLevel.Trace);


                _loggerInstance = loggerFactory.CreateLogger<IqOptionApi>();
            }
#endif

            return _loggerInstance;
        }
    }
}