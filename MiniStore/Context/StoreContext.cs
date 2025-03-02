using Microsoft.EntityFrameworkCore;
using MiniStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStore.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options)
            : base(options)
        {

        }

        public StoreContext()
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
