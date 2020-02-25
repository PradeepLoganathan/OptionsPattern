using System;
using System.Threading.Tasks;
using Amazon.S3;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using OptionsPattern.configuration;

namespace OptionsPattern
{
    public class App
    {
        private readonly ILogger<App> _logger;
        private readonly AppConfig _appConfig;
        private readonly IAmazonS3 S3Client;

        public App(IOptions<AppConfig> appSettings, ILogger<App> logger, IAmazonS3 s3Client)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appConfig = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
            this.S3Client = s3Client;
           
        }

        public async Task Run()
        {
            _logger.LogInformation("this is the information message");
            _logger.LogDebug("this is the debug message");
            var buckets = await S3Client.ListBucketsAsync();
            foreach (var item in buckets.Buckets)
            {
                _logger.LogInformation(item.BucketName);
            }
            
            

            Console.WriteLine(_appConfig.AppName);
           
            await Task.CompletedTask;
        }
    }
}
