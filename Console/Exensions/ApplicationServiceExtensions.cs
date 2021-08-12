using System;
using Generator;
using Generator.Implementations;
using Generator.Interfaces;
using Generator.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Operator;
using Operator.Lambda;

namespace Web.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // this is the default random generator selector config if the caller does not specify any. Set in the config
            services.Configure<RandomGeneratorSelectorSettings>(configuration.GetSection("RandomGeneratorSelectorSettings"));

            // Config for random generator via 3rd party API
            var generatorApisettings = configuration.GetSection("RandomGeneratorAPISettings").Get<RandomGeneratorViaAPISettings>();
            services.AddSingleton(generatorApisettings);

            // Config for AWS Lambda connection
            var awsConnectionSettings = configuration.GetSection("AWSLambdaConnectionSettings").Get<LambdaConnectionSettings>();
            services.AddSingleton(awsConnectionSettings);

            services.AddSingleton<IGenerator, RandomGeneratorLocal>();
            services.AddScoped<IGenerator, RandomGeneratorViaAPI>();

            services.AddSingleton<ICloudCalculateService, AWSLambdaCalculateService>();

            services.AddSingleton<IStrategy, Add>();
            services.AddSingleton<IStrategy, Subtract>();
            services.AddSingleton<IStrategy, Multiply>();
            services.AddSingleton<IStrategy, Divide>();


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

            // register a factory to return the operation based on the key passed.
            services.AddTransient<OperationResolver>(serviceProvider => key =>
            {
                // switch case would have been ideal but the properties we are comparing against are not constant
                if(key.Equals(Add.Name, StringComparison.OrdinalIgnoreCase) || key.Equals(Add.Shorthand))
                {
                    return serviceProvider.GetService<Add>();
                }
                else if(key.Equals(Subtract.Name, StringComparison.OrdinalIgnoreCase) || key.Equals(Subtract.Shorthand))
                {
                    return serviceProvider.GetService<Subtract>();
                }
                else if(key.Equals(Multiply.Name, StringComparison.OrdinalIgnoreCase) || key.Equals(Multiply.Shorthand))
                {
                    return serviceProvider.GetService<Multiply>();
                }
                else if(key.Equals(Divide.Name, StringComparison.OrdinalIgnoreCase) || key.Equals(Divide.Shorthand))
                {
                    return serviceProvider.GetService<Divide>();
                }
                else
                {
                    return null;
                }
            });

            return services;
        }
    }
}