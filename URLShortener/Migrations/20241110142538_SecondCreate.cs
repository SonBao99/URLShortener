using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.Migrations
{
    /// <inheritdoc />
    public partial class SecondCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Urls");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Urls");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "Urls",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
