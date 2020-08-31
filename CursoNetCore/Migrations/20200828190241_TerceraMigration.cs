using Microsoft.EntityFrameworkCore.Migrations;

namespace CursoNetCore.Migrations
{
    public partial class TerceraMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Correo",
                table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Usuario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Usuario");

            migrationBuilder.AddColumn<string>(
                name: "Correo",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
