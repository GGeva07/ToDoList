using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoListAPI.Core.Application;
using ToDoListAPI.Core.Application.SignalR;
using ToDoListAPI.Infrastructure.Persistence;
using ToDoListAPI.Infrastructure.Persistence.Context;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.ResgistrationPersistenceLayer(builder.Configuration);
            builder.Services.RegistrationApplicationLayer();

            builder.Services.AddSignalR();

            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("http://127.0.0.1:5500")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            var app = builder.Build();

            app.MapHub<SignalHub>("/tareaHub");

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TodoListDBContext>();

                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                        Console.WriteLine("Migraciones aplicadas exitosamente.");
                    }

                    DbInitializer.Initialize(context);
                    Console.WriteLine("Base de datos inicializada correctamente.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error durante la inicialización: {ex.Message}");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseCors("AllowSpecificOrigin");

            app.UseRouting();
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.MapControllers();

            app.Lifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine($"Swagger UI: https://localhost:5001/swagger");
            });

            app.Run();
        }
    }
}