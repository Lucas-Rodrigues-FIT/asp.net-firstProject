using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.DataStore
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions op) : base(op)
        {
        }

        public DbSet<Pizza> pizzas { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Order> orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>()
                .HasMany(p => p.orderItem)
                .WithOne(o => o.pizza)
                .HasForeignKey(k => k.pizzaId);

            modelBuilder.Entity<Order>()
                .HasMany(i => i.orderItems)
                .WithOne(o => o.order)
                .HasForeignKey(f => f.orderId);

            //seeding
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { id = 1, name = "Bacon", isGlutenFree = false, price = 33.39 },
                new Pizza { id = 2, name = "3 queijos", isGlutenFree = false, price = 32.39},
                new Pizza { id = 3, name = "Italiana", isGlutenFree = true, price = 13.39 }
                );
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, quantity = 1, orderId = 1, pizzaId = 1 },
                new OrderItem { Id = 2, quantity = 2, orderId = 1, pizzaId = 2 }
                );
            modelBuilder.Entity<Order>().HasData(
                new Order { id = 1 }
                );
        }

    }
}
