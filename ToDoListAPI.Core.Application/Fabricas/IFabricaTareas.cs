using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Entities.TiposTareas;

namespace ToDoListAPI.Core.Application.Fabricas
{
    public interface IFabricaTareas
    {
        TareaBase OctenerTareaFactory(Tarea tarea);
    }
}
