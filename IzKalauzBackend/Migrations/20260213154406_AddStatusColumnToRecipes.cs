using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IzKalauzBackend.Migrations
{
    public partial class AddStatusColumnToRecipes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Csak a hiányzó oszlopot adjuk hozzá, a táblákat nem bántjuk, mert már léteznek
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Recipes",
                type: "TEXT",
                nullable: false,
                defaultValue: "Active");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Recipes");
        }
    }
}