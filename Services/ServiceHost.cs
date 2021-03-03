using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestRazor.Model;

namespace TestRazor.Services
{

    //public class jjj
    //{
    //    private readonly BlockingCollection<TaskSettings> _tasks;

    //    public TasksToRun() => _tasks = new BlockingCollection<TaskSettings>();

    //    public void Enqueue(TaskSettings settings) => _tasks.Add(settings);

    //    public TaskSettings Dequeue(CancellationToken token) => _tasks.Take(token);
    //}
    public class RecureHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _log;
        private Timer _timer;
        private readonly AppData appData;
        public RecureHostedService(ILogger<RecureHostedService> log, AppData appData)
        {
            _log = log;
            this.appData = appData;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _log.LogInformation("RecureHostedService is Starting");
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _log.LogInformation("RecureHostedService is Stopping");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
                _log.LogInformation("Timed Background Service is working.");
        }
    }
}
