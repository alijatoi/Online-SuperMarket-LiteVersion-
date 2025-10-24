using Microsoft.EntityFrameworkCore;
using OnlineSuperMarket.Api.Models;

namespace OnlineSuperMarket.Api.Data;

public class SuperMarketDbContext : DbContext
{
    public SuperMarketDbContext(DbContextOptions<SuperMarketDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.UnitPrice)
            .HasColumnType("decimal(18,2)");

        // Seed data
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Fruits & Vegetables", Description = "Fresh fruits and vegetables" },
            new Category { Id = 2, Name = "Dairy Products", Description = "Milk, cheese, yogurt and more" },
            new Category { Id = 3, Name = "Bakery", Description = "Fresh bread and baked goods" },
            new Category { Id = 4, Name = "Beverages", Description = "Soft drinks, juices, and water" },
            new Category { Id = 5, Name = "Snacks", Description = "Chips, cookies, and other snacks" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Apple", Description = "Fresh red apples", Price = 2.99m, Stock = 100, CategoryId = 1, ImageUrl = "/images/apple.jpg" },
            new Product { Id = 2, Name = "Banana", Description = "Ripe yellow bananas", Price = 1.99m, Stock = 150, CategoryId = 1, ImageUrl = "/images/banana.jpg" },
            new Product { Id = 3, Name = "Milk", Description = "Fresh whole milk 1L", Price = 3.49m, Stock = 50, CategoryId = 2, ImageUrl = "/images/milk.jpg" },
            new Product { Id = 4, Name = "Cheese", Description = "Cheddar cheese block", Price = 5.99m, Stock = 30, CategoryId = 2, ImageUrl = "/images/cheese.jpg" },
            new Product { Id = 5, Name = "White Bread", Description = "Fresh white bread loaf", Price = 2.49m, Stock = 40, CategoryId = 3, ImageUrl = "/images/bread.jpg" },
            new Product { Id = 6, Name = "Orange Juice", Description = "Fresh squeezed orange juice 1L", Price = 4.99m, Stock = 60, CategoryId = 4, ImageUrl = "/images/oj.jpg" },
            new Product { Id = 7, Name = "Potato Chips", Description = "Classic salted chips", Price = 3.99m, Stock = 80, CategoryId = 5, ImageUrl = "/images/chips.jpg" },
            new Product { Id = 8, Name = "Carrots", Description = "Fresh organic carrots", Price = 2.29m, Stock = 70, CategoryId = 1, ImageUrl = "/images/carrots.jpg" }
        );
    }
}
