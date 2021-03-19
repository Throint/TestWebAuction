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
using TestRazor.Services;
using static TestRazor.Model.Item;

namespace TestRazor.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppData data;
        public LoginModel(AppData _data)
        {
            data = _data;
        }
        [BindProperty]
        public string EmailLogin { get; set; }
        [BindProperty]
        public string PassLogin { get; set; }
        public IActionResult OnGet()
        {
            return Page();

        }
        public async Task<IActionResult>OnPostAsync(string returnUrl)
        {
          return   await Login(returnUrl);
        }


        public async Task<IActionResult> Login(string returnUrl )
        {
            if (EmailLogin != null && PassLogin != null)
            {
               //string url= Request.Headers["Refer"]

                var __user = await data.Users.FirstOrDefaultAsync(i => i.EmailAddress == EmailLogin);
                if(__user!=null)
                {
                    var temp_pass = PassLogin;
                    var tmp_s = Convert.FromBase64String(__user.Salt);
                    var tmp_res = HashService.GetHashStr(temp_pass, tmp_s, 10000);
                    if (tmp_res == __user.HashPass)
                    {
                        await Authenticate(EmailLogin, __user.Id.ToString(), __user.FirstName ); // аутентификация

                        TempData["Name"] = EmailLogin;
                   //     Object redirectURLObject = request.getSession().getAttribute(REDIRECT_URL_SESSION_ATTRIBUTE_NAME);
                      if(!string.IsNullOrEmpty(returnUrl))
                        {
                             return RedirectToPage(returnUrl);
                        }
                        else
                        {
                            return RedirectToPage("/Show");
                        }
                       
                    }
                    else
                    {
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                    }
                }

            
            }
            return Page();
        }

        private async Task Authenticate(string Email, string Id, string name)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, Email),
                  
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Sid, Id),
                   

            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Login", "User");
        //}


    }
}
