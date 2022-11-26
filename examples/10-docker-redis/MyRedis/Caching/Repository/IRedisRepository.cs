using MyRedis.Caching.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRedis.Caching.Repository
{
    public interface IRedisRepository
    {
        IRedisConnectionFactory ConnectionFactory { get; }
    }

    public interface IRedisRepository<T> : IRedisRepository
        where T : CachedModel
    {
        
        Task<T> GetAsync(string key);

        Task SaveAsync(string key, T data, TimeSpan? expiry = null);

        Task DeleteAsync(string key);
    }
}
