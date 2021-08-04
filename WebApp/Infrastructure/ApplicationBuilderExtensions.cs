using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;
using WebApp.Data.Models;

namespace WebApp.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
          this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var data = scopedServices.ServiceProvider.GetService<CarRepairDbContext>();

            data.Database.Migrate();

            SeedFuels(data);  SeedRepairs(data);
            return app;
        }

        private static void SeedRepairs(CarRepairDbContext data)
        {
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
        private static void SeedFuels(CarRepairDbContext data)
        {
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

    }
}
