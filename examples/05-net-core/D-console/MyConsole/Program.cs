﻿// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyConsole.Infrastructure;
using MyConsole.Services;
using Serilog;
using Serilog.Events;
using System.IO;
using System.Threading.Tasks;

namespace MyConsole
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    configBuilder.SetBasePath(Directory.GetCurrentDirectory());
                    configBuilder.AddJsonFile("appsettings.json", optional: true);
                    configBuilder.AddJsonFile(
                        $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",
                        optional: true);
                    configBuilder.AddEnvironmentVariables();
                })
                .UseSerilog((hostContext, services, configLogging) =>
                {

                    configLogging
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .WriteTo.Console()
                        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<EndpointConfiguration>(hostContext.Configuration.GetSection("EndpointConfiguration"));
                    
                    services.AddSingleton<IEndpointConfiguration>(serviceProvider =>
                    {
                        //bug
                        var res = hostContext.Configuration.GetSection("EndpointConfiguration").Get<EndpointConfiguration>();
                        return res;
                    });

                    services.AddScoped<IHostedService, WorkerService>();
                });

            await hostBuilder.RunConsoleAsync();
        }
    }
}