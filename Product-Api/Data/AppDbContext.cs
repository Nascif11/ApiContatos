using Product_Api.Model;
using Product_Api.Data;
using Microsoft.EntityFrameworkCore;
namespace Product_Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>().HasData(
            new Product { id = 1, ProductName = "Product 1", Price = 10 },
            new Product { id = 2, ProductName = "Product 2", Price = 20 },
            new Product { id = 3, ProductName = "Product 3", Price = 30 }
        );
    }
}    

    