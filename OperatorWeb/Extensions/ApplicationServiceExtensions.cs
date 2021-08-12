using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Operator;
using Operator.Lambda;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OperatorWeb.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Config for AWS Lambda connection
            var awsConnectionSettings = configuration.GetSection("AWSLambdaConnectionSettings").Get<LambdaConnectionSettings>();
            services.AddSingleton(awsConnectionSettings);

            services.AddSingleton<ICloudCalculateService, AWSLambdaCalculateService>();

            services.AddSingleton<Add>();
            services.AddSingleton<Subtract>();
            services.AddSingleton<Multiply>();
            services.AddSingleton<Divide>();

            // register a factory to return the operation based on the key passed.
            services.AddTransient<OperationResolver>(serviceProvider => key =>
            {
                // switch case would have been ideal but the properties we are comparing against are not constant
                if (key.Equals(Add.Name, StringComparison.OrdinalIgnoreCase) || key.Equals(Add.Shorthand))
                {
                    return serviceProvider.GetService<Add>();
                }
                else if (key.Equals(Subtract.Name, StringComparison.OrdinalIgnoreCase) || key.Equals(Subtract.Shorthand))
                {
                    return serviceProvider.GetService<Subtract>();
                }
                else if (key.Equals(Multiply.Name, StringComparison.OrdinalIgnoreCase) || key.Equals(Multiply.Shorthand))
                {
                    return serviceProvider.GetService<Multiply>();
                }
                else if (key.Equals(Divide.Name, StringComparison.OrdinalIgnoreCase) || key.Equals(Divide.Shorthand))
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
