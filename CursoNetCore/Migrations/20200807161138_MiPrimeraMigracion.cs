using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CursoNetCore.Migrations
{
    public partial class MiPrimeraMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cat");

            migrationBuilder.CreateTable(
                name: "Categoria",
                schema: "Cat",
                columns: table => new
                {
                    IdCategoria = table.Column<Guid>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 50, nullable: false),
                    Activo = table.Column<bool>(nullable: false),
                    FechaCreo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.IdCategoria);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categoria",
                schema: "Cat");
        }
    }
}
