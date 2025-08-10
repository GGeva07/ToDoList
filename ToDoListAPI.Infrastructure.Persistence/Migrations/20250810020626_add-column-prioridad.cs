using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListAPI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnprioridad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prioridad",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prioridad",
                table: "Tareas");
        }
    }
}
