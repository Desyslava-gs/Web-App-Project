using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Models;
using WebApp.Models.Cars;

namespace WebApp.Data
{
    public class CarRepairDbContext : IdentityDbContext
    {
        public CarRepairDbContext(DbContextOptions<CarRepairDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<RepairType> RepairTypes { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Provider> Providers { get; set; }
       

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Car>()
                .HasOne(ft=>ft.FuelType)
                .WithMany(ft => ft.Cars)
                .HasForeignKey(ft=>ft.FuelTypeId)
                .OnDelete(deleteBehavior:DeleteBehavior.Restrict);

            builder.Entity<Repair>()
                .HasOne(c=>c.Car)
                .WithMany(c => c.Repairs)
                .HasForeignKey(c=>c.CarId)
                .OnDelete(deleteBehavior:DeleteBehavior.Restrict);

            builder.Entity<Repair>()
                .HasOne(rt=>rt.RepairType)
                .WithMany(rt => rt.Repairs)
                .HasForeignKey(rt=>rt.RepairTypeId)
                .OnDelete(deleteBehavior:DeleteBehavior.Restrict);

            builder.Entity<Part>()
                .HasOne(r=>r.Repair)
                .WithMany(r => r.Parts)
                .HasForeignKey(r=>r.RepairId)
                .OnDelete(deleteBehavior:DeleteBehavior.Restrict);

            builder.Entity<Part>()
                .HasOne(p=>p.Provider)
                .WithMany( p=> p.Parts)
                .HasForeignKey(p=>p.ProviderId)
                .OnDelete(deleteBehavior:DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
        
    }
}
