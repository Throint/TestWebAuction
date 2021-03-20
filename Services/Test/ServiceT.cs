using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestRazor.Model;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using Microsoft.AspNetCore.Html;
using MailKit.Net.Smtp;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using static TestRazor.Model.Item;

namespace TestRazor.Services
{
    
    public class ServiceT:IHostedService, IDisposable
    {
       
        private readonly TestS testS;

        private readonly ILogger _log;
        private Timer _timer;
        private readonly IServiceScopeFactory serviceScopeFactory;
    //    private readonly AppData appData;
      
        public ServiceT(TestS testS, IServiceScopeFactory serviceScopeFactory, ILogger<RecureHostedService> log)
        {
            this.testS = testS;
            //  this.appData = appData;
            this.serviceScopeFactory = serviceScopeFactory;
            this._log = log;
        }
        //public void DoWork()
        //{


        //  //  if(appData.Items.FirstOrDefaultAsync(i=>i.Id==testS.Id[0]).Result.Time
        //}

        public void Dispose()
        {
            _timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _log.LogInformation("RecureHostedService is Starting");
            //List<Timer> timers = new List<Timer>();
            //AppData appData;
            //appData.Items.AsParallel().ForAll((i) => { timers.Add(i)})
           _timer = new Timer(DoWork, null, TimeSpan.Zero.Minutes, TimeSpan.FromMinutes(1).Minutes);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _log.LogInformation("RecureHostedService is Stopping");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<AppData>();
                //for (int i = 0; i < testS.Id.Count; i++)
                //{

                //    var t = await dbcontext.Items.FirstOrDefaultAsync(o => o.Id == testS.Id[i]);
                //    if (t?.DateTimeEnd.CompareTo(DateTime.Now)>0)
                //    {
                //Item item = new Item()
                //{
                //    UserCreatedId = 999,
                //    BeginPrice = 29,
                //    Name = "Test"
                //};
                //dbcontext.Items.Add(item);




             
                      //await  dbcontext?.Items.ForEachAsync((async i => {await Task.Run(async() => {
                      //    if (DateTime.Now.CompareTo(i.DateTimeEnd) >= 0)
                      //    {
                      //        i.Status = "Expired";
                      //        if(i.BetWasDone && !i.ItemWasRedempt)
                      //        {
                      //            var buyer = await dbcontext.Users.FirstOrDefaultAsync(q => q.Id == i.LastBetUserId); 
                      //            var seller = await dbcontext.Users.FirstOrDefaultAsync
                      //            (q =>
                      //                JSONConvert<List<long>>.
                      //               GetIdListFromJSONString(q.ItemsList).Contains(i.Id)

                      //           );
                      //           if(seller!=null)
                      //            {
                      //              await  EmailSendService.SendEmailAsync(seller.EmailAddress, "WebAuction", $"Your item {i.Id} was ordered by {i.LastBetUserId}. Contact with " +
                      //                  $"him to detail order, email {buyer.EmailAddress}" +
                      //                  $"phone number {buyer.PhoneNumber}");
                      //            }


                                  //var q=await dbcontext.Users.FirstOrDefaultAsync(i=>i.)
                                  //EmailSendService.SendEmailAsync()
                                  
                                //  var u = await dbcontext.Users.FirstOrDefaultAsync(q => q.Id == i.LastBetUserId);
                                  
                                 //sonSerializer.Serialize()
                              }
                           //   dbcontext.Items.Update(i);
                 //         await    dbcontext.SaveChangesAsync();
                          }
                      }); }));
                   //     dbcontext.Items.Remove(t);
                     //   dbcontext.Items.Update(t);
                        await dbcontext.SaveChangesAsync();
                        _log.LogInformation("Deleted");
                //    }
                //}
                _log.LogInformation("Timed Background Service is working.");
            }
        }
    }


    public class EmailSendService
    {
        public static async Task<string> SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            var build = new HtmlContentBuilder();
            //    build.AppendFormat($"<html><a href>{message ")
            emailMessage.From.Add(new MailboxAddress("Admin", "new.test.user.newtest@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<html><body><a href='{message}'>{message}</a></body></html>"

            };


            using (var client = new SmtpClient())
            {
                string pass = String.Empty;
                try
                {
                    pass = File.ReadAllText(@"C:\source\Test\mail_test.txt");
                }
                catch (DirectoryNotFoundException ex)
                {
                    //  Logger.LogCritical($"{ex.Message}, {ex.Data}, {ex.Source}");
                    return await Task.Run(() => $"Directory not found \t {ex.Message}, {ex.Data}, {ex.Source}");

                }
                catch (FileNotFoundException ex)
                {
                    //  Logger.LogCritical($"{ex.Message}, {ex.Data}, {ex.Source}");
                    return await Task.Run(() => $"File not found \t {ex.Message}, {ex.Data}, {ex.Source} ");
                }
                await client.ConnectAsync("smtp.gmail.com", 465, true);
                await client.AuthenticateAsync("new.test.user.newtest@gmail.com", pass);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
                return await Task.Run(() => "Email was send");
            }
        }
        public async Task<string> Token(User user)
        {
            SHA512 sHA512_f = SHA512.Create();
            SHA512 sHA512_s = SHA512.Create();
            SHA512 sHA512_i = SHA512.Create();
            SHA512 sHA512_e = SHA512.Create();

            var r1 = Convert.ToBase64String(sHA512_f.ComputeHash(Encoding.ASCII.GetBytes(user.Id.ToString())));

            var r2 = Convert.ToBase64String(sHA512_s.ComputeHash(Encoding.ASCII.GetBytes(user.FirstName)));


            var r3 = Convert.ToBase64String(sHA512_i.ComputeHash(Encoding.ASCII.GetBytes(user.SecondName)));

            var r4 = Convert.ToBase64String(sHA512_e.ComputeHash(Encoding.ASCII.GetBytes(user.EmailAddress)));


            StringBuilder temp = new StringBuilder(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            StringBuilder temp_q = new StringBuilder(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            temp.Append(r1);
            temp.Append(r2);
            temp.Append(temp_q);
            temp.Append(r3);
            temp.Append(r4);
            temp.Replace("+", "%2B");//Url encode

            // string token = "ConfirmToken";

            return await Task.Run(() => temp.ToString());

        }

    }

    public class JSONConvert<T>
    {
        public static T GetIdListFromJSONString(string input)

        {
            return JsonSerializer.Deserialize<T>(input);
        }
       
        public static string GetJsonString(T obj)
        {


            return JsonSerializer.Serialize(obj);
        }
        public static byte[]GetJsonByteArray(T obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }
    }

    public static class Ext
    {
        public static List<long>GetListId(this string str)
        {
         return   JsonSerializer.Deserialize<List<long>>(str);
        }
    }
}
