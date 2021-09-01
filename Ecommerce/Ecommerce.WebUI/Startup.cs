using Ecommerce.BL.Repositories;
using Ecommerce.DAL.DbContexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebUI
{
    public class Startup
    {
        IConfiguration configuration;
        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<MSSQLDBContext>(opt=>opt.UseSqlServer(configuration.GetConnectionString("CS1")));
            services.AddScoped(typeof(MSSQLRepo<>));//Dependency Injection Life Time: Singleton, Scoped, Trainsient

            //Dependency Injection (DI), Middleware

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt=> {
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                opt.LoginPath = "/admin/giris";
                opt.LogoutPath = "/";
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseStatusCodePagesWithReExecute("/hata/{0}");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name:"default",pattern:"{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(name: "areas",pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
            });
        }
    }
}
