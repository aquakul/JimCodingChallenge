using Generator.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Operator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Web.Extensions;

namespace console
{
    class Program
    {

        static void Main(string[] args)
        {
            // Setup Host
            var host = CreateDefaultBuilder(args).Build();
            host.RunAsync();
        }

        static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            // instantiate startup
            // all the constructor logic would happen
            //var startup = new Startup(args);

            return Host.CreateDefaultBuilder()
                .ConfigureHostConfiguration(hostConfig =>
                {
                    hostConfig.SetBasePath(Directory.GetCurrentDirectory());

                    var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");

                    hostConfig.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    hostConfig.AddJsonFile($"appsettings.{env}.json", true, true);
                    hostConfig.AddEnvironmentVariables();
                    hostConfig.AddCommandLine(args);
                })
                .ConfigureAppConfiguration((context, app) =>
                {
                    var env = context.HostingEnvironment.EnvironmentName.ToLower();
                    app.AddSystemsManager($"/calculator/{env}/", TimeSpan.FromMinutes(5));
                })
                .ConfigureServices((context,services) =>
                {
                    services.AddTransient<MyConsoleApplication>();
                    services.AddHostedService<MyHostedService>();
                    services.AddHttpClient();
                    services.AddApplicationServices(context.Configuration);
                });
        }

        /// <summary>
        /// 
        /// </summary>
        internal sealed class MyHostedService : IHostedService
        {
            private readonly IServiceProvider _serviceProvider;
            private readonly ILogger<MyHostedService> _logger;
            private readonly IHostApplicationLifetime _appLifetime;

            public MyHostedService(IServiceProvider serviceProvider, ILogger<MyHostedService> logger, IHostApplicationLifetime appLifetime)
            {
                _serviceProvider = serviceProvider;
                _logger = logger;
                _appLifetime = appLifetime;
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                _logger.LogDebug($"Starting with arguments: {string.Join(" ", Environment.GetCommandLineArgs())}");

                _appLifetime.ApplicationStarted.Register(() =>
                {
                    _logger.LogDebug("Application host has started");
                });

                StartComputation();

                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }

            private void StartComputation()
            {
                while (true)
                {
                    Console.Write("Welcome to Calculator Service ... ");
                    Console.Write("\n\n");
                    Console.Write("Choose one of the methods to generate a random number by typing in the number against it.");

                    using var scope = _serviceProvider.CreateScope();

                    var generator = ChooseGenerator(scope);
                    var strategy = ChooseStrategy(scope);

                    var numbers = generator.Generate().Result;
                    var result = strategy.Compute(numbers);

                    Console.Write("\n\n");
                    Console.WriteLine($"Inputs are {string.Join(",", numbers)}.\n\nThe result of compute is {result}");

                    Console.ReadLine();

                    return;
                }
            }

            /// <summary>
            /// Choose one of the Generators
            /// </summary>
            /// <param name="scope"></param>
            /// <returns></returns>

            private static IGenerator ChooseGenerator(IServiceScope scope)
            {
                var services = scope.ServiceProvider.GetServices<IGenerator>();

                string generators = string.Empty;
                List<string> choices = new();
                int i = 1;
                foreach (var s in services)
                {
                    var name = s.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Static).GetValue(s);
                    var shorthand = s.GetType().GetProperty("Shorthand", BindingFlags.Public | BindingFlags.Static).GetValue(s);
                    generators += i + ". " + s.GetType().Name + " (" + shorthand + ")" + "\n";
                    choices.Add(i + "");
                    i++;
                }

                Console.Write("\n\n");
                Console.WriteLine(generators);
                while (true)
                {
                    string choice = Console.ReadLine();
                    if (choice != null && choice.Length == 1)
                    {
                        if (choices.Contains(choice))
                        {
                            return ((IGenerator[])services)[int.Parse(choice) - 1];
                        }
                        else Console.Write("Choose a valid Generator \n\n");
                    }
                    else
                    {
                        Console.Write("Choose a valid Generator \n\n");
                    }
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="scope"></param>
            /// <returns></returns>

            private static IStrategy ChooseStrategy(IServiceScope scope)
            {
                Console.WriteLine("\n\n");
                Console.Write("Choose one of the operators by typing in the number against it.");

                var services = scope.ServiceProvider.GetServices<IStrategy>();

                string strategies = string.Empty;
                List<string> choices = new();
                int i = 1;
                foreach (var s in services)
                {
                    var name = s.GetType().GetProperty("Name", BindingFlags.Public | BindingFlags.Static).GetValue(s);
                    var shorthand = s.GetType().GetProperty("Shorthand", BindingFlags.Public | BindingFlags.Static).GetValue(s);
                    strategies += i + ". " + s.GetType().Name + " (" + shorthand + ")" + "\n";
                    choices.Add(i + "");
                    i++;
                }

                Console.Write("\n\n");
                Console.WriteLine(strategies);
                while (true)
                {
                    string choice = Console.ReadLine();
                    if (choice != null && choice.Length == 1)
                    {
                        if (choices.Contains(choice))
                        {
                            return ((IStrategy[])services)[int.Parse(choice) - 1];
                        }
                        else Console.Write("Choose a valid Operator \n\n");
                    }
                    else
                    {
                        Console.Write("Choose a valid Operator \n\n");
                    }
                }
            }
        }
    }
}
