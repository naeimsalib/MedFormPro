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
                .Property(u => u.Id).HasColumnName("id");
            modelBuilder.Entity<User>()
                .Property(u => u.Username).HasColumnName("username");
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash).HasColumnName("password_hash");
            modelBuilder.Entity<User>()
                .Property(u => u.Email).HasColumnName("email");
            modelBuilder.Entity<User>()
                .Property(u => u.Role).HasColumnName("role");
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName).HasColumnName("first_name");
            modelBuilder.Entity<User>()
                .Property(u => u.LastName).HasColumnName("last_name");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configure Prescription entity
            modelBuilder.Entity<Prescription>()
                .Property(p => p.Id).HasColumnName("id");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.PatientName).HasColumnName("patient_name");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.MedicationName).HasColumnName("medication_name");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.Dosage).HasColumnName("dosage");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.Instructions).HasColumnName("instructions");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.CreatedDate).HasColumnName("created_date");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.CreatedBy).HasColumnName("created_by");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.Status).HasColumnName("status");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.ShipmentStatus).HasColumnName("shipment_status");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.ReviewedBy).HasColumnName("reviewed_by");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.ReviewedDate).HasColumnName("reviewed_date");
            modelBuilder.Entity<Prescription>()
                .Property(p => p.ReviewNotes).HasColumnName("review_notes");
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