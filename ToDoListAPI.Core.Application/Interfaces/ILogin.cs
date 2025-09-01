using ToDoListAPI.Core.Application.DTos;
using ToDoListAPI.Core.Domain.Entities;

namespace ToDoListAPI.Core.Application.Interfaces
{
    public interface ILogin
    {
        public Task<AuthResponse> ValidarUsuario(string email, string pass);
        public Task<Usuario> RegistrarUsuario(string name, string email, string pass);
    }
}
