using ToDoListAPI.Core.Domain.Enum;

namespace ToDoListAPI.Core.Domain.Entities.TiposTareas
{
    public abstract class TareaBase : Tarea
    {
        public abstract string GetTaskType();
    }

    public class Practica : TareaBase
    {
        public override string GetTaskType() => "PRACTICA";
    }

    public class Cuestionario : TareaBase
    {
        public override string GetTaskType() => "CUESTIONARIO";
    }

    public class Asignacion : TareaBase
    {
        public override string GetTaskType() => "ASIGNACION";
    }
}
