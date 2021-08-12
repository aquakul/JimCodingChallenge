using Generator;
using Generator.Implementations;
using Generator.Interfaces;
using Generator.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeneratorWeb.Extensions
{
    public static  class ApplicationServiceExtensions
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // this is the default random generator selector config if the caller does not specify any. Set in the config
            services.Configure<RandomGeneratorSelectorSettings>(configuration.GetSection("RandomGeneratorSelectorSettings"));

            // Config for random generator via 3rd party API
            var generatorApisettings = configuration.GetSection("RandomGeneratorAPISettings").Get<RandomGeneratorViaAPISettings>();
            services.AddSingleton(generatorApisettings);

            services.AddSingleton<RandomGeneratorLocal>();
            services.AddScoped<RandomGeneratorViaAPI>();

            // register a factory to return the generator based on the key.
            // check if there is a better way to resolve multiple services registered for the same service
            services.AddTransient<GeneratorResolver>(serviceProvider => key => {

                // for logging
                //var services = serviceProvider.GetServices<IGenerator>();

                return key switch
                {
                    "local" => serviceProvider.GetService<RandomGeneratorLocal>(),
                    "api" => serviceProvider.GetService<RandomGeneratorViaAPI>(),
                    _ => null,
                };
            });

            return services;
        }
    }
}
