using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Interfaces;

namespace ToDoListAPI.Core.Application.Services
{
    public class TareaService : ITarea
    {
        private readonly ITareaRepository _tareaRepository;

        public TareaService(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }
        public async Task<List<Tarea>?> Get()
        {
            try
            {
                var tarea = await _tareaRepository.GetAllAsync();
                if (tarea == null) return null;
                return tarea.ToList();
            }catch(Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return new List<Tarea>();
            }
        }

        public async Task<object?> GetTareaById(int id)
        {
            try
            {
                var tarea = await _tareaRepository.GetByIdAsync(id);
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
                var Tareas = await _tareaRepository.GetAllAsync();
                if (Tareas == null) return null;
                return Tareas.Where(t => t.Nombre == Nombre).ToList();
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return new List<Tarea>();
            }
        }

        public async Task<List<Tarea>?> GetTareasByIdUsuario(int id)
        {
            try
            {
                var tarea = await _tareaRepository.GetAllAsync();

                if (tarea == null) return null;
                return tarea.Where(t => t.idUsuario == id).ToList();
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
                var usuario = model != null ? await _tareaRepository.AddAsync(model) : null;
                return new { message = "Tarea creada", tarea = model }; 
            }
            catch (Exception e)
            {
                return new { error = $"Error: {e.Message}" }; 
            }
        }



        public async Task<object> Put(Tarea model)
        {
            var tarea = await _tareaRepository.GetByIdAsync(model.Id);

            if (tarea == null) return null;

            tarea.Nombre = model.Nombre;
            tarea.Contenido = model.Contenido;
            tarea.Estado = model.Estado;

            await _tareaRepository.UpdateAsync(tarea);
            return new { message = "Tarea editada", tarea };
        }

        public async Task<object> Delete(int id)
        {
            try
            {
                var tareaEliminar = await _tareaRepository.DeleteAsync(id);
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

        public Task<Tarea> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> Put(int id, Tarea model)
        {
            throw new NotImplementedException();
        }

        Task<string?> ITarea.Post(Tarea model)
        {
            throw new NotImplementedException();
        }

        Task<string?> ITarea.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
