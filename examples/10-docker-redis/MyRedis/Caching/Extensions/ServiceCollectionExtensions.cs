using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRedis.Caching.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisMultiplexer(this IServiceCollection services, string redisServer)
        {
            var multiplexer = ConnectionMultiplexer.Connect(redisServer);
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            return services;
        }
    }
}
