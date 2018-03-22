using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace iqoptionapi {

    internal static class IqOptionLoggerFactory {

        public static ILogger CreateLogger() {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true)
                .Build();


            var loggerFactory = new LoggerFactory()
                .AddConsole(configuration.GetSection("Logging"))
                .AddDebug(LogLevel.Trace);


            return loggerFactory.CreateLogger<IqOptionApi>();
        }
    }
}