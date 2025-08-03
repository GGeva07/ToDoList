using Microsoft.Extensions.DependencyInjection;
using ToDoList.Services;
using ToDoListAPI.Core.Application.Interfaces;
using ToDoListAPI.Core.Application.Services;

namespace ToDoListAPI.Core.Application
{
    public static class ServiceRegistration
    {
        public static void RegistrationApplicationLayer(this IServiceCollection service)
        {
            service.AddTransient<ITarea, TareaService>();
            service.AddTransient<ILogin, LoginService>();
            service.AddTransient<IUsuario, UsuarioService>();
        }
    }
}
