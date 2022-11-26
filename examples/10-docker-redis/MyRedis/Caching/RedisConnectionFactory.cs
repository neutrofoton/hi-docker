using Microsoft.Extensions.Options;
using MyRedis.Caching.Model;
using StackExchange.Redis;

namespace MyRedis.Caching
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly Lazy<ConnectionMultiplexer> connection;
        private readonly IOptionsMonitor<RedisCacheConfiguration> optionRedisCacheConfiguration;

        public RedisConnectionFactory(IOptionsMonitor<RedisCacheConfiguration> optionRedisCacheConfiguration)
        {
            this.optionRedisCacheConfiguration = optionRedisCacheConfiguration;
            connection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(this.optionRedisCacheConfiguration.CurrentValue.ServerFullName));
        }

        public ConnectionMultiplexer Connection
        {
            get { return connection.Value; }
        }
        public RedisCacheConfiguration OptionRedisCacheConfiguration
        {
            get { return optionRedisCacheConfiguration.CurrentValue; }
        }
    }
}
