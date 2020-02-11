using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OptionsPattern.configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OptionsPattern
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        public static async Task Main(string[] args)
        {
            RegisterServices(args);
            await _serviceProvider.GetService<App>().Run();
            DisposeServices();
        }

        private static void RegisterServices(string[] args)
        {
            
            var services = new ServiceCollection();
            services.AddLogging(builder => builder
                                           .AddConsole());

            IConfiguration Configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .AddCommandLine(args)
               .Build();

            services.AddOptions();

            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));
            services.AddTransient<App>();
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
