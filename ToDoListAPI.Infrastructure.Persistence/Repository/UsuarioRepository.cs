using ToDoListAPI.Core.Domain.Entities;
using ToDoListAPI.Core.Domain.Interfaces;
using ToDoListAPI.Infrastructure.Persistence.Context;

namespace ToDoListAPI.Infrastructure.Persistence.Repository
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        private readonly TodoListDBContext _context;

        public UsuarioRepository(TodoListDBContext context) : base(context) 
        {
            _context = context;
        }
    }
}
