using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure OrderItem as owned entity
            modelBuilder.Entity<Order>()
                .OwnsMany(o => o.Items, a =>
                {
                    a.WithOwner().HasForeignKey("OrderId"); // FK to Order
                    a.Property<Guid>("Id"); // Shadow primary key
                    a.HasKey("Id");         // Set primary key
                });

            modelBuilder.Entity<Order>()
    
                .Property(o => o.TotalPrice)
    
                .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .OwnsMany(o => o.Items, a =>
                {
                    a.Property(i => i.Price).HasPrecision(18, 2);
                });
        }
    }
}
