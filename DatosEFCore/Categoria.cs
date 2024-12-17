using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatosEFCore
{
    [Table("Categorias")]
    public class Categoria
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; }

        // Relación uno a muchos con Producto
        [NotMapped]
        public List<Producto> Productos { get; set; } = new List<Producto>();

        // Método para cargar todas las categorías desde la base de datos
        public static List<Categoria> CargarCategorias()
        {
            using (var context = new SqLiteDbContext())
            {
                // Obtener todas las categorías de la base de datos
                return context.Categorias.ToList();
            }
        }

        public static void AgregarCategoria(string nombre)
        {
            using (var context = new SqLiteDbContext())
            {
                // Verificar si ya existe la categoría
                if (context.Categorias.Any(c => c.Nombre == nombre))
                {
                    throw new Exception("La categoría ya existe.");
                }

                // Crear una nueva categoría
                var nuevaCategoria = new Categoria
                {
                    Nombre = nombre
                };

                context.Categorias.Add(nuevaCategoria);
                context.SaveChanges();
            }
        }
    }
}
