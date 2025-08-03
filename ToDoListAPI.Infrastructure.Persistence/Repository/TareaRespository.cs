using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Interfaces;
using ToDoListAPI.Infrastructure.Persistence.Context;

namespace ToDoListAPI.Infrastructure.Persistence.Repository
{
    public class TareaRespository : GenericRepository<Tarea>, ITareaRepository
    {
        private readonly TodoListDBContext context;
        public TareaRespository(TodoListDBContext context) : base(context)
        {
            this.context = context;
        }
    }
}
