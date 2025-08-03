using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoListAPI.Core.Application.Fabricas;
using ToDoListAPI.Core.Domain.Interfaces;
using ToDoListAPI.Infrastructure.Persistence.Context;
using ToDoListAPI.Infrastructure.Persistence.Fabricas;
using ToDoListAPI.Infrastructure.Persistence.Repository;

namespace ToDoListAPI.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void ResgistrationPersistenceLayer(this IServiceCollection service, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection");
            service.AddDbContext<TodoListDBContext>(configuration => configuration.UseSqlServer(connectionString));

            service.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddTransient<ITareaRepository, TareaRespository>();
            service.AddTransient<IUsuarioRepository, UsuarioRepository>();
            service.AddTransient<IFabricaTareas, FabricaTareas>();
        }
    }
}
