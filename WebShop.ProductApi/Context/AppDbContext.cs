using Microsoft.EntityFrameworkCore;
using WebShop.ProductApi.Models;

namespace WebShop.ProductApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories {get; set;}
    public DbSet<Product> Products {get; set;}
    
}
