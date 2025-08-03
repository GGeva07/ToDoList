using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListAPI.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class deletingForeignKeyUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Usuarios_idUsuario",
                table: "Tareas");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_idUsuario",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "idUsuario",
                table: "Tareas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idUsuario",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_idUsuario",
                table: "Tareas",
                column: "idUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Usuarios_idUsuario",
                table: "Tareas",
                column: "idUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
