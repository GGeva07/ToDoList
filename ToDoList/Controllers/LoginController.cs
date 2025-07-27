using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin service;

        public LoginController(ILogin service)
        {
            this.service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            try
            {
                var usuario = await service.ValidarUsuario(login.Correo, login.Contrasenia);

                if (usuario == null)
                {
                    return Unauthorized(new
                    {
                        success = false,
                        message = "Credenciales incorrectas",
                        errorCode = 401
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Login exitoso",
                    errorCode = 200,
                    data = new
                    {
                        id = usuario.id,
                        usuarioNombre = usuario.usuarioNombre,
                        correo = usuario.correo
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error durante el login: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpPost("Sign-In")]
        public async Task<IActionResult> SignIn([FromBody] Login login)
        {
            try
            {
                var usuario = await service.RegistrarUsuario(login.usuarioNombre, login.Correo, login.Contrasenia);

                if (usuario == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Credenciales incorrectas o en uso",
                        errorCode = 400
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Usuario registrado exitosamente",
                    errorCode = 200,
                    data = new
                    {
                        id = usuario.id,
                        usuarioNombre = usuario.usuarioNombre,
                        correo = usuario.correo
                    }
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error durante el registro: {e.Message}",
                    errorCode = 500
                });
            }
        }
    }
}

