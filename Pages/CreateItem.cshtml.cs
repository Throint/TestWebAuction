using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestRazor.Model;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Globalization;
using TestRazor.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace TestRazor.Pages
{
   
    [Authorize]
    public class CreateItemModel : PageModel
    {
        private readonly ILogger<CreateItemModel> _logger;
        private readonly AppData appData;
      //  private readonly IHostedService _recureHostedService;
        //     public Services.CheckTime CheckTime;
        private readonly TestRazor.Services.TestS testS;
        public CreateItemModel(ILogger<CreateItemModel> logger, AppData data, TestS testS)
        {
           // CheckTime = checkTime;
            _logger = logger;
            this.testS = testS;
            appData = data;
           // _recureHostedService = recureHostedService;
        }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Desription { get; set; }
        [BindProperty]
        public decimal BeginPrice { get; set; }
        [BindProperty]
        public decimal RedemtPrice { get; set; }
        [BindProperty]
        public string Category { get; set; }
        [BindProperty]
        public string Time { get; set; }

        public List<int> TimeWork { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

       // [OperationBehavior(AutoDisposeParameters = false)]
        public async Task<IActionResult> OnPostAsync()
        {
         string tmp_id=   User.FindFirst(ClaimTypes.Sid).Value;

            long id = long.Parse(tmp_id);
                //   var name = Request.Form["ItemName"].ToString();
            // var desription = Request.Form["ItemDesription"].ToString();
            if (this.Name != null && this.Desription!=null)
            {

                DateTime dateTime = DateTime.ParseExact(this.Time,"ss", CultureInfo.InvariantCulture);
                DateTime dateTime1 = DateTime.Now;

                TimeSpan timeSpan = new TimeSpan(0, 0, 0, dateTime.Second, 0);
              DateTime dateTimeResult=  dateTime1.AddSeconds(timeSpan.TotalSeconds);
                Item item = new Item()
                {
                    Name = Name,
                    Describe = this.Desription, BeginPrice = this.BeginPrice,
                    RedemtionPrice = this.RedemtPrice,
                    Category = this.Category,
                    UserCreatedId = id,
                    DurationTime = dateTime,
                    DateTimeBegin = dateTime1,
                  DateTimeEnd = dateTimeResult,
                  Status="Started"
                   


                };
                //TimeWork.Add(item.Id);
                await appData.Items.AddAsync(item);
                await appData.SaveChangesAsync();
                testS.Id.Add(item.Id);

                //    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(item.Time.Second);
                //   await _recureHostedService.StartAsync(new CancellationToken());
                //   await Task.Delay(TimeSpan.FromSeconds(item.Time.Second));
                //   await TimeDelay(item);
                //         appData.Items.Remove(item);
                //    
                //   await   appData.SaveChangesAsync();

                //   await CheckTime.StartAsync();
              //  TimeSpan timeSpan = new TimeSpan(0, 0, item.Time.Second);
                

              //await  Task.Factory.StartNew(async() =>
              //  {
              //      await Task.Delay((int)timeSpan.TotalMilliseconds).ContinueWith((i) => appData.Items.Remove(item));
              //      return RedirectToPage("Show");

                      
              //  });
                return Redirect("Show");
            }
            return Page();
        }
        //public async Task TheMethod()
        //{
            

        //}

        public async Task TimeDelay(Item item)
        {
          await  Task.Delay(TimeSpan.FromSeconds(item.DateTimeEnd.Second));
        }

    }
}
