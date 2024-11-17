using MyRedis.Caching.Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyRedis.Caching.Repository
{
    //https://stackexchange.github.io/StackExchange.Redis/Basics
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RedisRepository<T> : IRedisRepository<T> where T : CachedModel, new()
    {
        private readonly IRedisConnectionFactory connectionFactory;
        public RedisRepository(IRedisConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public IRedisConnectionFactory ConnectionFactory
        {
            get { return this.connectionFactory; }
        }

        public async Task<T> GetAsync(string key)
        {
            string json = await this.ConnectionFactory.Connection
                .GetDatabase()
                .StringGetAsync(key);

            return JsonSerializer.Deserialize<T>(json);
        }

        public async Task SaveAsync(string key, T data, TimeSpan? expiry=null)
        {
            var json = JsonSerializer.Serialize(data);
            await this.ConnectionFactory.Connection
                .GetDatabase()
                .StringSetAsync(key, json, expiry);
        }

        public async Task DeleteAsync(string key)
        {
            await this.ConnectionFactory.Connection
                .GetDatabase()
                .KeyDeleteAsync(key);
        }

        protected HashEntry[] GenerateHash(T obj)
        {
            var props = this.GetType().GetProperties();
            var hash = new HashEntry[props.Count()];

            for (var i = 0; i < props.Count(); i++)
                hash[i] = new HashEntry(props[i].Name, props[i].GetValue(obj).ToString());

            return hash;
        }

        protected T MapFromHash(HashEntry[] hash)
        {
            var instance = new T();
            var props = instance.GetType().GetProperties();

            for (var i = 0; i < props.Count(); i++)
            {
                for (var j = 0; j < hash.Count(); j++)
                {
                    if (props[i].Name == hash[j].Name)
                    {
                        var val = hash[j].Value;
                        var type = props[i].PropertyType;

                        if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                            if (string.IsNullOrEmpty(val))
                            {
                                props[i].SetValue(instance, null);
                            }
                        props[i].SetValue(instance, Convert.ChangeType(val, type));
                    }
                }
            }
            return instance;
        }

        
    }
}
