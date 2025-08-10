using ToDoListAPI.Core.Domain.Enum;

namespace ToDoListAPI.Core.Domain.Entities.TiposTareas
{
    public abstract class TareaBase : Tarea
    {
        public abstract string GetTaskType();
        public abstract Tarea SetPriorityByType(Tarea tarea);
    }

    public class Practica : TareaBase
    {
        public override string GetTaskType() => "PRACTICA";
        public override Tarea SetPriorityByType(Tarea tarea)
        {
            tarea.Prioridad = PrioridadTarea.SUPERALTA;
            return tarea;
        }
    }

    public class Cuestionario : TareaBase
    {
        public override string GetTaskType() => "CUESTIONARIO";
        public override Tarea SetPriorityByType(Tarea tarea)
        {
            tarea.Prioridad = PrioridadTarea.MEDIA;
            return tarea;
        }
    }

    public class Asignacion : TareaBase
    {
        public override string GetTaskType() => "ASIGNACION";
        public override Tarea SetPriorityByType(Tarea tarea)
        {
            tarea.Prioridad = PrioridadTarea.ALTA;
            return tarea;
        }
    }
}
