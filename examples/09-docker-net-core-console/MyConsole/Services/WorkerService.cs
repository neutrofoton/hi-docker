using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyConsole.Infrastructure;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MyConsole.Services
{
    public class WorkerService : IHostedService, IDisposable
    {
        public bool isRunningProcess=false;       
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Thread t = new Thread(new ThreadStart(Do));

            isRunningProcess = true;
            t.Start();
           
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            isRunningProcess=false;

            Log.Information($"ThreadProc is stopped at: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");

            return Task.CompletedTask;
        }

       
        private void Do()
        {
            while (true)
            {
                Log.Information($"ThreadProc at: {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");
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
