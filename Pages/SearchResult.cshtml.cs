using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestRazor.Model;
using TestRazor.Services;
using Newtonsoft.Json;

namespace TestRazor.Pages
{
    public class SearchResultModel : PageModel
    {
        [BindProperty]
        public List<Item> Items { get; set; }
        public void OnGet()
        {
            if ((TempData["Found"] != null))
            {
             Items=   JsonConvert.DeserializeObject<List<Item>>(TempData["Found"].ToString());
            }
            else
            {
                Items = new List<Item>(); 
            }
        }
    }
}
