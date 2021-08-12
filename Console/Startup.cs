using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Web.Extensions;

namespace console
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IServiceProvider provider;

        // access the built service pipeline
        public IServiceProvider Provider => provider;

        // access the built configuration
        public IConfiguration Configuration => configuration;

        public Startup(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

            var dir = Directory.GetCurrentDirectory();
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] Files = d.GetFiles("*.*"); //Getting Text files
            string str = "";

            foreach (FileInfo file in Files)
            {
                str = str + ", " + file.Name;
            }

            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", false, true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            // instantiate
            var services = new ServiceCollection();

            // add necessary services
            services.AddSingleton(configuration);

            // these are my app specific services being aded to the service collection
            services.AddApplicationServices(configuration);

            // build the pipeline
            provider = services.BuildServiceProvider();
        }
    }
}
