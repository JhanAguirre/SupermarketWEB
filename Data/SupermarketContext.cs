using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Models;

namespace SupermarketWEB.Data
{
    public class SupermarketContext : DbContext
    {
        public SupermarketContext(DbContextOptions<SupermarketContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = default!; 
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<PayMode> PayModes { get; set; } = default!;

        public DbSet<Provider> Providers { get; set; } = default!;

        public DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }   
}