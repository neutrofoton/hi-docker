using Microsoft.Extensions.Hosting;
using MyRedis.Caching.Extension;
using MyRedis.Caching.Repository;
using MyRedis.Model;
using Serilog;
using System.Text.Json;

namespace MyRedis.Services
{
    public class WorkerServiceWithRedisMultiplexer : IHostedService, IDisposable
    {
        public bool isRunningProcess = false;
        private IRedisRepository<Job> jobRedisRepository;

        public WorkerServiceWithRedisMultiplexer(IRedisRepository<Job> jobRedisRepository)
        {
            this.jobRedisRepository = jobRedisRepository;
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
            var key = $"{this.GetType().Name}:job";
            var job = new Job();

            while (true)
            {
                var now = DateTime.Now;
                job.Id = Guid.NewGuid().ToString();
                job.At = now;
                job.Name = $"Job at {now.ToString("dd-MMM-yyyy hh:mm:ss")}";


                //save job
                Task saveTask = jobRedisRepository.SaveAsync(key, job, TimeSpan.FromSeconds(30));

                Task waitTask = Task.Delay(1000);
                Task logTask = Task.Run(() => Log.Warning($"{"Save to cache".PadRight(20)}: {JsonSerializer.Serialize<Job>(job)}"));

                Task.WaitAll(new Task[] { saveTask, waitTask, logTask });

                Job jobCache1 = await jobRedisRepository.GetAsync(key);

                Log.Information($"{"Data from cache1".PadRight(20)} [{key}] : {JsonSerializer.Serialize<Job>(jobCache1)}");

                Thread.Sleep(1000);

                if (!isRunningProcess)
                    break;
            }
        }

        private void ShowAllDataInCache()
        {  
            var keys = jobRedisRepository.GetAllKeys();
            Log.Error($"============================ keys ==================================");

            foreach (var k in keys)
            {  
                Log.Error($"{k}");
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
                    ShowAllDataInCache();
                }

                // TODO: free unmanaged resources	
                isDisposed = true;
            }
        }

        ~WorkerServiceWithRedisMultiplexer()
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
