using ToDoListAPI.Core.Application.DTos;
using ToDoListAPI.Core.Domain.Entities;

namespace ToDoListAPI.Core.Application.Interfaces
{
    public interface ITarea 
    {
        public Task<List<Tarea>> Get();
        public Task<Tarea?> GetById(int id);
        public Task<string?> Post(TareaDto model);
        public Task<string?> Put(TareaDto model);
        public Task<string?> Delete(int id);
        public Task<List<Tarea>?> GetTareasByNombre(string Nombre);
        public Task<List<Tarea>?> GetTareasByIdUsuario(int idUsuario);
    }
}
