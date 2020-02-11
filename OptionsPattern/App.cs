using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using OptionsPattern.configuration;

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
            _logger.LogInformation("this is the information message");
            _logger.LogDebug("this is the debug message");
            
            Console.WriteLine(_appConfig.AppName);
           
            await Task.CompletedTask;
        }
    }
}
