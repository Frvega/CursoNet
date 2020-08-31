using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CursoNetCore.Migrations
{
    public partial class cuartaMigraciont : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaltPass",
                table: "Usuario");

            migrationBuilder.AddColumn<byte[]>(
                name: "SaltPassword",
                table: "Usuario",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaltPassword",
                table: "Usuario");

            migrationBuilder.AddColumn<byte[]>(
                name: "SaltPass",
                table: "Usuario",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
