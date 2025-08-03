using ToDoListAPI.Core.Application.Fabricas;
using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Entities.TiposTareas;
using ToDoListAPI.Core.Domain.Enum;

namespace ToDoListAPI.Infrastructure.Persistence.Fabricas
{
    public class FabricaTareas : IFabricaTareas
    {
        public TareaBase OctenerTareaFactory(Tarea tarea)
        {
            TareaBase tipoTarea = tarea.Tipo switch
            {
                TipoTarea.PRACTICA => new Practica(),
                TipoTarea.ASIGNACION => new Asignacion(),
                TipoTarea.CUESTIONARIO => new Cuestionario(),
                _ => throw new ArgumentException("Tipo De Tarea No existente")
            };

            tipoTarea.idUsuario = tarea.idUsuario;
            tipoTarea.Estado = tarea.Estado;
            tipoTarea.Tipo = tarea.Tipo;
            tipoTarea.Nombre = tarea.Nombre;
            tipoTarea.Contenido = tarea.Contenido;

            return tipoTarea;
        }
    }
}
