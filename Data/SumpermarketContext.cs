using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Models;

namespace SupermarketWEB.Data
{
    public class SupermarketContext : DbContext
    {
        public SupermarketContext(DbContextOptions options) : base(options)
        {
        }

<<<<<<< HEAD
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Category> Categories { get; set; }

        public DbSet<Provider> Providers { get; set; } = default!;
=======
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
>>>>>>> 2259ff3db980ad36a4da4cd4a04b056baa246be0
    }
}
