using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Enum;

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

                if (context.Tarea.Any())
                {
                    var tareas = new Tarea[]
                    {
                new Tarea
                {
                    Nombre = "Primera tarea",
                    Contenido = "Descripción de la primera tarea",
                    Estado = EstadoTarea.PENDENGTING,
                    Tipo = TipoTarea.PRACTICA,
                },
                new Tarea
                {
                    Nombre = "Segunda tarea",
                    Contenido = "Descripción de la segunda tarea",
                    Estado = EstadoTarea.INPROGRES,
                    Tipo = TipoTarea.PRACTICA,
                }
                    };

                    context.Tarea.AddRange(tareas);
                    context.SaveChanges();
                }
            }

        }
    }
}





