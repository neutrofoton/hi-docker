using MyRedis.Caching.Repository;

namespace MyRedis.Caching.Extension
{
    public static class RedisRepositoryExtension
    {
        public static Task<V> DoGet<V>(this IRedisRepository repository, Func<IRedisConnectionFactory, V> func)
        {
            return Task.FromResult(func(repository.ConnectionFactory));
        }

        public static Task DoAction(this IRedisRepository repository, Action<IRedisConnectionFactory> action)
        {
            action(repository.ConnectionFactory);
            return Task.CompletedTask;
        }

        public static List<string> GetAllKeys(this IRedisRepository repository)
        {
            List<string> listKeys = new List<string>();

            var server = repository.ConnectionFactory.OptionRedisCacheConfiguration.Server;
            var port = repository.ConnectionFactory.OptionRedisCacheConfiguration.Port;

            var keys = repository.ConnectionFactory.Connection.GetServer(server, port).Keys();
            listKeys.AddRange(keys.Select(key => (string)key).ToList());

            return listKeys;
        }
    }
}
