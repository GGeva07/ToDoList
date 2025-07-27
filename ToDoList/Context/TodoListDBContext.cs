using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Context
{
    public class TodoListDBContext : DbContext
    {
        public TodoListDBContext(DbContextOptions<TodoListDBContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tarea> Tarea { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(e => e.id);
            });


            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.ToTable("Tareas");
                entity.HasKey(e => e.id);


                entity.HasOne<Usuario>()
                    .WithMany()
                    .HasForeignKey(t => t.idUsuario)
                    .OnDelete(DeleteBehavior.Cascade);
            });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("SServer=localhost\\SQLEXPRESS;Database=TodolistBD;Trusted_Connection=True; TrustServerCertificate=True;");
            }
        }
    }
}
