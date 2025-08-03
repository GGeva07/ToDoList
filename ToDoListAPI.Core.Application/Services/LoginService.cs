using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Interfaces;

namespace ToDoListAPI.Core.Application.Services
{
    public class LoginService : ILogin
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> RegistrarUsuario(string usuarioNombre, string correo, string contrasenia)
        {
            try
            {
                var users = await _usuarioRepository.GetAllAsync();
                var user = users.FirstOrDefault(u => u.UsuarioNombre == usuarioNombre && u.Correo == correo);

                if (user != null)
                {
                    Console.WriteLine("Usuario o Correo en uso");
                    return null!;
                }

                var usuario = new Usuario
                {
                    UsuarioNombre = usuarioNombre,
                    Correo = correo,
                    Contrasenia = contrasenia
                };

                await _usuarioRepository.AddAsync(usuario);
                return usuario;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return null!;
            }
        }

        public async Task<Usuario> ValidarUsuario(string correo, string contrasenia)
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                 var usuario = usuarios.FirstOrDefault(u => u.Correo == correo && u.Contrasenia == contrasenia);

                if (usuario == null)
                {
                    Console.WriteLine("Usuario no existente");
                    return null!;
                }

                return usuario;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return null!;
            }
        }
    }
}
