using Microsoft.Extensions.DependencyInjection;
using ToDoList.Services;
using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Application.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToDoListAPI.Core.Application.Services.Cache;

namespace ToDoListAPI.Core.Application
{
    public static class ServiceRegistration
    {
        public static void RegistrationApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<ITarea, TareaService>();
            services.AddTransient<ILogin, LoginService>();
            services.AddTransient<IUsuario, UsuarioService>();
            services.AddMemoryCache();
            services.AddScoped(typeof(Cache<,>));
        }
    }
}