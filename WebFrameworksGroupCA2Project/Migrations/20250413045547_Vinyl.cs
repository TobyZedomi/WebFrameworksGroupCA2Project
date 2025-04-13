using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebFrameworksGroupCA2Project.Migrations
{
    /// <inheritdoc />
    public partial class Vinyl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vinyl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VinylName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfRelease = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    VinylInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vinyl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vinyl_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artist",
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

            migrationBuilder.CreateIndex(
                name: "IX_Vinyl_ArtistId",
                table: "Vinyl",
                column: "ArtistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vinyl");
        }
    }
}
