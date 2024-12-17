using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEFCore
{
    [Table("Clientes")]

    public class Cliente
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; }

        [Required, MaxLength(50)]
        public string Apellido { get; set; }

        [Required, MaxLength(100)]
        public string Correo { get; set; }

        [MaxLength(20)]
        public string? Telefono { get; set; }

        // Método para guardar un cliente
        public static void GuardarCliente(string nombre, string apellido, string correo, string? telefono)
        {
            using (var context = new SqLiteDbContext())
            {
                // Verificar si ya existe un cliente con el mismo correo
                if (context.Clientes.Any(c => c.Correo == correo))
                {
                    throw new Exception("Ya existe un cliente con ese correo.");
                }

                // Crear un nuevo cliente
                var nuevoCliente = new Cliente
                {
                    Nombre = nombre,
                    Apellido = apellido,
                    Correo = correo,
                    Telefono = telefono
                };

                // Agregar el cliente a la base de datos
                context.Clientes.Add(nuevoCliente);
                context.SaveChanges();
            }
        }
    }
}
