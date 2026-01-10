using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cattobot.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddTmdbIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Films_KinopoiskId",
                table: "Films");

            migrationBuilder.CreateIndex(
                name: "IX_Films_TmdbId",
                table: "Films",
                column: "TmdbId",
                unique: true,
                filter: "\"TmdbId\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Films_TmdbId",
                table: "Films");

            migrationBuilder.CreateIndex(
                name: "IX_Films_KinopoiskId",
                table: "Films",
                column: "KinopoiskId",
                unique: true,
                filter: "\"KinopoiskId\" IS NOT NULL");
        }
    }
}
