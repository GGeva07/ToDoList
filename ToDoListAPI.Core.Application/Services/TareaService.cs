using System.Reflection;
using System.Threading;
using ToDoListAPI.Core.Application.DTos;
using ToDoListAPI.Core.Application.Fabricas;
using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Enum;
using ToDoListAPI.Core.Domain.Interfaces;

namespace ToDoListAPI.Core.Application.Services
{
    public class TareaService : ITarea
    {
        private readonly ITareaRepository _tareaRepository;
        private readonly IFabricaTareas _fabrica;

        Func<TareaDto, Tarea> TareaDtoToTarea = delegate (TareaDto model)
        {
           var tarea = new Tarea
            {
                Id = model.Id,
                Estado = model.Estado,
                Tipo = model.Tipo,
                Nombre = model.Nombre,
                Contenido = model.Contenido,
            };
            return tarea;
        };

        public TareaService(ITareaRepository tareaRepository, IFabricaTareas fabrica)
        {
            _tareaRepository = tareaRepository;
            _fabrica = fabrica;
        }
        public async Task<List<Tarea>> Get()
        {
            try
            {
                var tareas = await _tareaRepository.GetAllAsync();
                return tareas.Any() ? [.. tareas] : [];
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<Tarea?> GetById(int id)
        {
            try
            {
                var tarea = await _tareaRepository.GetByIdAsync(id);
                return tarea ?? new();
            }
            catch (Exception)
            {
                return new();
            }
        }

        //public async Task<List<Tarea>?> GetTareasByIdUsuario(int idUsuario)
        //{
        //    try
        //    {
        //        var tareas = await _tareaRepository.GetAllAsync();
        //        var taresByUsuario = tareas.Where(t => t.idUsuario == idUsuario).ToList();

        //        return taresByUsuario.Count != 0 ? taresByUsuario : [];
        //    }
        //    catch(Exception)
        //    {
        //        return [];
        //    }
        //}

        public async Task<List<Tarea>?> GetTareasByNombre(string Nombre)
        {
            try
            {
                var tareas = await _tareaRepository.GetAllAsync();
                var tareasByNombre = tareas.Where(t => t.Nombre == Nombre).ToList();
                return tareasByNombre.Count != 0 ? tareasByNombre : [];
            }
            catch (Exception)
            {
                return [];
            }
        }

        public async Task<string?> Post(TareaDto model)
        {
            try
            {
                model.Estado = EstadoTarea.PENDENGTING;
                var tarea = _fabrica.ObtenerTareaFactory(TareaDtoToTarea(model));

                var content = await _tareaRepository.AddAsync(tarea);
                return content != null ? $"Tarea de typo: {tarea.GetTaskType()} Creada Correctamente" : "no se a podido crear la tarea";
            }
            catch (Exception ex)
            {
                return $"Error al crear la Tarea: {ex.Message}";
            }
        }

        public async Task<string?> Put(TareaDto model)
        {
            try
            {
                var tarea = await _tareaRepository.GetByIdAsync(model.Id);
                
                tarea.Estado = model.Estado;
                tarea.Tipo = model.Tipo;
                tarea.Nombre = model.Nombre;
                tarea.Contenido = model.Contenido;
                
                var content = await _tareaRepository.UpdateAsync(tarea);

                return content != null ? "Tarea Acutalizada correctamente" : "No se encontro una tarea con este id";
            }
            catch(Exception ex)
            {
                return $"Error al Actualizar la Tarea: {ex.Message}";
            }
        }
        public async Task<string?> Delete(int id)
        {
            try
            {
                var content = await _tareaRepository.DeleteAsync(id);
                return content != null ? "Tarea eliminada correctamente" : "La tarea a eliminar no fue encontrada";
            }
            catch (Exception ex)
            {
                return $"Error al Eliminar la Tarea: {ex.Message}";
            }
        }
        public async Task<string?> Delete(int[] ids)
        {
            try
            {
                List<Tarea> tareas = [];
                foreach (var id in ids)
                {
                    tareas.Add(await _tareaRepository.GetByIdAsync(id));
                }
                var content = await _tareaRepository.DeleteRangeAsync([.. tareas]);
                return content.Count != 0 ? "Tareas eliminadas correctamente" : "No se encontraron las tareas a eliminar";
            }
            catch (Exception ex)
            {
                return $"Error al Eliminar las Tareas: {ex.Message}";
            }
        }
    }
}
