using Microsoft.Extensions.Configuration;
using System;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //IConfiguration Configuration = new ConfigurationBuilder()
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    .AddJsonFile($"appsettings.{env}.json", true, true)
            //    .AddEnvironmentVariables()
            //    .AddCommandLine(args)
            //    .Build();
        }
    }
}
