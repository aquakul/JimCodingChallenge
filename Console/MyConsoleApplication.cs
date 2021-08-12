using Microsoft.Extensions.Logging;
using System;

namespace console
{
    public class MyConsoleApplication
    {
        public MyConsoleApplication(IServiceProvider serviceProvider, ILogger<MyConsoleApplication> logger)
        {
            logger.LogDebug("I am here");
        }
    }
}
