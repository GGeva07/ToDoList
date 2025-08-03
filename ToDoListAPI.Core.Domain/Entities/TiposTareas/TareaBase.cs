using ToDoListAPI.Core.Domain.Enum;

namespace ToDoListAPI.Core.Domain.Entities.TiposTareas
{
    public abstract class TareaBase : Tarea
    {
        public abstract TipoTarea GetTaskType();
    }

    public class LimpiezaGeneral : TareaBase
    {
        public override TipoTarea GetTaskType() => TipoTarea.LIMPIEZAGENEARAL;
    }
}
