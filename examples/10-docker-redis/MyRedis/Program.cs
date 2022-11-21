using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyRedis.Services;
using Serilog;
using Serilog.Events;

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
                    ;
            })
            .ConfigureServices((hostContext, services) =>
            {   
                services.AddStackExchangeRedisCache(options => {
                    options.Configuration = hostContext.Configuration.GetConnectionString("Redis");
                    options.InstanceName = "RedisDemo_";
                });

                services.AddScoped<IHostedService, WorkerService>();
            });

        await hostBuilder.RunConsoleAsync();
    }
}
