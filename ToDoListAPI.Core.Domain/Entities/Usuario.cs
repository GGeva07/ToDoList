using ToDoListAPI.Core.Domain.Common;

namespace ToDoListAPI.Core.Domain.Entities
{
    public class Usuario : BaseEntity<int>
    {
        public string UsuarioNombre { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Contrasenia { get; set; } = string.Empty;
    }
}
