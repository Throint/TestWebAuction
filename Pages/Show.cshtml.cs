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
using static TestRazor.Model.Item;

namespace TestRazor.Pages
{
    public class ShowModel : PageModel
    {
        private readonly AppData appData;
        public ShowModel(AppData data)
        {

            appData = data;
        }
        [BindProperty]
     public   List<Item> Items { get; set; }
        [BindProperty]
        public List<User> _Users { get; set; }

        [BindProperty]
        public string ToFound { get; set; }
        [BindProperty]
        public List<string>h { get; set; }
        public void OnGet()
        {
            if (appData.Items != null)
            {
                Items = appData.Items.ToList();
                
            }
            if(appData.Users!=null)
            {
                _Users = appData.Users.ToList();
            }
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                u();
            }


        }


        public void u()
        {
            h = new List<string>();
            //    var claim = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            // var ttt=   claim?.FirstOrDefault().Type;
            //  dynamic test=  Activator.CreateInstance(Type.GetType(ttt));
            h.Add(HttpContext.User.FindFirst(ClaimTypes.Name)?.Value);
            h.Add(HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);
            h.Add(HttpContext.User.FindFirst(ClaimTypes.Email)?.Value);
           
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ToFound!=null)
            {
              
                var seek = appData.Items.Where(i => i.Name.Contains(ToFound)).Select(i => i).ToList();

                ViewData["Found"] = seek;
                
            
            }
            return RedirectToPage("SearchResult");
        }
    }
}
