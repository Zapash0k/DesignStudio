using System.Collections.Generic;
using System.Reflection.Emit;
using DesignStudio.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesignStudio.DAL
{
    public class DesignStudioContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PortfolioItem> PortfolioItems { get; set; }
        public DbSet<OrderService> OrderServices { get; set; }

        public DesignStudioContext(DbContextOptions<DesignStudioContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Композитний ключ для зв'язкової таблиці
            modelBuilder.Entity<OrderService>()
                .HasKey(os => new { os.OrderId, os.ServiceId });

            modelBuilder.Entity<OrderService>()
                .HasOne(os => os.Order)
                .WithMany(o => o.OrderServices)
                .HasForeignKey(os => os.OrderId);

            modelBuilder.Entity<OrderService>()
                .HasOne(os => os.Service)
                .WithMany(s => s.OrderServices)
                .HasForeignKey(os => os.ServiceId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.PortfolioItem)
                .WithOne(p => p.Order)
                .HasForeignKey<PortfolioItem>(p => p.OrderId);

            modelBuilder.Entity<Service>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
