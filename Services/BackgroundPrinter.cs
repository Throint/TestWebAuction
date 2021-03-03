using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using TestRazor.Model;

namespace TestRazor.Services
{
    public class BackgroundPrinter : IHostedService
    {
        private readonly ILogger<BackgroundPrinter> logger;
        private readonly ICheck worker;
        private readonly AppData appData;

        public BackgroundPrinter(ILogger<BackgroundPrinter> logger,
            ICheck worker, 
            AppData data)
        {
            this.logger = logger;
            this.worker = worker;
            this.appData = data;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await worker.DoWork(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Printing worker stopping");
            return Task.CompletedTask;
        }
    }
}
