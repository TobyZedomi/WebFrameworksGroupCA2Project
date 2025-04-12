using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebFrameworksGroupCA2Project.Migrations
{
    /// <inheritdoc />
    public partial class VinylandStockSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    VinylId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stock_Vinyl_VinylId",
                        column: x => x.VinylId,
                        principalTable: "Vinyl",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Vinyl",
                columns: new[] { "Id", "ArtistId", "DateOfRelease", "ImageFileName", "ListPrice", "VinylInfo", "VinylName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1968, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "EL.jpg", 30.0, "The third studio album by the artist Jimi Hnedrix", "Electric Ladyland" },
                    { 2, 2, new DateTime(2015, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "tpab.jpg", 30.0, "The third studio album by the artist Kendrick Lamar", "To Pimp A Butterfly" },
                    { 3, 3, new DateTime(2015, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "30.jpg", 20.0, "The comeback studio album by the artist Adele", "30" },
                    { 4, 4, new DateTime(1984, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "pr.jpg", 40.0, "The most popular album by the artist Prince", "Purple Rain" },
                    { 5, 5, new DateTime(1973, 6, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "ZiggyStardust.jpg", 20.0, "The most popular album by the artist David Bowie", "Ziggy Stardust" }
                });

            migrationBuilder.InsertData(
                table: "Stock",
                columns: new[] { "Id", "Quantity", "VinylId" },
                values: new object[,]
                {
                    { 1, 50, 1 },
                    { 2, 5, 2 },
                    { 3, 50, 3 },
                    { 4, 50, 4 },
                    { 5, 10, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stock_VinylId",
                table: "Stock",
                column: "VinylId",
                unique: true,
                filter: "[VinylId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DeleteData(
                table: "Vinyl",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vinyl",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vinyl",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vinyl",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vinyl",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
