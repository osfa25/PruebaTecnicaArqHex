using Microsoft.EntityFrameworkCore;
using PruebaTecnicaArqHex.Models.Clients.domain.entities;
using PruebaTecnicaArqHex.Models.Invoices.domain.entities;
using PruebaTecnicaArqHex.Models.Products.domain.entities;

namespace PruebaTecnicaArqHex.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<Products> Products { get; set; } // Agregado
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; } // Agregado

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(c => c.name)
                      .IsRequired();

                entity.Property(c => c.email)
                      .IsRequired();
            });

            modelBuilder.Entity<Invoices>(entity =>
            {
                entity.HasOne(i => i.Client)
                      .WithMany()
                      .HasForeignKey(i => i.ClientID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasOne(id => id.Invoice)
                      .WithMany()
                      .HasForeignKey(id => id.InvoiceID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(id => id.Product)
                      .WithMany()
                      .HasForeignKey(id => id.ProductID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(p => p.name).IsRequired();
                entity.Property(p => p.price).IsRequired();
            });
        }
    }
}
