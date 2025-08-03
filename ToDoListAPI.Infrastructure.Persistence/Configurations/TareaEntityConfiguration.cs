using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListAPI.Core.Domain.Entities;

namespace ToDoListAPI.Infrastructure.Persistence.Configurations
{
    public class TareaEntityConfiguration : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            builder.ToTable("Tareas");
            builder.HasKey(e => e.Id);


            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(t => t.idUsuario)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
