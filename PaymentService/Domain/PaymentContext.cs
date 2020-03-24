using System;
using Microsoft.EntityFrameworkCore;

namespace PaymentService.Domain
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> opt) : base(opt) { }

        public DbSet<Payment> payment { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Order_details> order_Details { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<userModel> userModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Order_details>()
                .HasOne(x => x.orders)
                .WithMany()
                .HasForeignKey(x => x.Order_id);
            modelBuilder
                .Entity<Order_details>()
                .HasOne(x => x.products)
                .WithMany()
                .HasForeignKey(x => x.Product_id);
            modelBuilder
                .Entity<Orders>()
                .HasOne(x => x.users)
                .WithMany()
                .HasForeignKey(x => x.User_id);
        }

    }
}
