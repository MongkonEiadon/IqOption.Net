using System;
using System.IO;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iqoptionapi.sample
{
    class Program
    {
        static void Main(string[] args)
        {

            var provider = ConfigureServices(new ServiceCollection());

            try {

                Console.WriteLine("IqOption.NET Sample");

                var app = provider.GetService<Startup>().RunSample();


                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true)
                .Build();

            services
                .AddSingleton<ILoggerFactory, LoggerFactory>()
                .AddSingleton(typeof(ILogger<>), typeof(Logger<>)) // Add first my already configured instance
                .AddLogging(c =>
                        c.SetMinimumLevel(LogLevel.Trace)
                        .AddConsole()
                        .AddConfiguration(configuration.GetSection("Logging")));
            

            // Support typed Options
            services
                .AddSingleton(configuration)
                .AddSingleton<Startup>()
                .AddOptions()
                .Configure<IqOptionConfiguration>(o => configuration.GetSection("iqoption").Bind(o)) ;

            return services.BuildServiceProvider();
        }

    }
}
