using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Firmezaa.Web.Migrations
{
    /// <inheritdoc />
    public partial class FixBrokenAddAgeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_users_UserId1",
                table: "sales");

            migrationBuilder.DropIndex(
                name: "IX_sales_UserId1",
                table: "sales");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "sales");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "users",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "sales",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_sales_UserId",
                table: "sales",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_users_UserId",
                table: "sales",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sales_users_UserId",
                table: "sales");

            migrationBuilder.DropIndex(
                name: "IX_sales_UserId",
                table: "sales");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "sales",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "sales",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sales_UserId1",
                table: "sales",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_sales_users_UserId1",
                table: "sales",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
