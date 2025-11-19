using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firmezaa.Web.Migrations
{
    /// <inheritdoc />
    public partial class FixProductQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stock",
                table: "products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
