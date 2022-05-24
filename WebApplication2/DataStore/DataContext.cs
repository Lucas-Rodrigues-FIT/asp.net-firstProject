using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.DataStore
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions op) : base(op)
        {
        }

        public DbSet<Pizza> pizzas { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Order> orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>()
                .HasOne(p => p.orderItem)
                .WithOne(o => o.pizza);


            modelBuilder.Entity<Order>()
                .HasMany(i => i.Items)
                .WithOne(o => o.order)
                .HasForeignKey(f => f.orderId);
            

            //seeding
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { id = 1, name = "Pizza Bacon", isGlutenFree = false, price = 33.39, orderItemId = 1 },
                new Pizza { id = 2, name = "Pizza 3 queijos", isGlutenFree = false, price = 32.39, orderItemId = 2 },
                new Pizza { id = 3, name = "Pizza Italiana", isGlutenFree = false, price = 13.39 }
                );
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, quantity = 1, orderId = 1 },
                new OrderItem { Id = 2, quantity = 2, orderId = 1 }
                );
            modelBuilder.Entity<Order>().HasData(
                new Order { id = 1 }
                );
        }
    }
}
