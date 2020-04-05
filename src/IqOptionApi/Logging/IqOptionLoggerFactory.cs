using IqOptionApi.Logging;
using IqOptionApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace IqOptionApi
{
    internal class IqOptionLoggerFactory
    {
        private static ILogger _loggerInstance;

        public static ILogger CreateHttpLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] [HTTP]\t{SourceContext}{Message:lj}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Literate)
                .CreateLogger();
        }


        public static ILogger CreateWebSocketLogger(Profile profile)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Async(
                    a => a.LiterateConsole(
                        outputTemplate:
                        "[{Timestamp:HH:mm:ss} {Level:u4}] [WSS] [@{Email} {BalanceType} {Currency}{BalanceAmount:N2}] {SourceContext} {Topic} | {Message:lj}{NewLine}{Exception}"))
                .Enrich.With(new ProfileEnricher(profile))
                .CreateLogger();
        }

        public static ILogger CreateLogger()
        {
            if (_loggerInstance == null)
                _loggerInstance = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Async(
                        a => a.Console(
                            outputTemplate:
                            "[{Timestamp:HH:mm:ss} {Level:u4}] [API] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}",
                            theme: AnsiConsoleTheme.Literate))
                    .Enrich.FromLogContext()
                    .CreateLogger();

            return _loggerInstance;
        }
    }
}