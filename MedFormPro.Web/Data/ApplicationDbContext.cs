using Microsoft.EntityFrameworkCore;
using MedFormPro.Web.Models;

namespace MedFormPro.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Guide> Guides { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configure Prescription entity
            modelBuilder.Entity<Prescription>()
                .Property(p => p.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Prescription>()
                .Property(p => p.ShipmentStatus)
                .HasConversion<string>();

            // Configure Guide entity
            modelBuilder.Entity<Guide>()
                .HasIndex(g => new { g.Category, g.DisplayOrder });
        }
    }
}