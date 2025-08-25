using Microsoft.AspNetCore.Mvc;
using ToDoListAPI.Core.Application.DTos;
using ToDoListAPI.Core.Application.Interfaces;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITarea service;
        
        public TareaController(ITarea service)
        {
            this.service = service;
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

        //[HttpGet("Get-Tareas-by-Usuario/{idUsuario}")]
        //public async Task<IActionResult> GetTareasByIdUsuario(int idUsuario)
        //{
        //    try
        //    {
        //        var tareas = await service.GetTareasByIdUsuario(idUsuario);
        //        if (tareas.Any())
        //        {
        //            return Ok(new
        //            {
        //                success = true,
        //                message = "Tareas del usuario obtenidas exitosamente",
        //                errorCode = 200,
        //                data = tareas
        //            });
        //        }
        //        else
        //        {
        //            return NotFound(new
        //            {
        //                success = false,
        //                message = $"No se encontraron tareas para el usuario con id {idUsuario} o el usuario fue eliminado",
        //                errorCode = 404
        //            });
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(500, new
        //        {
        //            success = false,
        //            message = $"Error obteniendo las tareas por el id Usuario: {e.Message}",
        //            errorCode = 500
        //        });
        //    }
        //}

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

            var resultado = await service.Post(dto);
            return Ok(new
            {
                success = true,
                message = "Tarea creada exitosamente",
                errorCode = 200,
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

