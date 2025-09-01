using ToDoListAPI.Core.Domain.Common;

namespace ToDoListAPI.Core.Domain.Entities
{
    public class Usuario : BaseEntity<int>
    {
        public int Id { get; set; }
        public string UsuarioNombre { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set; }
        public string Salt { get; set; }
    }
}

