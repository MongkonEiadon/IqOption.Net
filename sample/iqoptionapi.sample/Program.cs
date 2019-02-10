using System;
using System.IO;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using IqOptionApi;
using IqOptionApi.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace IqOptionApi.Sample
{
    class Program
    {
        static void Main(string[] args)
        {

            var provider = ConfigureServices(new ServiceCollection());

            try
            {

                Console.WriteLine("IqOption.NET Sample");
                LogProvider.SetCurrentLogProvider(new ColoredConsoleLogProvider());

                var app = provider.GetService<IqClientExample>();
                app.RunAsync().ConfigureAwait(false);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }

        }

        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true)
                .Build();

            services
                .AddSingleton(configuration)
                .AddSingleton(new IqOptionConfiguration()
                {
                    Email = configuration["iqoption:email"],
                    Password = configuration["iqoption:password"]
                })
                .AddSingleton<TradingExample>()
                .AddSingleton<IqClientExample>()
                .AddSingleton<Startup>();

            return services.BuildServiceProvider();
        }

    }
}
