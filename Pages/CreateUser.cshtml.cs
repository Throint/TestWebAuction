using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestRazor.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace TestRazor.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly AppData data;
        public CreateUserModel(AppData _data)
        {
            data = _data;
        }
        [BindProperty]
        public string FName { get; set; }
        [BindProperty]
        public string Input_pass {  get;  set; }
        [BindProperty]
        public string Input_pass_2 { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string SName { get; set; }
        
        public IActionResult OnGet()
        {
            return Page();   
        }
        public async Task<IActionResult> OnPostAsync()
        {
            Model.User user = new User();
            if (Input_pass == Input_pass_2)
            {
                user.FirstName = FName;
                user.SecondName = SName;
                user.EmailAddress = Email;

                if (ModelState.IsValid)
                {

                    if (data.Users.Where(i => i.EmailAddress == Email).Count() != 0)
                    {
                        ViewData["Error"] = "Email is already used";
                        //  return View();
                    }

                    //Person person = new Person
                    //{
                    //    FirstName = crUser.FirstName,
                    //    SecondName = crUser.SecondName,
                    //    ThirdName = crUser.ThirdName,
                    //    Email = crUser.Email,
                    //    _DateTime = crUser._DateTime
                    //};

                    var in_pass = Input_pass;

                    var Rdgn = RandomNumberGenerator.Create();
                    byte[] temparr = new byte[128];
                    Rdgn.GetNonZeroBytes(temparr);
                    byte[] temp = Encoding.ASCII.GetBytes(in_pass);
                    SHA512 sha512 = SHA512.Create();
                    for (int i = 0; i < 10000; i++)
                    {
                        var lx = sha512.ComputeHash(temp);

                        var t_res = lx.ToList().Concat(temparr);
                        temp = sha512.ComputeHash(t_res.ToArray());
                    }
                    user.HashPass = Convert.ToBase64String(temp);
                    user.Salt = Convert.ToBase64String(temparr);

                    if (data.Users.Where(i => i == user).Count() == 0)
                    {
                        await data.Users.AddAsync(user);
                        await data.SaveChangesAsync();
                    }

                    try
                    {
                        return RedirectToPage("/Show");
                    }
                    catch
                    {
                        return Page();
                    }
                }
                else return Page();
            }

            return Page();
            


        }
    }
}
