using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExamenRibbit.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class creaciondb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Precio = table.Column<decimal>(type: "TEXT", nullable: false),
                    CantidadEnStock = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "CantidadEnStock", "Descripcion", "FechaCreacion", "Nombre", "Precio" },
                values: new object[,]
                {
                    { 1, 10, null, new DateTime(2025, 1, 15, 4, 47, 57, 156, DateTimeKind.Utc).AddTicks(5506), "Refresco", 100m },
                    { 2, 20, null, new DateTime(2025, 1, 15, 4, 47, 57, 156, DateTimeKind.Utc).AddTicks(5653), "Tortilla", 200m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
