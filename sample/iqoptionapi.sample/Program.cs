using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iqoptionapi.sample {
    public class Program {
        public static void Main(string[] args)
        { 

            var provider = ConfigureServices(new ServiceCollection());

            provider.GetService<Startup>().Run().GetAwaiter();

            Console.ReadKey();
        }

        private static IServiceProvider ConfigureServices(IServiceCollection services) {
            ILoggerFactory loggerFactory = new LoggerFactory()
                .AddConsole()
                .AddDebug();

            services.AddSingleton(loggerFactory); // Add first my already configured instance
            services.AddLogging(c => c.AddConsole()); // Allow ILogger<T>

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true)
                .Build();


            services
                .AddSingleton(configuration)
                .AddSingleton(s => s.GetService<IOptions<IqOptionConfiguration>>().Value)
                .AddSingleton<Startup>();


            // Support typed Options
            services
                .AddOptions()
                .Configure<IqOptionConfiguration>(o => configuration.GetSection("iqoption").Bind(o));

            return services.BuildServiceProvider();
        }

    }
}
