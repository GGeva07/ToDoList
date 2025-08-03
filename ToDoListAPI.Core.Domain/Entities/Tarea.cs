using ToDoListAPI.Core.Domain.Common;

namespace ToDoListAPI.Core.Domain.Entities
{
    public class Tarea : BaseEntity<int>
    {
        public string Nombre { get; set; }
        public string  Contenido { get; set; }

        public string Estado { get; set; }

        public int idUsuario { get; set; }
    }
}
