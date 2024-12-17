using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosEFCore
{
    [Table("Usuarios")]

    public class Usuario
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Alias { get; set; }

        [Required, MaxLength(50)]
        public string Correo { get; set; }

        [Required]
        public string Contrasenia { get; set; }

        public Rol Rol { get; set; } // Admin, Cajero, etc.

        // Método estático para agregar un usuario
        public static void AgregarUsuario(string alias, string correo, string contrasenia, Rol rol)
        {
            using (var context = new SqLiteDbContext())
            {
                // Verificar si ya existe un usuario con el mismo alias
                if (context.Usuarios.Any(u => u.Alias == alias))
                {
                    throw new Exception("El alias ya está en uso.");
                }

                // Crear un nuevo usuario
                var nuevoUsuario = new Usuario
                {
                    Alias = alias,
                    Correo = correo,
                    Contrasenia = contrasenia, // Considera cifrar la contraseña
                    Rol = rol
                };

                context.Usuarios.Add(nuevoUsuario);
                context.SaveChanges();

                Console.WriteLine("Usuario agregado exitosamente.");
            }
        }
        public static Usuario IniciarSesion(string alias, string contrasenia)
        {
            using (var context = new SqLiteDbContext())
            {
                // Buscar el usuario por alias
                var usuario = context.Usuarios.FirstOrDefault(u => u.Alias == alias);

                if (usuario == null)
                {
                    throw new Exception("El usuario no existe.");
                }

                // Validar la contraseña (puedes agregar cifrado aquí)
                if (usuario.Contrasenia != contrasenia)
                {
                    throw new Exception("Contraseña incorrecta.");
                }

                // Retornar el usuario si las credenciales son correctas
                return usuario;
            }
        }

        public static void RestablecerContrasenia(string alias, string correo, string nuevaContrasenia)
        {
            using (var context = new SqLiteDbContext())
            {
                // Buscar el usuario por alias y correo
                var usuario = context.Usuarios.FirstOrDefault(u => u.Alias == alias && u.Correo == correo);

                if (usuario == null)
                {
                    throw new Exception("No se encontró un usuario con ese alias y correo.");
                }

                // Actualizar la contraseña (considera cifrarla)
                usuario.Contrasenia = nuevaContrasenia;
                context.SaveChanges();
            }
        }
    }
    public enum Rol
    {
        Administrador, Cajero, Auxiliar
    }
}
