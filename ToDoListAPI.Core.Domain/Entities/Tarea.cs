using ToDoListAPI.Core.Domain.Common;
using ToDoListAPI.Core.Domain.Enum;

namespace ToDoListAPI.Core.Domain.Entities
{
    public class Tarea : BaseEntity<int>
    {
        public string Nombre { get; set; }
        public string  Contenido { get; set; }

        public EstadoTarea Estado { get; set; }
        public TipoTarea Tipo { get; set; }
        public PrioridadTarea Prioridad { get; set; }

    }
}
