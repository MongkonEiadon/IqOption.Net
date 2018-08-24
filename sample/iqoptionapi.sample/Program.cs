using System;
using System.IO;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using IqOptionApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace iqoptionapi.sample
{
    class Program
    {
        static void Main(string[] args)
        {

            var provider = ConfigureServices(new ServiceCollection());

            try
            {

                Console.WriteLine("IqOption.NET Sample");

                var app = provider.GetService<Startup>().RunSample();


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

            var logging = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();


            services
                .AddSingleton<Serilog.ILogger>(logging)
                .AddSingleton(configuration)
                .AddSingleton(new IqOptionConfiguration()
                {
                    Email = configuration["iqoption:email"],
                    Password = configuration["iqoption:password"]
                })
                .AddSingleton<Startup>();

            return services.BuildServiceProvider();
        }

    }
}
