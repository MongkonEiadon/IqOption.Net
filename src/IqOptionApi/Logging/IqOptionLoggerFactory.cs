using System;

using IqOptionApi.Logging;
using IqOptionApi.Models;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using ILogger = Serilog.ILogger;

namespace IqOptionApi {

    internal enum ChannelType {
        WSS,
        Http,
        Api
    }

    internal class ConnectionInfo {

        public string PayloadMessage { get; set; }
        
        public Profile UserProfile { get; set; }
        
        public ChannelType ChannelType { get; set; }
        
        public string MessageDescription { get; set; }
        
        public Exception Exception { get; set; }
        public ConnectionInfo(
            ChannelType type,
            string message,
            string payloadMessage = null,
            string description = null,
            Profile profile = null,
            Exception exception = null) {
            
        }
    }

    internal class IqOptionLoggerFactory {

        private static ILogger _loggerInstance;

        public static ILogger CreateHttpLogger() =>
            new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] [HTTP]\t{SourceContext}{Message:lj}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Literate)
                .CreateLogger();


        public static ILogger CreateWebSocketLogger(Profile profile) => new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Async(
                a => a.LiterateConsole(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level:u4}] [WSS] [@{Email} {BalanceType} {Currency}{BalanceAmount:N2}] {SourceContext} {Topic} | {Message:lj}{NewLine}{Exception}"))
            .Enrich.With(new ProfileEnricher(profile))
            .CreateLogger();

        public static ILogger CreateLogger() {
            if (_loggerInstance == null) {
                _loggerInstance = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Async(
                        a => a.Console(
                            outputTemplate:
                            "[{Timestamp:HH:mm:ss} {Level:u4}] [API] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}",
                            theme: AnsiConsoleTheme.Literate))
                    .Enrich.FromLogContext()
                    .CreateLogger();
            }

            return _loggerInstance;
        }
    }
}