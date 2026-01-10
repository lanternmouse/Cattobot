using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cattobot.Db.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFilmModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocalizedTitle",
                table: "Films",
                newName: "SearchIndex");

            migrationBuilder.RenameIndex(
                name: "IX_Films_LocalizedTitle",
                table: "Films",
                newName: "IX_Films_SearchIndex");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Films",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "PreviewImageUrl",
                table: "Films",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Films",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string[]>(
                name: "Directors",
                table: "Films",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string>(
                name: "WikidataId",
                table: "Films",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Directors",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "WikidataId",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "SearchIndex",
                table: "Films",
                newName: "LocalizedTitle");

            migrationBuilder.RenameIndex(
                name: "IX_Films_SearchIndex",
                table: "Films",
                newName: "IX_Films_LocalizedTitle");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "Films",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PreviewImageUrl",
                table: "Films",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Films",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
