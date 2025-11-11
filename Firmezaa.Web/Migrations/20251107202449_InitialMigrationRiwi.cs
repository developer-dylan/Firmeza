using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firmezaa.Web.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationRiwi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddProduct",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddProduct",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "products");
        }
    }
}
