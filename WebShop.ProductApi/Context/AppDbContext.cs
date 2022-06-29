using Microsoft.EntityFrameworkCore;
using WebShop.ProductApi.Models;

namespace WebShop.ProductApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories {get; set;}
    public DbSet<Product> Products {get; set;}
    
    //Fluent API
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Category
        builder.Entity<Category>().
                HasKey(c=> c.CategoryId);

        builder.Entity<Category>().
                Property(c => c.Name).
                HasMaxLength(100).
                IsRequired();

        //Product
        builder.Entity<Product>().
                Property(c => c.Name).
                HasMaxLength(100).
                IsRequired();

        builder.Entity<Product>().
                Property(c => c.Description).
                HasMaxLength(255).
                IsRequired();

        builder.Entity<Product>().
                Property(c => c.ImageURL).
                HasMaxLength(255).
                IsRequired();

        builder.Entity<Product>().
                Property(c => c.Price).
                HasPrecision(12,2).
                IsRequired();                

        builder.Entity<Category>().
                HasMany(g => g.Products).
                WithOne(c => c.Category).
                IsRequired().
                OnDelete(DeleteBehavior.Cascade);

        //Populate Category Table
        builder.Entity<Category>().
                HasData(
                    new Category
                    {
                        CategoryId = 1,
                        Name = "Material Escolar"
                    },
                    new Category
                    {
                        CategoryId = 2,
                        Name = "Acessórios"
                    }
                );
    }
}
