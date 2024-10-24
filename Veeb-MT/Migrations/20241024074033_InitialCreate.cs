using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Veeb_MT.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kasutajad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parool = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Eesnimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Perenimi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kasutajad", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tooted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nimi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tooted", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ostukorvid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KasutajaId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ostukorvid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ostukorvid_Kasutajad_KasutajaId",
                        column: x => x.KasutajaId,
                        principalTable: "Kasutajad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OstukorvTooted",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OstukorvId = table.Column<int>(type: "int", nullable: false),
                    ToodeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OstukorvTooted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OstukorvTooted_Ostukorvid_OstukorvId",
                        column: x => x.OstukorvId,
                        principalTable: "Ostukorvid",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OstukorvTooted_Tooted_ToodeId",
                        column: x => x.ToodeId,
                        principalTable: "Tooted",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ostukorvid_KasutajaId",
                table: "Ostukorvid",
                column: "KasutajaId");

            migrationBuilder.CreateIndex(
                name: "IX_OstukorvTooted_OstukorvId",
                table: "OstukorvTooted",
                column: "OstukorvId");

            migrationBuilder.CreateIndex(
                name: "IX_OstukorvTooted_ToodeId",
                table: "OstukorvTooted",
                column: "ToodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OstukorvTooted");

            migrationBuilder.DropTable(
                name: "Ostukorvid");

            migrationBuilder.DropTable(
                name: "Tooted");

            migrationBuilder.DropTable(
                name: "Kasutajad");
        }
    }
}
