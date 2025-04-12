using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebFrameworksGroupCA2Project.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingCartandCartInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCart_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CartInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false),
                    VinylId = table.Column<int>(type: "int", nullable: true),
                    ShoppingCartId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartInfo_ShoppingCart_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCart",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartInfo_Vinyl_VinylId",
                        column: x => x.VinylId,
                        principalTable: "Vinyl",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartInfo_ShoppingCartId",
                table: "CartInfo",
                column: "ShoppingCartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartInfo_VinylId",
                table: "CartInfo",
                column: "VinylId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_UserId",
                table: "ShoppingCart",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartInfo");

            migrationBuilder.DropTable(
                name: "ShoppingCart");
        }
    }
}
