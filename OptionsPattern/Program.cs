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
                                            .AddDebug()
                                            .AddConsole());
            services.AddOptions();

            var loggingConfig = configuration.GetSection("LoggingConfig").Get<LoggingConfig>();
            services.AddSingleton(loggingConfig);
            


                            

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
