using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestRazor.Model;
using TestRazor.Services;
using static TestRazor.Model.Item;

namespace TestRazor.Pages
{
    [Authorize]
    public class PayModel : PageModel
    {

        private readonly AppData appData;
        private readonly TestRazor.Services.EmailSendService emailSendService;

        public PayModel(AppData appData, EmailSendService emailSendService)
        {
            this.appData = appData;
            this.emailSendService = emailSendService;
        }

        [BindProperty]
        public decimal Value { get; set; }
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public Order Order { get; set; }

        public Item t { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
             t = await appData.Items.FirstOrDefaultAsync(i => i.Id == id);
            Value = t.RedemtionPrice;
            Name = t.Name;
         return Page();
        }

        public async Task OnPostAsync()
        {
           
            if(Order!=null)
            {


                if (User.Identity.IsAuthenticated)
                {
                    if (User.FindFirst(ClaimTypes.Sid)?.Value != Order.SellerId.ToString())
                    {
                        var EmailValue = User.FindFirst(ClaimTypes.Email)?.Value;
                        var user_buyer = await appData.Users.FirstOrDefaultAsync(i => i.EmailAddress == EmailValue);
                        await EmailSendService.SendEmailAsync(Order.SellerEmail, $"Item number {Request.Query["id"]}", $"Buyer {user_buyer.EmailAddress} {user_buyer.PhoneNumber}");
                        await EmailSendService.SendEmailAsync(user_buyer.EmailAddress, $"Web auction", $"You buy item number {Request.Query["id"]}  seller email: {Order.SellerEmail}  seller phone number{Order.SellerTel}");
                        var Item = await appData.Items.FirstOrDefaultAsync(i => i.Id == Order.ItemId);
                        Item.ItemWasRedempt = true;
                        Item.Status = "Ordered";

                    }
                }
                else
                {
                   await EmailSendService.SendEmailAsync(Order.SellerEmail, $"Item number {Request.Query["id"]}", $"Buyer {Order.BuyerEmail} {Order.BuyerTel}");
                    await EmailSendService.SendEmailAsync(Order.BuyerEmail, $"Web auction", $"You buy item number {Request.Query["id"]}  seller email: {Order.SellerEmail}  seller phone number{Order.SellerTel}");

                    var Item = await appData.Items.FirstOrDefaultAsync(i => i.Id == Order.ItemId);
                    Item.ItemWasRedempt = true;
                    Item.Status = "Ordered";
                }
            }
            await Task.Delay(1000);
        }
    }
}
