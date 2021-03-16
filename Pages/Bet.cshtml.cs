using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestRazor.Model;
using System.Text.Json;
using TestRazor.Services;

namespace TestRazor.Pages
{
    public class BetModel : PageModel
    {
        private readonly AppData appData;
        [BindProperty]

        public Item Item { get; set; }

        [BindProperty]
        public Order CurOrder { get; set; } = new Order();

        [BindProperty]
        public decimal BetValue { get; set; }
        public BetModel(AppData app)
        {
            appData = app;
        }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            Item = await appData.Items.FirstOrDefaultAsync(i => i.Id == id);
            return Page();
        }
        public async Task<IActionResult>OnPostAsync(long id)
        {

          var temp=await  appData.Users.FirstOrDefaultAsync(i => i.Id == Item.UserCreatedId);
            CurOrder.SellerEmail = temp.EmailAddress;
            CurOrder.SellerId = temp.Id;
            

            if (BetValue >= Item.BeginPrice)
            {
                Item.BeginPrice = BetValue;
                Item.BetWasDone = true;
                appData.Items.Update(Item);
                //if(CurOrder!=null && Item.)
                //{
                //  await  appData.Orders.AddAsync(CurOrder);
                //}
                //await appData.SaveChangesAsync();
                //freeze money on card until lot end 
                //or  input only winner's card 

                if(User.Identity.IsAuthenticated)
                {
                 var i=   User.FindFirst(ClaimTypes.Sid)?.Value;
                    long sid;
                    long.TryParse(i, out sid);
                    var q = await appData.Users.FirstOrDefaultAsync(i => i.Id == sid);

                    q.OrdersBetId.Concat(Request.Query["id"].ToString());
                    appData.Users.Update(q);
                    await appData.SaveChangesAsync();
                }
                return RedirectToPage("Show");
            }
            else
            {
                TempData["Error"] = "Bet value should be greater or equal to begin price";
                return Page();
            }

           

        }
    }
}
