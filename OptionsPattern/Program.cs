using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OptionsPattern.configuration;
using System;
using System.IO;

namespace OptionsPattern
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            RegisterServices(Configuration);

            DisposeServices();
        }

        private static void RegisterServices(IConfiguration configuration)
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.Configure<LoggingConfig>(configuration.GetSection(""));
            
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
