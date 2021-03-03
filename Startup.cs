using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TestRazor.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestRazor.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace TestRazor
{
    
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connect = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppData>(opt => opt.UseSqlServer(connect));
            services.AddRazorPages();
            services.AddTransient<HashService>();
            services.AddSingleton<TestS>();
     //       services.AddSingleton<AppData>();
            services.AddHostedService<ServiceT>();

          //  services.AddSingleton<IHostedService, RecureHostedService>();
            //services.AddSingleton(new TimerService());
            //services.AddHostedService<CheckTime>();




            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>

            {
                opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login/");
                    opt.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Logout/OnGetAsync");
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
