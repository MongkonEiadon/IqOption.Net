using IqOptionApi.Logging;

#if NETCOREAPP
using Microsoft.Extensions.Configuration;
#endif


namespace IqOptionApi.Extensions {
    internal static class IqOptionLoggerFactory {
        private static ILog _loggerInstance;

        public static void SetLogger(ILog logger) {
            _loggerInstance = logger;
        }

        public static ILog CreateLogger() {
            if (_loggerInstance == null) _loggerInstance = LogProvider.GetCurrentClassLogger();

            return _loggerInstance;
        }
    }
}