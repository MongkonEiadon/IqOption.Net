using System;
using IqOptionApi.Logging;
using IqOptionApi.Sample.Logging;
using Microsoft.Extensions.DependencyInjection;
using LogProvider = IqOptionApi.Logging.LogProvider;

namespace IqOptionApi.Sample {
    public class Program {
        private static IIqOptionApi api;

        private static void Main(string[] args) {
            var provider = ConfigureServices(new ServiceCollection());
            var sample = provider.GetService<TradingExample>();

            try {
                Console.WriteLine("IqOption.NET SampleOpenOrder");

                sample.RunAsync().ConfigureAwait(false);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                Console.ReadLine();
            }
        }

        private static IServiceProvider ConfigureServices(IServiceCollection services) {
            LogProvider.SetCurrentLogProvider(new ColoredConsoleLogProvider());

            services
                .AddSingleton<TradingExample>()
                .AddSingleton<SampleOpenOrder>();

            return services.BuildServiceProvider();
        }
    }
}