using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyRedis.Support;
using MyRedis.Model;
using System.Text.Json;

namespace MyRedis.Services
{
    public class WorkerService : IHostedService, IDisposable
    {
        public bool isRunningProcess = false;

        private IDistributedCache cache;
        public WorkerService(IDistributedCache cache)
        {
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

                Job jobCache = await cache.GetCacheAsync<Job>(key);

                Log.Information($"{"Data from cache".PadRight(20)}: {JsonSerializer.Serialize<Job>(jobCache)}");
                Thread.Sleep(1000);

                if (!isRunningProcess)
                    break;
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
