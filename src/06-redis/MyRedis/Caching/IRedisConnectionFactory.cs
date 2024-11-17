using MyRedis.Caching.Model;
using StackExchange.Redis;

namespace MyRedis.Caching
{
    public interface IRedisConnectionFactory
    {
        public ConnectionMultiplexer Connection
        {
            get;
        }
        public RedisCacheConfiguration OptionRedisCacheConfiguration
        {
            get;
        }
    }
}
