using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebFrameworksGroupCA2Project.Migrations
{
    /// <inheritdoc />
    public partial class listPriceForVinylRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ListPrice",
                table: "UserVinylRequest",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListPrice",
                table: "UserVinylRequest");
        }
    }
}
