using Microsoft.EntityFrameworkCore;
using CourseBD.Models;

namespace CourseBD.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Material> Materials { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<TechProcess> TechProcesses { get; set; }
        public DbSet<TechProcessOperation> TechProcessOperations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductComponent> ProductComponents { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Составные первичные ключи
            modelBuilder.Entity<TechProcessOperation>()
                .HasKey(tpo => new { tpo.TechProcessId, tpo.OperationId });

            modelBuilder.Entity<ProductComponent>()
                .HasKey(pc => new { pc.ProductId, pc.ComponentId });

            modelBuilder.Entity<TechProcessOperation>()
                .HasOne(tpo => tpo.TechProcess)
                .WithMany(tp => tp.TechProcessOperations)
                .HasForeignKey(tpo => tpo.TechProcessId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TechProcessOperation>()
                .HasOne(tpo => tpo.Operation)
                .WithMany(o => o.TechProcessOperations)
                .HasForeignKey(tpo => tpo.OperationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductComponent>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductComponents)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductComponent>()
                .HasOne(pc => pc.Component)
                .WithMany(c => c.ProductComponents)
                .HasForeignKey(pc => pc.ComponentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.TechProcess)
                .WithMany(tp => tp.Products)
                .HasForeignKey(p => p.TechProcessId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Product)
                .WithMany(p => p.Requests)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // Индексы
            modelBuilder.Entity<Product>().HasIndex(p => p.Name);
            modelBuilder.Entity<Request>().HasIndex(r => r.RequestDate);
            modelBuilder.Entity<TechProcess>().HasIndex(t => t.MaterialId);

            modelBuilder.Entity<Material>()
                .ToTable(t => t.HasCheckConstraint("CK_Material_Cost", "\"Cost\" >= 0"));
            modelBuilder.Entity<Operation>()
                .ToTable(t => t.HasCheckConstraint("CK_Operation_Cost", "\"Cost\" >= 0"));
            modelBuilder.Entity<Component>()
                .ToTable(t => t.HasCheckConstraint("CK_Component_Cost", "\"Cost\" >= 0"));
     
            modelBuilder.Entity<TechProcess>()
                .ToTable(t => t.HasCheckConstraint("CK_TechProcess_MaterialQuantity", "\"MaterialQuantity\" > 0"));
            modelBuilder.Entity<ProductComponent>()
                .ToTable(t => t.HasCheckConstraint("CK_ProductComponent_Quantity", "\"Quantity\" > 0"));
 
            modelBuilder.Entity<Request>()
                .ToTable(t => t.HasCheckConstraint("CK_Request_Quantity", "\"Quantity\" > 0"));

            modelBuilder.Entity<Request>()
                .Property(r => r.RequestDate)
                .HasColumnType("timestamp without time zone");
        }
    }
}