using CourseBD.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace CourseBD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Material> Materials { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComposition> ProductCompositions { get; set; }
        public DbSet<TechProcess> TechProcesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Составной первичный ключ для ProductComposition
            modelBuilder.Entity<ProductComposition>()
                .HasKey(pc => new { pc.ProductId, pc.ComponentId });

            // Самоссылающаяся связь Component (TechProcessRef)
            modelBuilder.Entity<Component>()
                .HasOne(c => c.ParentComponent)
                .WithMany(c => c.ChildComponents)
                .HasForeignKey(c => c.TechProcessRef)
                .OnDelete(DeleteBehavior.Restrict);

            // Индексы
            modelBuilder.Entity<Component>()
                .HasIndex(c => c.Name)
                .HasDatabaseName("IX_Components_Name");

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .HasDatabaseName("IX_Products_Name");

            modelBuilder.Entity<ProductComposition>()
                .HasIndex(pc => pc.ProductId)
                .HasDatabaseName("IX_ProductComposition_ProductId");

            modelBuilder.Entity<TechProcess>()
                .HasIndex(t => t.ComponentId)
                .HasDatabaseName("IX_TechProcess_ComponentId");

            modelBuilder.Entity<TechProcess>()
                .HasIndex(t => t.MaterialId)
                .HasDatabaseName("IX_TechProcess_MaterialId");

            modelBuilder.Entity<TechProcess>()
                .HasIndex(t => t.OperationId)
                .HasDatabaseName("IX_TechProcess_OperationId");

            // Уникальность в TechProcess
            modelBuilder.Entity<TechProcess>()
                .HasIndex(t => new { t.ComponentId, t.MaterialId, t.OperationId })
                .IsUnique()
                .HasDatabaseName("IX_TechProcess_Unique");

            // Ограничения CHECK (через Fluent API)
            modelBuilder.Entity<Material>()
                .ToTable(t => t.HasCheckConstraint("CK_Material_Price", "\"Price\" >= 0"));

            modelBuilder.Entity<Operation>()
                .ToTable(t => t.HasCheckConstraint("CK_Operation_HourlyRate", "\"HourlyRate\" >= 0"));
            modelBuilder.Entity<Operation>()
                .ToTable(t => t.HasCheckConstraint("CK_Operation_Hours", "\"Hours\" > 0"));

            modelBuilder.Entity<Component>()
                .ToTable(t => t.HasCheckConstraint("CK_Component_Cost", "\"Cost\" >= 0"));

            modelBuilder.Entity<Product>()
                .ToTable(t => t.HasCheckConstraint("CK_Product_LaborHours", "\"LaborHours\" >= 0"));

            modelBuilder.Entity<ProductComposition>()
                .ToTable(t => t.HasCheckConstraint("CK_ProductComposition_Quantity", "\"Quantity\" > 0"));

            modelBuilder.Entity<TechProcess>()
                .ToTable(t => t.HasCheckConstraint("CK_TechProcess_Quantity", "\"Quantity\" > 0"));
        }
    }
}