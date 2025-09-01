using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListAPI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addingsaltfuncionality : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Usuarios");
        }
    }
}
