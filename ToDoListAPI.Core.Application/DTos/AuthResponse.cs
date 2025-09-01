namespace ToDoListAPI.Core.Application.DTos
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string UsuarioNombre { get; set; }
        public string Correo { get; set; }
        public string Token { get; set; }
    }
}