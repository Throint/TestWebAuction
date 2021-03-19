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
using TestRazor.Services;
using static TestRazor.Model.Item;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace TestRazor.Pages
{
    public class CreateUserModel : PageModel
    {
        private readonly AppData data;
        private Logger<CreateUserModel> _logger;
        EmailSendService EmailSendService;
        public CreateUserModel(AppData _data, EmailSendService emailSendService, Logger<CreateUserModel> logger)
        {
            data = _data;
            EmailSendService = emailSendService;
            this._logger = logger;
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
        
        public async Task<IActionResult> OnGetAsync()
        {
            await ConfirmEmail();
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
                        await ConfirmEmailCreate(user);
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

     
        public async Task<IActionResult> ConfirmEmail()
        {
            if (Request.Query.ContainsKey("token"))
            {
                string value = Request.Query["token"];
                //      string value_check = value.Replace(' ', '+');
                var ql = data.ConfirmTokens.ToList();
                for (int i = 0; i < ql.Count; i++)
                {
                    if (ql[i].Token == value)
                    {
                        TimeSpan timeSpan = new TimeSpan(0, ql[i].LifeTimeMin, 0);
                        var qw = ql[i].CreationDateTime + timeSpan;
                        if (DateTime.Compare(DateTime.Now, qw) <= 0)
                        {
                            var tmp = await data.Users.FirstOrDefaultAsync(u => u.Id == ql[i].PersonId);
                            tmp.EmailWasConfirmed = true;
                            data.Users.Update(tmp);
                            await data.SaveChangesAsync();
                        }
                        return RedirectToAction("Index", "Home");

                    }
                }

            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> ConfirmEmailCreate(User _person)
        {
            string str = User.FindFirst(ClaimTypes.Email)?.Value;
            if (str != null)
            {
                var q = await data.Users.FirstOrDefaultAsync(i => i.EmailAddress == str);


            }

            //more fast
            StringBuilder path = new StringBuilder(Request.Scheme);
            path.Append("://");
            path.Append(Request.Host.Value);
            path.Append("/User/ConfirmEmail");
            path.Append("/?token=");

            //  string s =Request.Scheme+"://"+ Request.Host.Value;
            ////  var allowedString = String.Concat(s.Select(i => i)) ;

            //  var tmp_res = s + "/User/ConfirmEmail";
            //  var ttt = tmp_res + "/?token=";
            var token = await EmailSendService.Token(_person);
            StringBuilder stringBuilder = new StringBuilder(token);
            stringBuilder.Replace("+", "%2B");
            path.Append(stringBuilder.ToString());



            await data.ConfirmTokens.AddAsync(new ConfirmToken()
            {
                CreationDateTime = DateTime.Now,
                Email = _person.EmailAddress,

                LifeTimeMin = 10,
                Token = token,
                PersonId = _person.Id

            });
            await data.SaveChangesAsync();

            var log_tmp = await EmailSendService.SendEmailAsync(_person.EmailAddress, "Confirm", path.ToString());
            _logger.LogInformation(log_tmp);

            return RedirectToAction("Login");
        }
    }
}

    

