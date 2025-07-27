using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Threading;
using ToDoList.Context;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class TareaService : ITarea
    {
        private readonly TodoListDBContext context;

        public TareaService(TodoListDBContext context)
        {
            this.context = context;
        }

        public async Task<List<Tarea>> Get()
        {
            try
            {
                var tarea = context.Tarea.ToListAsync();
                if (tarea == null) return null;
                return await tarea;
            }catch(Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return new List<Tarea>();
            }
        }

        public async Task<Object> GetTareaById(int id)
        {
            try
            {
                var tarea = await context.Tarea.FirstOrDefaultAsync(t => t.id == id);
                if (tarea == null) return null;

                return tarea;
            }
            catch (Exception e)
            {
                return new { error = $"Error: {e.Message}" };
            }
        }

        public async Task<List<Tarea>> GetTareasByNombre(string Nombre)
        {
            try
            {
                var Tareas = context.Tarea.Where(t => t.Nombre == Nombre).ToListAsync();
                if (Tareas == null) return null;
                return await Tareas;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return new List<Tarea>();
            }
        }

        public async Task<List<Tarea>> GetTareasByIdUsuario(int id)
        {
            try
            {

                var tarea = context.Tarea.Where(t => t.idUsuario == id).ToListAsync();
                if (tarea == null) return null;
                return await tarea;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return new List<Tarea>();
            }
        }

        public async Task<object> Post(Tarea model)
        {
            try
            {
                var usuario = await context.Usuario
                    .FirstOrDefaultAsync(u => u.id == model.idUsuario);

                Console.WriteLine(usuario);

                if (usuario == null) return null;

                await context.Tarea.AddAsync(model);
                await context.SaveChangesAsync();

                return new { message = "Tarea creada", tarea = model }; 
            }
            catch (Exception e)
            {
                return new { error = $"Error: {e.Message}" }; 
            }
        }



        public async Task<object> Put(Tarea model)
        {
            var tarea = await context.Tarea
                .FirstOrDefaultAsync(t => t.id == model.id && t.idUsuario == model.idUsuario);

            if (tarea == null) return null;

            tarea.Nombre = model.Nombre;
            tarea.Contenido = model.Contenido;
            tarea.Estado = model.Estado;

            await context.SaveChangesAsync();
            return new { message = "Tarea editada", tarea };
        }

        public async Task<object> Delete(int id)
        {
            try
            {
                var tareaEliminar = await context.Tarea
    .FirstOrDefaultAsync(t => t.id == id);
                var tarea = await context.Tarea.FirstOrDefaultAsync(T => T.idUsuario == tareaEliminar.idUsuario);
                if (tarea == null || tareaEliminar == null) return null;

                context.Tarea.Remove(tarea);
                await context.SaveChangesAsync();
                return new { message = "Tarea eliminada" };
            }
            catch (Exception e)
            {
                return new { error = $"Error: {e.Message}" };
            }
        }

        public async Task<object> Delete(int id, int idUsuario)
        {
            try
            {
                await Delete(id);
                return new { message = "Tarea eliminada"};
            }
            catch (Exception e)
            {
                return new { error = $"Error: {e.Message}" };
            }
        }
    }
}
