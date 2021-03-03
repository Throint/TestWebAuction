using Microsoft.Extensions.Hosting;
using System.Threading;

using System.Threading.Tasks;

namespace TestRazor.Services
{
    public class DerivedBackgroundPrinter : BackgroundService
    {
        private readonly ICheck worker;

        public DerivedBackgroundPrinter(ICheck worker)
        {
            this.worker = worker;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await worker.DoWork(stoppingToken);    
        }
    }
}
