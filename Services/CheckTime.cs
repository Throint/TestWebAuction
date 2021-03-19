using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestRazor.Model;
using static TestRazor.Model.Item;

namespace TestRazor.Services
{
    public class CheckTime : BackgroundService
    {
        private readonly AppData appData;
        public CheckTime(AppData data)
        {
            appData = data;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var i = appData.Items.FirstOrDefault(s => s.Id == 10);
         //   await Task.Delay(TimeSpan.FromSeconds(i.Time.Second), stoppingToken);
            var r = await appData.Items.FirstOrDefaultAsync(o => o.Id == i.Id);
            appData.Items.Remove(r);
        await    appData.SaveChangesAsync();
          //  throw new NotImplementedException();
        }
        
    }
}
