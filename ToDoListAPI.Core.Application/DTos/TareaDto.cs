using ToDoListAPI.Core.Domain.Enum;

namespace ToDoListAPI.Core.Application.DTos
{
    public class TareaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Contenido { get; set; }

        public EstadoTarea Estado { get; set; }
        public TipoTarea Tipo { get; set; }
    }
}
