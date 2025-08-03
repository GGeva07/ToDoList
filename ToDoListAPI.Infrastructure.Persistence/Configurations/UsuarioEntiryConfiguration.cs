using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListAPI.Core.Domain.Entities;

namespace ToDoListAPI.Infrastructure.Persistence.Configurations
{
    internal class UsuarioEntiryConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(e => e.Id);
        }
    }
}
