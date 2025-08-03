using Microsoft.EntityFrameworkCore;
using ToDoListAPI.Core.Domain.Interfaces;

namespace ToDoListAPI.Infrastructure.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        public static DbContext context;

        public GenericRepository(DbContext _context)
        {
            context = _context ?? throw new ArgumentNullException(nameof(_context));
        }
        
        public virtual async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity!);
            await context.SaveChangesAsync();
            return entity;

        }

        public virtual async Task<T> DeleteAsync(int id)
        {
            var entry = await context.Set<T>().FindAsync(id);
            context.Remove(entry!);
            context.SaveChanges();

            return entry!;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await context.Set<T>().ToListAsync();
            return entities;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            return entity!;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            var entry = context.Set<T>().Entry(entity);
            context.Set<T>().Update(entry.Entity);
            await context.SaveChangesAsync();

            return entry.Entity;
        }
    }
}
