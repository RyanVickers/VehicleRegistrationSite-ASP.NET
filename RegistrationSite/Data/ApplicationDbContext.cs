using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RegistrationSite.Models;

namespace RegistrationSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Vehicle>()
                           .HasOne(p => p.Driver)
                           .WithMany(c => c.Vehicles)
                           .HasForeignKey(p => p.DriverId)
                           .HasConstraintName("FK_Vehicles_DriverId");
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
