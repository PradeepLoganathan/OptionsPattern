using Amazon.S3;
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

        private static async void RegisterServices(string[] args)
        {
            
            var services = new ServiceCollection();
            services.AddLogging(builder => builder
                                           .AddConsole());
            var EnvironmentType = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");

            
            IConfiguration Configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{EnvironmentType}.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables()
               .AddCommandLine(args)
               .Build();

            
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());          

            services.AddTransient<App>();
            
            services.AddAWSService<IAmazonS3>();
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
