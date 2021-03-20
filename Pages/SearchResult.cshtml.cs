using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestRazor.Model;
using TestRazor.Services;

namespace TestRazor.Pages
{
    public class SearchResultModel : PageModel
    {
        [BindProperty]
        public List<Item> Items { get; set; }
        public void OnGet()
        {
            if((ViewData["Found"]!=null)&& ViewData["Found"] is List<Item>)
            Items = ViewData["Found"] as List<Item>;
        }
    }
}
