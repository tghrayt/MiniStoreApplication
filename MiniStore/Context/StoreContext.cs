using Microsoft.EntityFrameworkCore;
using MiniStore.Models;

namespace MiniStore.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.category);
            
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
        }
        
    }

}
