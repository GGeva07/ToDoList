using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Core.Application.DTos;
using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Domain.Entities;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITarea service;
        Func<Tarea, TareaDto> TareaToTareaDto = delegate (Tarea model)
        {
            var tarea = new TareaDto
            {
                Id = model.Id,
                idUsuario = model.idUsuario,
                Estado = model.Estado,
                Tipo = model.Tipo,
                Nombre = model.Nombre,
                Contenido = model.Contenido,
            };
            return tarea;
        };

        public TareaController(ITarea service)
        {
            this.service = service;
        }

        [HttpGet("Get-Tareas")]
        public async Task<IActionResult> GetTareas()
        {
            try
            {
                var tareas = await service.Get();
                if (tareas.Any())
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Tareas obtenidas exitosamente",
                        errorCode = 200,
                        data = tareas
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No se encontraron tareas",
                        errorCode = 404
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error obteniendo la lista de tareas: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpGet("Get-TareaById/{id}")]
        public async Task<IActionResult> GetTareaById(int id)
        {
            try
            {
                var tarea = await service.GetById(id);
                if (tarea == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Tarea no encontrada",
                        errorCode = 404
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Tarea obtenida exitosamente",
                    errorCode = 200,
                    data = tarea
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error obteniendo la tarea: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpGet("Get-Tareas-by-Usuario/{idUsuario}")]
        public async Task<IActionResult> GetTareasByIdUsuario(int idUsuario)
        {
            try
            {
                var tareas = await service.GetTareasByIdUsuario(idUsuario);
                if (tareas.Any())
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Tareas del usuario obtenidas exitosamente",
                        errorCode = 200,
                        data = tareas
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"No se encontraron tareas para el usuario con id {idUsuario} o el usuario fue eliminado",
                        errorCode = 404
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error obteniendo las tareas por el id Usuario: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpGet("Get-TareaByTitle/{Nombre}")]
        public async Task<IActionResult> GetTareaByNombre(string Nombre)
        {
            try
            {
                var tareas = await service.GetTareasByNombre(Nombre);
                if (tareas.Any())
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Tareas encontradas por nombre exitosamente",
                        errorCode = 200,
                        data = tareas
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"No se encontraron tareas con el titulo: {Nombre}",
                        errorCode = 404
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error obteniendo las tareas por su nombre: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpPost("Post-Tarea")]
        public async Task<IActionResult> PostTarea([FromBody] Tarea model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Los datos de la tarea no son válidos",
                        errorCode = 400
                    });
                }

                var resultado = await service.Post(TareaToTareaDto(model));
                return Ok(new
                {
                    success = true,
                    message = "Tarea creada exitosamente",
                    errorCode = 200,
                    data = resultado
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error creando la tarea: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpPut("Put-Tarea/{id}/{idUsuario}")]
        public async Task<IActionResult> PutTarea(int id, int idUsuario, [FromBody] Tarea model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Los datos de la tarea no son válidos",
                        errorCode = 400
                    });
                }

                model.Id = id;
                model.idUsuario = idUsuario;

                var resultado = await service.Put(TareaToTareaDto(model));
                return Ok(new
                {
                    success = true,
                    message = "Tarea actualizada exitosamente",
                    errorCode = 200,
                    data = resultado
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error editando la tarea: {e.Message}",
                    errorCode = 500
                });
            }
        }

        [HttpDelete("Delete-Tarea/{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            try
            {
                var resultado = await service.Delete(id);

                if (resultado == "Tarea eliminada")
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Tarea eliminada exitosamente",
                        errorCode = 200,
                        data = resultado
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = resultado,
                        errorCode = 404
                    });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error borrando la tarea: {e.Message}",
                    errorCode = 500
                });
            }
        }
    }
}

