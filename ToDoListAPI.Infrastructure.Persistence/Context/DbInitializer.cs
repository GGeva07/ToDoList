using ToDoListAPI.Core.Domain.Entities;

namespace ToDoListAPI.Infrastructure.Persistence.Context
{
        public static class DbInitializer
        {
        public static void Initialize(TodoListDBContext context)
        {

            context.Database.EnsureCreated();

            if (context.Usuario.Any())
            {
                var usuarios = new Usuario[]
                {
                new Usuario
                {
                    UsuarioNombre = "admin",
                    Correo = "admin@example.com",
                    Contrasenia = "admin123"
                },
                new Usuario
                {
                    UsuarioNombre = "usuario1",
                    Correo = "usuario1@example.com",
                    Contrasenia = "password123"
                }
                };

                context.Usuario.AddRange(usuarios);
                context.SaveChanges();

                var tareas = new Tarea[]
                {
                new Tarea
                {
                    Nombre = "Primera tarea",
                    Contenido = "Descripción de la primera tarea",
                    Estado = "Pendiente",
                    idUsuario = 1
                },
                new Tarea
                {
                    Nombre = "Segunda tarea",
                    Contenido = "Descripción de la segunda tarea",
                    Estado = "En Progreso",
                    idUsuario = 1
                }
                };

                context.Tarea.AddRange(tareas);
                context.SaveChanges();
            }
        }

        }
}




