using System.IO;
using Serilog;
using Serilog.Events;
#if NETCOREAPP
using Microsoft.Extensions.Configuration;
#endif

using ILogger = Serilog.ILogger;

namespace IqOptionApi {

    internal static class IqOptionLoggerFactory {
        private static ILogger _loggerInstance;

        public static void SetLogger(ILogger logger) {
            _loggerInstance = logger;
        }

        public static ILogger CreateLogger() {


            if (_loggerInstance == null) {

                _loggerInstance = new LoggerConfiguration()
                    .CreateLogger();
            }

            return _loggerInstance;
        }
    }
}