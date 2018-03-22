using System;
using System.IO;
using Autofac;
using AutofacContrib.NSubstitute;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using Shouldly;

namespace iqoptionapi.unit {
    [TestFixture]
    public class BaseUnitTest
    {
        protected AutoSubstitute AutoSubstitute { get; private set; }
        protected IqOptionConfiguration Configuration { get; set; }

        [OneTimeSetUp]
        public void SetUpFixture() {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath(Directory.GetCurrentDirectory()).Build();

            var services = new ServiceCollection();
            services.AddOptions()
                .AddSingleton<IqOptionConfiguration>(s => s.GetService<IOptions<IqOptionConfiguration>>().Value)
                .Configure<IqOptionConfiguration>(o => config.GetSection("iqoption").Bind(o));

            var build = services.BuildServiceProvider();
            

            AutoSubstitute = new AutoSubstitute(c => {
                c.Register<IServiceProvider>(x => services.BuildServiceProvider());
                c.Register<IqOptionConfiguration>(x => build.GetService<IqOptionConfiguration>());
            });

            Configuration = AutoSubstitute.Resolve<IqOptionConfiguration>();
        }
        


    }
}