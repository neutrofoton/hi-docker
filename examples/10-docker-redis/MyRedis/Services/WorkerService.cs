using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyRedis.Caching;
using MyRedis.Caching.Extensions;
using MyRedis.Model;
using Serilog;
using StackExchange.Redis;
using System.Text.Json;

namespace MyRedis.Services
{
    public class WorkerService : IHostedService, IDisposable
    {
        public bool isRunningProcess = false;

        private IDistributedCache cache;
        private IConnectionMultiplexer multiplexer;
        private readonly IOptionsMonitor<RedisCacheConfiguration> optionRedisCacheConfiguration;

        public WorkerService(IOptionsMonitor<RedisCacheConfiguration> optionRedisCacheConfiguration, IConnectionMultiplexer multiplexer, IDistributedCache cache)
        {
            this.optionRedisCacheConfiguration = optionRedisCacheConfiguration;
            this.multiplexer = multiplexer;
            this.cache = cache;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Thread t = new Thread(new ThreadStart(Do));

            isRunningProcess = true;
            t.Start();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            isRunningProcess = false;

            Log.Information($"ThreadProc is stopped at: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");

            return Task.CompletedTask;
        }

        private async void Do()
        {
            var key = $"{this.GetType().Name}_data";
            var job = new Job();

            while (true)
            {
                var now = DateTime.Now;
                job.Id = Guid.NewGuid().ToString();
                job.At = now;
                job.Name = $"Job at {now.ToString("dd-MMM-yyyy hh:mm:ss")}";


                Task setTask = cache.SaveCacheAsync<Job>(key, job);

                Task waitTask =Task.Delay(1000);
                Task logTask = Task.Run(() => Log.Warning($"{"Save to cache".PadRight(20)}: {JsonSerializer.Serialize<Job>(job)}"));

                Task.WaitAll(new Task[] { setTask, waitTask });

                Job jobCache1 = await cache.GetCacheAsync<Job>(key);
                Job jobCache2 = await cache.GetCacheAsync<Job>(key);

                Log.Information($"{"Data from cache1".PadRight(20)} [{key}] : {JsonSerializer.Serialize<Job>(jobCache1)}");
                Log.Information($"{"Data from cache2".PadRight(20)} [{key}] : {JsonSerializer.Serialize<Job>(jobCache2)}");

                Thread.Sleep(1000);

                //ShowAllDataInCache();

                if (!isRunningProcess)
                    break;
            }
        }

        private void ShowAllDataInCache()
        {
            IEnumerable<RedisKey> keys = multiplexer
                .GetServer(optionRedisCacheConfiguration.CurrentValue.ServerFullName)
                .Keys();

            var db = multiplexer.GetDatabase();
            string instance = optionRedisCacheConfiguration.CurrentValue.InstanceName;

            Log.Error($"============================ keys ==================================");

            foreach (var k in keys)
            {
                string cutKey = k.ToString().Replace(instance, string.Empty);
                Log.Error($"{k} => {cutKey} => {db.StringGet(new RedisKey(cutKey))}");
            }
        }

        #region IDisposable Support
        private bool isDisposed = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {

                    // TODO: dispose managed state (managed objects).
                    if (multiplexer != null)
                    {
                        ShowAllDataInCache();
                        multiplexer.Dispose();
                    }
                }

                // TODO: free unmanaged resources	
                isDisposed = true;
            }
        }

        ~WorkerService()
        {
            Log.Information($"Destructor of {this.GetType().Name} class is called");
            Dispose(false);
        }

        public void Dispose()
        {
            Log.Information($"Dispose() method of {this.GetType().Name} class is called");

            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
