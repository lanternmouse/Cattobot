using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cattobot.Db.Migrations
{
    /// <inheritdoc />
    public partial class RemoveShortDescriptionAndRenameRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "RatingImdb",
                table: "Films",
                newName: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Films",
                newName: "RatingImdb");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Films",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
