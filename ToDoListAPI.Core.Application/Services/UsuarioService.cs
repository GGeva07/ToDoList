using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Interfaces;

namespace ToDoList.Services
{
    public class UsuarioService : IUsuario
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _usuarioRepository = repository;
        }

        public async Task<List<Usuario>> Get()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                return [.. usuarios];
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return [];
            }
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);

                if (usuario == null)
                    return new();

                return usuario;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return new();
            }
        }


        public async Task<string> Post(Usuario model)
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                var usuario = usuarios.FirstOrDefault(u => u.Correo == model.Correo);


                if (usuario != null)
                {
                    return("El usuario ya existe.");
                }

                await _usuarioRepository.AddAsync(model);
                return "Registro insertado correctamente";
            }
            catch (Exception e)
            {
                return $"Error al insertar el registro: {e.Message}";
            }
        }

        public async Task<string> Put(int id, Usuario model)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);

                if (usuario == null)
                {
                    return "Usuario no encontrado";
                }

                usuario.UsuarioNombre = model.UsuarioNombre;
                usuario.Correo = model.Correo;
                usuario.Contrasenia = model.Contrasenia;

                await _usuarioRepository.UpdateAsync(usuario);
                return "Registro actualizado correctamente";
            }
            catch (Exception e)
            {
                return $"Error al actualizar el registro: {e.Message}";
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                await _usuarioRepository.DeleteAsync(id);
                return "Usuario eliminado correctamente";
            }
            catch (Exception e)
            {
                return $"Error al eliminar el usuario: {e.Message}";
            }
        }
    }
}
