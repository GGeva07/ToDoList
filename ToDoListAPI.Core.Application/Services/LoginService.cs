using ToDoListAPI.Core.Application.Helpers;
using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using ToDoListAPI.Core.Application.DTos;

namespace ToDoListAPI.Core.Application.Services
{
    public class LoginService : ILogin
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public LoginService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<Usuario> RegistrarUsuario(string nombre, string correo, string contrasenia)
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                var usuarioExistente = usuarios.FirstOrDefault(u => u.Correo == correo || u.UsuarioNombre == nombre);

                if (usuarioExistente != null)
                {
                    Console.WriteLine("Usuario ya existe");
                    return null!;
                }

                var hash = Auth.Hash(contrasenia, out string salt);

                var nuevo = new Usuario
                {
                    UsuarioNombre = nombre,
                    Correo = correo,
                    Contrasenia = hash,
                    Salt = salt
                };

                return await _usuarioRepository.AddAsync(nuevo);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return null!;
            }
        }


        public async Task<AuthResponse> ValidarUsuario(string email, string pass)
        {
            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                var usuario = usuarios.FirstOrDefault(u => u.Correo == email);

                if (usuario == null || !Auth.Verify(pass, usuario.Contrasenia, usuario.Salt))
                {
                    return null;
                }

                var token = GenerateJwtToken(usuario);

                return new AuthResponse
                {
                    Id = usuario.Id,
                    UsuarioNombre = usuario.UsuarioNombre,
                    Correo = usuario.Correo,
                    Token = token
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error {e.Message}");
                return null;
            }
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.UsuarioNombre),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, "User") 
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
