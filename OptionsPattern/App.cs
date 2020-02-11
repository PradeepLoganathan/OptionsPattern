using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OptionsPattern.configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OptionsPattern
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly AppConfig _appConfig;

        public App(IOptions<AppConfig> appSettings, ILogger<App> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appConfig = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public async Task Run()
        {
            Console.WriteLine();
            Console.WriteLine("Hello world!");
            Console.WriteLine();
            Console.WriteLine(_appConfig.AppName);
            Console.WriteLine();
            await Task.CompletedTask;
        }
    }
}
