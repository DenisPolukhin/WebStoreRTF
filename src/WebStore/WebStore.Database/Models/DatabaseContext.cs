using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebStore.Database.Models.Entities;

namespace WebStore.Database.Models;

public class DatabaseContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<ProductPriceHistory> ProductPriceHistories => Set<ProductPriceHistory>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<ProductInOrder> ProductInOrder => Set<ProductInOrder>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Order>()
            .HasMany(o => o.Products)
            .WithMany(p => p.Orders)
            .UsingEntity<ProductInOrder>(
                j => j
                    .HasOne(po => po.Product)
                    .WithMany(p => p.ProductInOrders)
                    .HasForeignKey(po => po.ProductId),
                j => j
                    .HasOne(po => po.Order)
                    .WithMany(o => o.ProductsInOrder)
                    .HasForeignKey(po => po.OrderId));

        builder.Entity<Order>()
            .HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId);

    }
}