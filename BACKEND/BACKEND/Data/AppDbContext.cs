using BACKEND.Dto;
using BACKEND.Entities;
using Microsoft.EntityFrameworkCore;

namespace BACKEND.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
     : base(options)
        {
        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetalleVenta> DetalleVenta { get; set; }
        public DbSet<HistorialVenta> HistorialVentas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo de las entidades a las tablas 
            modelBuilder.Entity<Producto>().ToTable("PRODUCTOS");
            modelBuilder.Entity<Cliente>().ToTable("CLIENTES");
            modelBuilder.Entity<Venta>().ToTable("VENTAS");
            modelBuilder.Entity<DetalleVenta>().ToTable("DETALLEVENTA");

            modelBuilder.Entity<HistorialVenta>()
                .HasNoKey()
                .ToView("VW_HISTORIAL_VENTAS");


            // Configurar claves foráneas 
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente)
                .WithMany()
                .HasForeignKey(v => v.ClienteId);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.Detalles)
                .HasForeignKey(d => d.VentaId);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.ProductoId);
        }
    }
}
