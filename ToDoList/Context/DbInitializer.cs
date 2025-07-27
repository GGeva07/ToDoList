using ToDoList.Models;

namespace ToDoList.Context
{
        public static class DbInitializer
        {
            public static void Initialize(TodoListDBContext context)
            {

                context.Database.EnsureCreated();


                if (context.Usuario.Any())
                {
                    return;


                    var usuarios = new Usuario[]
                    {
                new Usuario
                {
                    usuarioNombre = "admin",
                    correo = "admin@example.com",
                    contrasenia = "admin123"
                },
                new Usuario
                {
                    usuarioNombre = "usuario1",
                    correo = "usuario1@example.com",
                    contrasenia = "password123"
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


