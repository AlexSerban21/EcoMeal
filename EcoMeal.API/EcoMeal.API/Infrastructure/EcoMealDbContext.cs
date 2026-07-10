using EcoMeal.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.API.Infrastructure;

public class EcoMealDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public EcoMealDbContext (DbContextOptions<EcoMealDbContext> options) 
        : base (options)
    { }
    public DbSet<BusinessType> BusinessTypes { get; set;}
    public DbSet<PackageType> PackageTypes { get; set;}
    public DbSet<Business> Businesses { get; set;}
    public DbSet<Package> Packages { get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<BusinessType>().HasKey(e => e.Id);
        //modelBuilder.Entity<PackageType>().HasKey(e => e.Id);
        //modelBuilder.Entity<Business>().HasKey(e => e.Id);
        //modelBuilder.Entity<Package>().HasKey (e => e.Id);
        //modelBuilder.Entity<User>().HasKey(e => e.Id);
        //modelBuilder.Entity<Order>().HasKey (e => e.Id);


        modelBuilder.Entity<Package>()
            .Property(p => p.Price)
            .HasPrecision(10, 2);

        //Business
        modelBuilder.Entity<Business>()
            .HasOne (p => p.BusinessType)
            .WithMany(p => p.Businesses)
            .HasForeignKey(p => p.BusinessTypeId);


        //Package
        modelBuilder.Entity<Package>()
            .HasOne (p => p.PackageType)
            .WithMany(p => p.Packages)
            .HasForeignKey(p => p.PackageTypeId);

        modelBuilder.Entity<Package>()
            .HasOne(p => p.Business)
            .WithMany(p => p.Packages)
            .HasForeignKey(p => p.BusinessId);


        //orders 

        modelBuilder.Entity<Order>()
            .HasOne(p => p.Package)
            .WithMany(p => p.Orders)
            .HasForeignKey(p => p.PackageId);
    }
}