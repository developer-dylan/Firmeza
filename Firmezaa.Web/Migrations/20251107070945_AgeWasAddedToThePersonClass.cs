using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firmezaa.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgeWasAddedToThePersonClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "clients",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "clients");
        }
    }
}
