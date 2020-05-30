﻿using IqOptionApi.Logging;
using IqOptionApi.Models;
using Serilog;
 using Serilog.Events;
 using Serilog.Sinks.SystemConsole.Themes;

namespace IqOptionApi
{
    internal class IqOptionLoggerFactory
    {
        private static ILogger _loggerInstance;

        public static ILogger CreateHttpLogger()
        {
            return new LoggerConfiguration()
#if Release
                .MinimumLevel.Information()
#else
                .MinimumLevel.Verbose()
#endif
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] [HTTP]\t{SourceContext}{Message:lj}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Literate)
                .CreateLogger()
                .ForContext("SourceContext", nameof(IqOptionClient));
        }


        public static ILogger CreateWebSocketLogger(Profile profile)
        {
            var t =
                "[{Timestamp:HH:mm:ss} {Level:u4}] [WSS] [@{Email} {BalanceType} {Currency}{BalanceAmount:N2}] {SourceContext} {Topic} | {Message:lj}{NewLine}{Exception}";

            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Debug()
                .WriteTo.LiterateConsole(LogEventLevel.Debug, outputTemplate: t).Enrich.With(new ProfileEnricher(profile))
                .CreateLogger()
                .ForContext("SourceContext", nameof(IqOptionClient));
        }

        public static ILogger CreateLogger()
        {
            if (_loggerInstance == null)
                _loggerInstance = new LoggerConfiguration()
#if Release
                .MinimumLevel.Information()
#else
                    .MinimumLevel.Verbose()
#endif
                    .WriteTo.Async(
                        a => a.Console(
                            outputTemplate:
                            "[{Timestamp:HH:mm:ss} {Level:u4}] [API] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}",
                            theme: AnsiConsoleTheme.Literate))
                    .Enrich.FromLogContext()
                    .CreateLogger()
                    .ForContext("SourceContext", nameof(IqOptionClient));;

            return _loggerInstance;
        }
    }
}