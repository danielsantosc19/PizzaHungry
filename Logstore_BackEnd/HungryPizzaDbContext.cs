using Logstore_BackEnd.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logstore_BackEnd
{
    public class HungryPizzaDbContext : DbContext
    {

        public DbSet<Client> Clients { get; set; }
        public DbSet<Flavor> Flavors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }

        public HungryPizzaDbContext(DbContextOptions<HungryPizzaDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public HungryPizzaDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("HungryPizzaDb");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
            .HasMany(c => c.Items)
            .WithOne(e => e.Order)
            .IsRequired();

            modelBuilder.Entity<Flavor>().HasData(
                new Logstore_BackEnd.Model.Flavor()
                {
                    Id = 1,
                    Name = "3 Queijos",
                    Price = 50m
                },
                new Logstore_BackEnd.Model.Flavor()
                {
                    Id = 2,
                    Name = "Frango com requeijão",
                    Price = 59.99m
                },
                new Logstore_BackEnd.Model.Flavor()
                {
                    Id = 3,
                    Name = "Mussarela",
                    Price = 42.50m
                },
                new Logstore_BackEnd.Model.Flavor()
                {
                    Id = 4,
                    Name = "Calabresa",
                    Price = 42.50m
                },
                new Logstore_BackEnd.Model.Flavor()
                {
                    Id = 5,
                    Name = "Pepperoni",
                    Price = 55m
                },
                new Logstore_BackEnd.Model.Flavor()
                {
                    Id = 6,
                    Name = "Portuguesa",
                    Price = 45m
                },
                new Logstore_BackEnd.Model.Flavor()
                {
                    Id = 7,
                    Name = "Veggie",
                    Price = 45m
                });

            modelBuilder.Entity<Client>().HasData(
                new Client()
                {
                    Id = 1,
                    Name = "Daniel Castro",
                    Address = "Estrada do pontal 3820 casa 26"
                });

            modelBuilder.Entity<Client>().HasData(
                new Client()
                {
                    Id = 2,
                    Name = "Daniel 2",
                    Address = "Estrada do pontal 3820 casa 26"
                });


            base.OnModelCreating(modelBuilder);
            
        }
    }
}
