using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace URLShortener.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ShortenedCode",
                table: "Urls",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "Urls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<int>(
                name: "HitCount",
                table: "Urls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.CreateIndex(
                name: "IX_Urls_ShortenedCode",
                table: "Urls",
                column: "ShortenedCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Urls_ShortenedCode",
                table: "Urls");

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
                name: "ShortenedCode",
                table: "Urls",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "Urls",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "HitCount",
                table: "Urls",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 0);
        }
    }
}
