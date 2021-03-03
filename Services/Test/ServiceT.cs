using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestRazor.Model;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestRazor.Services
{
    public class ServiceT:IHostedService, IDisposable
    {
        private readonly TestS testS;

        private readonly ILogger _log;
        private Timer _timer;
        private readonly IServiceScopeFactory serviceScopeFactory;
    //    private readonly AppData appData;
      
        public ServiceT(TestS testS, IServiceScopeFactory serviceScopeFactory, ILogger<RecureHostedService> log)
        {
            this.testS = testS;
            //  this.appData = appData;
            this.serviceScopeFactory = serviceScopeFactory;
            this._log = log;
        }
        //public void DoWork()
        //{


        //  //  if(appData.Items.FirstOrDefaultAsync(i=>i.Id==testS.Id[0]).Result.Time
        //}

        public void Dispose()
        {
            _timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _log.LogInformation("RecureHostedService is Starting");
            //List<Timer> timers = new List<Timer>();
            //AppData appData;
            //appData.Items.AsParallel().ForAll((i) => { timers.Add(i)})
           _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _log.LogInformation("RecureHostedService is Stopping");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<AppData>();
                //for (int i = 0; i < testS.Id.Count; i++)
                //{

                //    var t = await dbcontext.Items.FirstOrDefaultAsync(o => o.Id == testS.Id[i]);
                //    if (t?.DateTimeEnd.CompareTo(DateTime.Now)>0)
                //    {
                //Item item = new Item()
                //{
                //    UserCreatedId = 999,
                //    BeginPrice = 29,
                //    Name = "Test"
                //};
                //dbcontext.Items.Add(item);
             
                      await  dbcontext.Items.ForEachAsync((async i => {await Task.Run(async() => {
                          if (DateTime.Now.CompareTo(i.DateTimeEnd) >= 0)
                          {
                              i.Status = "Checked";
                              if(i.BetWasDone)
                              {
                                  var u = await dbcontext.Users.FirstOrDefaultAsync(q => q.Id == i.LastBetUserId);
                                  
                                 //sonSerializer.Serialize()
                              }
                           //   dbcontext.Items.Update(i);
                 //         await    dbcontext.SaveChangesAsync();
                          }
                      }); }));
                   //     dbcontext.Items.Remove(t);
                     //   dbcontext.Items.Update(t);
                        await dbcontext.SaveChangesAsync();
                        _log.LogInformation("Deleted");
                //    }
                //}
                _log.LogInformation("Timed Background Service is working.");
            }
        }
    }

}
