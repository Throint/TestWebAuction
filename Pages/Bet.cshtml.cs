using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestRazor.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestRazor.Pages
{
    public class BetModel : PageModel
    {
        private readonly AppData appData;
        [BindProperty]

        public Item Item { get; set; }

        [BindProperty]
        public decimal BetValue { get; set; }
        public BetModel(AppData app)
        {
            appData = app;
        }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            Item = await appData.Items.Include(i => i).FirstOrDefaultAsync(i => i.Id == id);
            return Page();
        }
        public async Task<IActionResult>OnPostAsync(long id)
        {
            if (BetValue >= Item.BeginPrice)
            {
                Item.BeginPrice = BetValue;
                Item.BetWasDone = true;
                appData.Items.Update(Item);
                await appData.SaveChangesAsync();
                //freeze money on card until lot end 
                //or  input only winner's card 
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
