using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEFCore
{
    public class SqLiteDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbFileName = "biblioteca.sqlite";
            var dbDirectory = Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Db"));
            var dbPath = Path.Combine(dbDirectory.FullName, dbFileName);

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
            base.OnConfiguring(optionsBuilder);
        }

        //DbSets de cada Formulario
        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ProductosVentas> ProductosVentas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación de Productos a Categorías (N:1)
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.CategoriaId);

            //---------------------------------------------------------------------------------------------
            //   ---  IsUnique ---

            //Tabla Usuario: UQ
            modelBuilder.Entity<Usuario>(usuario =>
            {
                usuario.HasIndex(u => u.Alias).IsUnique();
                usuario.HasIndex(u => u.Correo).IsUnique();

            }
            );

            //Tabla Categoria : UQ
            modelBuilder.Entity<Categoria>()
                .HasIndex(cat => cat.Nombre).IsUnique();

            //----------------------------------------------------------------------------------------------
            //Define una llave primaria compuesta, de una tabla de muchos a muchos (ProductosVentas)

            modelBuilder.Entity<ProductosVentas>()
                .HasKey(dv => new { dv.VentaId, dv.ProductoId });
            //----------------------------------------------------------------------------------------------
            //Relacion de Uno a Muchos
            modelBuilder.Entity<ProductosVentas>(dv =>
            {
                dv.HasOne(d => d.Venta)
                    .WithMany(v => v.ProductosVentas)
                    .HasForeignKey("FK_ProductosVenta_Ventas_Id");

                dv.HasOne<Producto>(d => d.Producto)
                    .WithMany(p => p.ProductosVentas)
                    .HasForeignKey("FK_ProductosVenta_Productos_Id");
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}
