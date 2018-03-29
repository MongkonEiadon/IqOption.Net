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
            var logger = provider.GetService<ILogger<Program>>();

            try {
                logger.LogInformation("Application start!");

                var app = provider.GetService<Startup>().Run();


                Console.ReadLine();
            }
            catch (Exception ex) {
                logger.LogCritical("App Error!", ex);
            }

        }

        private static IServiceProvider ConfigureServices(IServiceCollection services)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
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

            services
                .AddSingleton(configuration)
                .AddSingleton<IqOptionConfiguration>(s => s.GetService<IOptions<IqOptionConfiguration>>().Value)
                .AddSingleton<Startup>();


            // Support typed Options
            services
                .AddOptions()
                .Configure<IqOptionConfiguration>(o => configuration.GetSection("iqoption").Bind(o));

            return services.BuildServiceProvider();
        }

    }
}
