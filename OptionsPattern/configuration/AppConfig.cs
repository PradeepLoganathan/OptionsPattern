using System;
using System.Collections.Generic;
using System.Text;

namespace OptionsPattern.configuration
{
    public class AppConfig
    {
        public string AppName { get; set; }
        public LoggingConfig loggingConfig { get; set; }
    }
}
