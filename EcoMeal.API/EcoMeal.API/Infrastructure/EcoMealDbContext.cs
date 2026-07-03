using EcoMeal.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.API.Infrastructure;

public class EcoMealDbContext : DbContext
{
    public EcoMealDbContext (DbContextOptions<EcoMealDbContext> options) 
        : base (options)
    { }
    public DbSet<User> Users { get; set;}
    public DbSet<BusinessType> BusinessType { get; set;}
    public DbSet<PackageType> PackageTypes { get; set;}
    public DbSet<Business> Businesses { get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Business>().HasKey(e => e.Id);
        modelBuilder.Entity<Business>()
            .HasOne (p => p.BusinessType)
            .WithMany(p => p.Businesses)
            .HasForeignKey(p => p.BusinessTypeId);



        base.OnModelCreating(modelBuilder);
    }
}