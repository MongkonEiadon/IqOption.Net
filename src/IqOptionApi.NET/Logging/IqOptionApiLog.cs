using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Options;

namespace IqOptionApi.Logging
{
    /// <summary>
    /// Provides static access to the global logger.
    /// </summary>
    public static class IqOptionApiLog
    {
        private static ILoggerFactory _Factory = null;

        /// <summary>
        /// Gets the global logger component.
        /// </summary>
        public static Microsoft.Extensions.Logging.ILogger Logger => IqOptionApiLog.LoggerFactory.CreateLogger("IqOptionApi");

        /// <summary>
        /// Responsible for instantiating a new Logger Factory.
        /// </summary>
        public static ILoggerFactory LoggerFactory
        {
            get
            {
                if (_Factory == null)
                {
                    _Factory = new LoggerFactory();
                    ConfigureLogger(_Factory);
                }
                return _Factory;
            }
            set { _Factory = value; }
        }

        /// <summary>
        /// Configures the logger and applies the configuration.
        /// </summary>
        /// <param name="factory">The logger factory.</param>
        public static void ConfigureLogger(ILoggerFactory factory)
        {
            factory
                .AddProvider(new DebugLoggerProvider());
        }        
    }
}