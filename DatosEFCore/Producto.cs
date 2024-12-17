using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEFCore
{
    [Table("Productos")]

    public class Producto
    {
        public int Id { get; set; }

        [ForeignKey("FK_Productos_Categoria_Id")]
        public int CategoriaId { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }

        [Required]
        public decimal PrecioCompra { get; set; }

        [Required]
        public int Stock { get; set; }

        //Manejo de Entity Framework
        [NotMapped]
        public Categoria Categoria { get; set; }
        [NotMapped]

        //Relacion con la clase Productosventas
        public List<ProductosVentas> ProductosVentas { get; set; }

        // Método para cargar todos los productos desde la base de datos
        public static List<Producto> CargarProductos()
        {
            using (var context = new SqLiteDbContext())
            {
                return context.Productos.ToList();
            }
        }

        public static void GuardarProducto(string nombre, decimal precioUnitario, decimal precioCompra, int stock, int categoriaId)
        {
            using (var context = new SqLiteDbContext())  // Usamos el contexto para interactuar con la base de datos
            {
                // Crear una nueva instancia de Producto
                var producto = new Producto
                {
                    Nombre = nombre,
                    PrecioUnitario = precioUnitario,
                    PrecioCompra = precioCompra,
                    Stock = stock,
                    CategoriaId = categoriaId
                };

                // Agregar el producto al contexto y guardar los cambios
                context.Productos.Add(producto);
                context.SaveChanges();  // Guardar en la base de datos
            }
        }
    }
}
