using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApp.Data;
using WebApp.Infrastructure;
using WebApp.Services.Repairs;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CarRepairDbContext>(options => options
                .UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services
                .AddDatabaseDeveloperPageExceptionFilter();

            services
                .AddDefaultIdentity<IdentityUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<CarRepairDbContext>();

            services
                .AddControllersWithViews();

            //Service 
            services.AddTransient<IRepairService, RepairService>();


            //FACEBOOK 
            //services.AddAuthentication().AddFacebook(facebookOptions =>
            //{
            //    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDatabase();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection()
               .UseStaticFiles()
               .UseRouting()
               .UseAuthentication()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();

                //endpoints.MapControllerRoute(
                //    name: "services",
                //    pattern: "{controller=Services}/{action=Services}");
                //endpoints.MapControllerRoute(
                //    name: "contacts",
                //    pattern: "{controller=Contacts}/{action=Contacts}");

                //endpoints.MapControllerRoute(
                //    name: "cars",
                //    pattern: "{controller=Cars}/{action=Cars}");


                //endpoints.MapControllerRoute(
                //    name: "cars",
                //    pattern: "{controller=Cars}/{action=Details}/{id?}");


                //endpoints.MapControllerRoute(
                //       name: "repairs",
                //       pattern: "{controller=Repairs}/{action=Create}/{id?}/{carid?}");

                endpoints.MapControllerRoute(
                     name: "blog",
                     pattern: "/Article/{date}/{id}",
                     defaults: new { controller = "Blog", action = "Article" },
                     constraints: new { date = "[0-9]" });
                endpoints.MapRazorPages();
            });

            

        }
    }
}
