using ToDoListAPI.Core.Application.Fabricas;
using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Entities.TiposTareas;
using ToDoListAPI.Core.Domain.Enum;

namespace ToDoListAPI.Infrastructure.Persistence.Fabricas
{
    public class FabricaTareas : IFabricaTareas
    {
        public TareaBase ObtenerTareaFactory(Tarea tarea)
        {
            TareaBase tipoTarea = tarea.Tipo switch
            {
                TipoTarea.PRACTICA => new Practica(),
                TipoTarea.ASIGNACION => new Asignacion(),
                TipoTarea.CUESTIONARIO => new Cuestionario(),
                _ => throw new ArgumentException("Tipo De Tarea No existente")
            };

            tipoTarea.Estado = tarea.Estado;
            tipoTarea.Tipo = tarea.Tipo;
            tipoTarea.Nombre = tarea.Nombre;
            tipoTarea.Contenido = tarea.Contenido;
            tipoTarea.SetPriorityByType(tarea);
            tipoTarea.Prioridad = tarea.Prioridad;

            return tipoTarea;
        }
    }
}
