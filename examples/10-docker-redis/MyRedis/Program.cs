using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyRedis.Services;
using Serilog;
using Serilog.Events;
using MyRedis.Caching;
using MyRedis.Caching.Extensions;

class Program
{
    public static async Task Main(string[] args)
    {
        var hostBuilder = new HostBuilder()
             .ConfigureHostConfiguration(configHost => configHost.AddEnvironmentVariables())
            .ConfigureAppConfiguration((hostContext, configBuilder) =>
            {
                configBuilder.SetBasePath(Directory.GetCurrentDirectory());
                configBuilder.AddJsonFile("appsettings.json", optional: true);
                configBuilder.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json",optional: true);
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
                //string config = $"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json";
                IConfigurationSection redisConfig = hostContext.Configuration.GetSection(nameof(RedisCacheConfiguration));

                services.Configure<RedisCacheConfiguration>(redisConfig);
                var redisCacheConfig = redisConfig.Get<RedisCacheConfiguration>();

                if (redisCacheConfig != null)
                {
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = $"{redisCacheConfig.Server}:{redisCacheConfig.Port}";
                        options.InstanceName = $"{redisCacheConfig.InstanceName}";
                    });

                    services.AddRedisMultiplexer(redisCacheConfig.ServerFullName);
                }

                services.AddScoped<IHostedService, WorkerService>();
                
            })
            
            ;

        await hostBuilder.RunConsoleAsync();
    }
}
