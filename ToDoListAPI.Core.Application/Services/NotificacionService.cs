using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAPI.Core.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;
using ToDoListAPI.Core.Domain.Interfaces;
using ToDoListAPI.Core.Application.SignalR;
using ToDoListAPI.Core.Domain.Entities;

namespace ToDoListAPI.Core.Application.Services
{
    public class NotificacionService : INotificacion
    {
        private readonly IHubContext<SignalHub> _hubContext;

        public NotificacionService(IHubContext<SignalHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotificarTareaCreada(Tarea tarea)
        {
            await _hubContext.Clients.All.SendAsync("TareaCreada", new
            {
                message = "Nueva tarea creada",
                data = tarea,
                timestamp = DateTime.UtcNow
            });
        }

        public async Task NotificarTareaActualizada(Tarea tarea)
        {
            await _hubContext.Clients.All.SendAsync("TareaActualizada", new
            {
                message = "Tarea actualizada",
                data = tarea,
                timestamp = DateTime.UtcNow
            });
        }

        public async Task NotificarTareaEliminada(int tareaId)
        {
            await _hubContext.Clients.All.SendAsync("TareaEliminada", new
            {
                message = "Tarea eliminada",
                tareaId = tareaId,
                timestamp = DateTime.UtcNow
            });
        }
    }
}
