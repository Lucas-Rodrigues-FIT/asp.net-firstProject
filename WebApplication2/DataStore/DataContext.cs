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
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Items);
            modelBuilder.Entity<OrderItem>().HasOne(i => i.pizza);

            //seeding
            modelBuilder.Entity<Pizza>().HasData(
                new Pizza { id = 1, name = "Pizza Bacon", isGlutenFree = false, price = 33.39 },
                new Pizza { id = 2, name = "Pizza 3 queijos", isGlutenFree = false, price = 32.39 },
                new Pizza { id = 3, name = "Pizza Italiana", isGlutenFree = false, price = 13.39 }
                );

            
        }
    }
}
