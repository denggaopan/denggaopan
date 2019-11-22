using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Denggaopan.GraphqlDemo.Entities
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Item> Items { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Role>().ToTable("Role");

            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Item>().HasKey(p => p.Barcode);

            modelBuilder.Entity<Item>().HasData(new Item
            {
                Barcode = "123",
                Title = "Headphone",
                SellingPrice = 50m
            });

            modelBuilder.Entity<Item>().HasData(new Item
            {
                Barcode = "456",
                Title = "Keyboard",
                SellingPrice = 40m
            });
            modelBuilder.Entity<Item>().HasData(new Item
            {
                Barcode = "789",
                Title = "Monitor",
                SellingPrice = 100m
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
