using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PruebaTreda.Entities
{
    public partial class pruebaContext : DbContext
    {
        public pruebaContext()
        {

        }

        public DbSet<Tienda> Tiendas { get; set; }
        public DbSet<Producto> Productos { get; set; }

        public pruebaContext(DbContextOptions<pruebaContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);

            modelBuilder.Entity<Producto>().HasIndex(p => p.SKU)
            .IsUnique();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
