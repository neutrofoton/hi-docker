using Microsoft.Extensions.Caching.Distributed;
using MyRedis.Caching.Model;
using System.Collections;
using System.Text.Json;

namespace MyRedis.Caching.Extension
{
    public static class DistributedCacheExtensions
    {
        public static async Task SaveCacheAsync<T>(this IDistributedCache cache,
                                                   string key,
                                                   object data,
                                                   TimeSpan? absoluteExpireTime = null,
                                                   TimeSpan? slidingExpireTime = null) where T : class
        {
            var options = new DistributedCacheEntryOptions();

            options.AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = slidingExpireTime ?? TimeSpan.FromSeconds(60);


            if (data is CachedModel || data is ICollection<T> || data is ICollection)
            {
                var json = JsonSerializer.Serialize(data);
                await cache.SetStringAsync(key, json, options);
            }
            else if (data is byte[])
            {
                await cache.SetAsync(key, (byte[])data, options);
            }
            else
            {
                throw new CacheException($"Type of {data.GetType().Name} identified not registered for to be cached");
            }
        }

        public static async Task<T> GetCacheAsync<T>(this IDistributedCache cache,
                                                       string key) where T : class
        {
            if (typeof(T).IsAssignableTo(typeof(CachedModel)) || typeof(T).IsAssignableTo(typeof(IEnumerable)))
            {
                var json = await cache.GetStringAsync(key);
                if (string.IsNullOrEmpty(json))
                    return default;

                return JsonSerializer.Deserialize<T>(json);
            }
            else if (typeof(T).IsAssignableTo(typeof(byte[])))
            {
                var bytes = await cache.GetAsync(key);
                return bytes as T;
            }
            else
            {
                throw new CacheException($"Type of {typeof(T).Name} identified not registered for to be cached");
            }
        }
    }
}
