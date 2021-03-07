using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestRazor.Model;

namespace TestRazor.Pages
{
    public class PayModel : PageModel
    {

        private readonly AppData appData;

        public PayModel(AppData appData)
        {
            this.appData = appData;
        }

        [BindProperty]
        public decimal Value { get; set; }
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public Order Order { get; set; }
        public async Task OnGetAsync(long id)
        {
            var t = await appData.Items.FirstOrDefaultAsync(i => i.Id == id);
            Value = t.RedemtionPrice;
            Name = t.Name;
            Page();
        }

        public async Task OnPostAsync()
        {
            await Task.Delay(1000);
        }
    }
}
