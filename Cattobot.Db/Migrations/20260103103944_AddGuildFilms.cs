using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cattobot.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddGuildFilms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM \"Films\"");
            
            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "IsWatched",
                table: "Films",
                newName: "IsSeries");

            migrationBuilder.AddColumn<string[]>(
                name: "Countries",
                table: "Films",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string[]>(
                name: "Genres",
                table: "Films",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Films",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PreviewImageUrl",
                table: "Films",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Films",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FilmGuilds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilmId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuildId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    FilmStatus = table.Column<int>(type: "integer", nullable: false),
                    StatusOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmGuilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmGuilds_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilmGuildMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FilmGuildId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmGuildMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmGuildMembers_FilmGuilds_FilmGuildId",
                        column: x => x.FilmGuildId,
                        principalTable: "FilmGuilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Films_KinopoiskId",
                table: "Films",
                column: "KinopoiskId",
                unique: true,
                filter: "\"KinopoiskId\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGuildMembers_FilmGuildId",
                table: "FilmGuildMembers",
                column: "FilmGuildId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGuildMembers_UserId",
                table: "FilmGuildMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGuilds_FilmId",
                table: "FilmGuilds",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGuilds_GuildId",
                table: "FilmGuilds",
                column: "GuildId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGuilds_GuildId_FilmId",
                table: "FilmGuilds",
                columns: new[] { "GuildId", "FilmId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FilmGuildMembers");

            migrationBuilder.DropTable(
                name: "FilmGuilds");

            migrationBuilder.DropIndex(
                name: "IX_Films_KinopoiskId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Countries",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "PreviewImageUrl",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Films");

            migrationBuilder.RenameColumn(
                name: "IsSeries",
                table: "Films",
                newName: "IsWatched");

            migrationBuilder.AddColumn<decimal>(
                name: "AddedBy",
                table: "Films",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Films",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "GuildId",
                table: "Films",
                type: "numeric(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Films",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
