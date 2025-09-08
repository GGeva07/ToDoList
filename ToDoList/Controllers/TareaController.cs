using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Core.Application.DTos;
using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Application.Services;

namespace ToDoList.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITarea service;
        private readonly INotificacion notificacionService;

        public TareaController(ITarea service, INotificacion notificacionService)
        {
            this.service = service;
            this.notificacionService = notificacionService;
        }

        [HttpGet("Get-Tareas")]
        public async Task<IActionResult> GetTareas()
        {
            var tareas = await service.Get();
            if (tareas.Any())
            {
                return Ok(new
                {
                    success = true,
                    message = "Tareas obtenidas exitosamente",
                    statusCode = 200,
                    data = tareas
                });
            }
            else
            {
                return NotFound(new
                {
                    success = false,
                    message = "No se encontraron tareas",
                    statusCode = 404
                });
            }
        }

        [HttpGet("Get-TareaById/{id}")]
        public async Task<IActionResult> GetTareaById(int id)
        {
            var tarea = await service.GetById(id);
            if (tarea == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Tarea no encontrada",
                    statusCode = 404
                });
            }

            return Ok(new
            {
                success = true,
                message = "Tarea obtenida exitosamente",
                statusCode = 200,
                data = tarea
            });
        }

        [HttpGet("Get-TareaByTitle/{Nombre}")]
        public async Task<IActionResult> GetTareaByNombre(string Nombre)
        {
            var tareas = await service.GetTareasByNombre(Nombre);
            if (tareas.Any())
            {
                return Ok(new
                {
                    success = true,
                    message = "Tareas encontradas por nombre exitosamente",
                    statusCode = 200,
                    data = tareas
                });
            }
            else
            {
                return NotFound(new
                {
                    success = false,
                    message = $"No se encontraron tareas con el titulo: {Nombre}",
                    statusCode = 404
                });
            }
        }

        [HttpPost("Post-Tarea")]
        public async Task<IActionResult> PostTarea([FromBody] TareaDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Los datos de la tarea no son válidos",
                    statusCode = 400
                });
            }

            var resultado = await service.GetById(dto.Id);
            if (resultado != null)
            {
                await notificacionService.NotificarTareaCreada(resultado);
            }

            return Ok(new
            {
                success = true,
                message = "Tarea creada exitosamente",
                statusCode = 200,
                data = resultado
            });
        }

        [HttpPut("Put-Tarea/{id}")]
        public async Task<IActionResult> PutTarea(int id, [FromBody] TareaDto dto)
        {
            if (dto == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Los datos de la tarea no son válidos",
                    statusCode = 400
                });
            }

            dto.Id = id;
            var resultado = await service.Put(dto);
            var tareaActualizada = await service.GetById(id);
            
            if (tareaActualizada != null)
            {
                await notificacionService.NotificarTareaActualizada(tareaActualizada);
            }

            return Ok(new
            {
                success = true,
                message = "Tarea actualizada exitosamente",
                statusCode = 200,
                data = resultado
            });
        }

        [HttpDelete("Delete-Tarea/{id}")]
        public async Task<IActionResult> DeleteTarea(int id)
        {
            var resultado = await service.Delete(id);

            if (resultado == "Tarea eliminada correctamente")
            { 
                await notificacionService.NotificarTareaEliminada(id);

                return Ok(new
                {
                    success = true,
                    message = resultado,
                    statusCode = 200
                });
            }
            else
            {
                return NotFound(new
                {
                    success = false,
                    message = resultado,
                    statusCode = 404
                });
            }
        }

        [HttpDelete("DeleteAll-Tareas")]
        public async Task<IActionResult> DeleteTarea(int[] ids)
        {
            var resultado = await service.Delete(ids);

            if (resultado == "Tareas eliminadas correctamente")
            {
                foreach (var id in ids)
                {
                    await notificacionService.NotificarTareaEliminada(id);
                }

                return Ok(new
                {
                    success = true,
                    message = resultado,
                    statusCode = 200
                });
            }
            else
            {
                return NotFound(new
                {
                    success = false,
                    message = resultado,
                    statusCode = 404
                });
            }
        }
    }
}

