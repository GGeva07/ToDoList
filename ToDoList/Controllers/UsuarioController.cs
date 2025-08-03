using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Domain.Entities;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario service;

        public UsuarioController(IUsuario service)
        {
            this.service = service;
        }

        [HttpGet("Get-Usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var usuarios = await service.Get();

                if (usuarios == null || !usuarios.Any())
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No se encontraron usuarios",
                        errorCode = 404
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Usuarios obtenidos exitosamente",
                    errorCode = 200,
                    data = usuarios
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error interno del servidor: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpPost("Post-Usuario")]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario model)
        {
            try
            {
                var result = await service.Post(model);

                if (result == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "El usuario ya existe, ingresa otro usuario",
                        errorCode = 400
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Usuario creado exitosamente",
                    errorCode = 200,
                    data = result
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error al insertar usuario: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpPut("Put-Usuario/{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario model)
        {
            try
            {
                var result = await service.Put(id, model);

                if (result.Contains("no encontrado"))
                {
                    return NotFound(new
                    {
                        success = false,
                        message = result,
                        errorCode = 404
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Usuario actualizado exitosamente",
                    errorCode = 200,
                    data = result
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error al actualizar usuario: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpDelete("Delete-Usuario/{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var result = await service.Delete(id);

                if (result.Contains("no encontrado"))
                {
                    return NotFound(new
                    {
                        success = false,
                        message = result,
                        errorCode = 404
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Usuario eliminado exitosamente",
                    errorCode = 200,
                    data = result
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error al eliminar usuario: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpGet("GetUsuarioById/{id}")]
        public async Task<IActionResult> GetUsuarioById(int id)
        {
            try
            {
                var result = await service.GetUsuarioById(id);

                if (result == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Usuario no encontrado",
                        errorCode = 404
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Usuario obtenido exitosamente",
                    errorCode = 200,
                    data = result
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error al encontrar el usuario por el id: {e.Message}",
                    errorCode = 500
                });
            }
        }
    }
}