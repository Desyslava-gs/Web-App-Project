using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;
using WebApp.Data.Models;
using static WebApp.WebConstants;

namespace WebApp.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
          this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            MigrateDatabase(serviceProvider);

            SeedFuels(serviceProvider);
            SeedRepairs(serviceProvider);
            SeedAdmin(serviceProvider);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider service)
        {
            var data = service.GetRequiredService<CarRepairDbContext>();

            data.Database.Migrate();
        }

        private static void SeedRepairs(IServiceProvider service)
        {
            var data = service.GetRequiredService<CarRepairDbContext>();

            if (data.RepairTypes.Any())
            {
                return;
            }

            data.RepairTypes.AddRange(new[]
            {
                new RepairType {Name = "Replacement Of Consumables"},
                new RepairType {Name = "Repair"},
            });

            data.SaveChanges();
        }

        private static void SeedFuels(IServiceProvider service)
        {
            var data = service.GetRequiredService<CarRepairDbContext>();

            if (data.FuelTypes.Any())
            {
                return;
            }

            data.FuelTypes.AddRange(new[]
            {
                new FuelType {Name = "PETROL"},
                new FuelType {Name = "DIESEL"},
                new FuelType {Name = "GAS"},
                new FuelType {Name = "METHANE"},
                new FuelType {Name = "ELECTRIC"},
                new FuelType {Name = "HYBRID"},

            });

            data.SaveChanges();
        }
        private static void SeedAdmin(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdminRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdminRoleName };
                await roleManager.CreateAsync(role);

                const string adminEmail = "admin@marserviz.com";
                const string adminPass = "123456";
                var user = new User
                {
                    Email =adminEmail ,
                    UserName =adminEmail,
                    NameUser = "admin"
                };
                await userManager.CreateAsync(user, adminPass);

                await userManager.AddToRoleAsync(user, role.Name);

            })
                .GetAwaiter()
                .GetResult();
        }
    }
}
