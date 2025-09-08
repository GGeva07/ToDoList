using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAPI.Core.Domain.Entities;

namespace ToDoListAPI.Core.Application.Interfaces
{
    public interface INotificacion
    {
        Task NotificarTareaCreada(Tarea tarea);
        Task NotificarTareaActualizada(Tarea tarea);
        Task NotificarTareaEliminada(int tareaId);
    }
}
