using ToDoListAPI.Core.Application.Memo;
using ToDoListAPI.Core.Application.DTos;
using ToDoListAPI.Core.Application.Fabricas;
using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Enum;
using ToDoListAPI.Core.Domain.Interfaces;

namespace ToDoListAPI.Core.Application.Services
{
    /// <summary>
    /// Servicio de aplicación para gestionar operaciones CRUD de tareas.
    /// Implementa funcionalidades de cache para mejorar el rendimiento y utiliza
    /// el patrón Factory para la creación de tareas.
    /// </summary>
    public class TareaService : ITarea
    {
        #region Private Fields

        private readonly ITareaRepository _tareaRepository;
        private readonly IFabricaTareas _fabrica;
        private readonly Cache<int, Tarea> _cache;

        /// <summary>
        /// Función de mapeo que convierte un TareaDto a una entidad Tarea.
        /// </summary>
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

        #endregion

        #region Constructor

        /// <summary>
        /// Inicializa una nueva instancia de TareaService.
        /// </summary>
        /// <param name="tareaRepository">Repositorio para operaciones de datos de tareas.</param>
        /// <param name="fabrica">Factory para crear instancias de tareas según su tipo.</param>
        /// <param name="cache">Sistema de cache para mejorar el rendimiento de consultas.</param>
        /// <exception cref="ArgumentNullException">Se lanza cuando algún parámetro es null.</exception>
        public TareaService(ITareaRepository tareaRepository, IFabricaTareas fabrica, Cache<int, Tarea> cache)
        {
            _tareaRepository = tareaRepository ?? throw new ArgumentNullException(nameof(tareaRepository));
            _fabrica = fabrica ?? throw new ArgumentNullException(nameof(fabrica));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Obtiene todas las tareas disponibles en el sistema.
        /// </summary>
        /// <returns>Lista de tareas. Lista vacía si no hay tareas o ocurre un error.</returns>
        public async Task<List<Tarea>> Get()
        {
            try
            {
                var tareas = await _tareaRepository.GetAllAsync();
                return tareas.Any() ? [.. tareas] : [];
            }
            catch (Exception)
            {
                // Log the exception in a real-world scenario
                return [];
            }
        }

        /// <summary>
        /// Obtiene una tarea específica por su identificador.
        /// Utiliza cache para mejorar el rendimiento de consultas repetidas.
        /// </summary>
        /// <param name="id">Identificador único de la tarea.</param>
        /// <returns>La tarea encontrada o una nueva instancia si no existe.</returns>
        public async Task<Tarea?> GetById(int id)
        {
            try
            {
                // Intentar obtener desde cache primero
                if (_cache.getValue(id, out var data))
                {
                   return data;
                }
                

                // Si no está en cache, buscar en base de datos
                var tarea = await _tareaRepository.GetByIdAsync(id);

                // Guardar en cache para consultas futuras
                if (tarea != null)
                {
                    _cache.set(id, tarea);
                }

                return tarea;
            }
            catch (Exception)
            {
                // Log the exception in a real-world scenario
                return new();
            }
        }

        /// <summary>
        /// Busca tareas que coincidan exactamente con el nombre proporcionado.
        /// </summary>
        /// <param name="Nombre">Nombre exacto de las tareas a buscar.</param>
        /// <returns>Lista de tareas que coinciden con el nombre. Lista vacía si no hay coincidencias o ocurre un error.</returns>
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
                // Log the exception in a real-world scenario
                return [];
            }
        }

        /// <summary>
        /// Crea una nueva tarea en el sistema.
        /// Establece automáticamente el estado inicial como PENDENGTING y utiliza el Factory pattern.
        /// </summary>
        /// <param name="model">DTO con los datos de la tarea a crear.</param>
        /// <returns>Mensaje indicando el resultado de la operación de creación.</returns>
        public async Task<string?> Post(TareaDto model)
        {
            try
            {
                // Establecer estado inicial
                model.Estado = EstadoTarea.PENDENGTING;

                // Crear tarea usando factory pattern
                var tarea = _fabrica.OctenerTareaFactory(TareaDtoToTarea(model));

                // Persistir en base de datos
                var content = await _tareaRepository.AddAsync(tarea);

                return content != null
                    ? $"Tarea de tipo: {tarea.GetTaskType()} Creada Correctamente"
                    : "No se ha podido crear la tarea";
            }
            catch (Exception ex)
            {
                return $"Error al crear la Tarea: {ex.Message}";
            }
        }

        /// <summary>
        /// Actualiza una tarea existente en el sistema.
        /// Actualiza tanto la base de datos como el cache.
        /// </summary>
        /// <param name="model">DTO con los datos actualizados de la tarea.</param>
        /// <returns>Mensaje indicando el resultado de la operación de actualización.</returns>
        public async Task<string?> Put(TareaDto model)
        {
            try
            {
                var tarea = await _tareaRepository.GetByIdAsync(model.Id);

                if (tarea == null)
                {
                    return "No se encontró una tarea con este id";
                }

                // Actualizar propiedades
                tarea.Estado = model.Estado;
                tarea.Tipo = model.Tipo;
                tarea.Nombre = model.Nombre;
                tarea.Contenido = model.Contenido;
                _cache.set(model.Id, tarea);

                // Actualizar cache
                _cache.set(model.Id, tarea);

                // Persistir cambios
                var content = await _tareaRepository.UpdateAsync(tarea);

                return content != null
                    ? "Tarea Actualizada correctamente"
                    : "No se encontró una tarea con este id";
            }
            catch (Exception ex)
            {
                return $"Error al Actualizar la Tarea: {ex.Message}";
            }
        }

        /// <summary>
        /// Elimina una tarea específica del sistema.
        /// Remueve la tarea tanto de la base de datos como del cache.
        /// </summary>
        /// <param name="id">Identificador único de la tarea a eliminar.</param>
        /// <returns>Mensaje indicando el resultado de la operación de eliminación.</returns>
        public async Task<string?> Delete(int id)
        {
            try
            {
                var content = await _tareaRepository.DeleteAsync(id);

                // Remover del cache
                _cache.remove(id);

                return content != null
                    ? "Tarea eliminada correctamente"
                    : "La tarea a eliminar no fue encontrada";
            }
            catch (Exception ex)
            {
                return $"Error al Eliminar la Tarea: {ex.Message}";
            }
        }

        /// <summary>
        /// Elimina múltiples tareas del sistema en una sola operación.
        /// Nota: Esta implementación no actualiza el cache, considera mejorar esta funcionalidad.
        /// </summary>
        /// <param name="ids">Array de identificadores de las tareas a eliminar.</param>
        /// <returns>Mensaje indicando el resultado de la operación de eliminación masiva.</returns>
        public async Task<string?> Delete(int[] ids)
        {
            try
            {
                List<Tarea> tareas = [];

                // Obtener todas las tareas a eliminar
                foreach (var id in ids)
                {
                    var tarea = await _tareaRepository.GetByIdAsync(id);
                    if (tarea != null)
                    {
                        tareas.Add(tarea);
                    }
                }

                if (!tareas.Any())
                {
                    return "No se encontraron las tareas a eliminar";
                }

                var content = await _tareaRepository.DeleteRangeAsync([.. tareas]);

                // TODO: Considerar remover las tareas del cache también
                // foreach (var id in ids) { _cache.remove(id); }

                return content.Count != 0
                    ? "Tareas eliminadas correctamente"
                    : "No se encontraron las tareas a eliminar";
            }
            catch (Exception ex)
            {
                return $"Error al Eliminar las Tareas: {ex.Message}";
            }
        }

        #endregion
    }
}