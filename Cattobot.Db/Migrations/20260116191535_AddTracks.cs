using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cattobot.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddTracks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrackDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Artist = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    ThumbnailUrl = table.Column<string>(type: "text", nullable: true),
                    ArtistUrl = table.Column<string>(type: "text", nullable: true),
                    ExternalUrl = table.Column<string>(type: "text", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackQueueDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GuildId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    CurrentTrackId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackQueueDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackQueueItemDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QueueId = table.Column<Guid>(type: "uuid", nullable: false),
                    TrackId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrevItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    NextItemId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackQueueItemDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackQueueItemDb_TrackDb_TrackId",
                        column: x => x.TrackId,
                        principalTable: "TrackDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackQueueItemDb_TrackQueueDb_QueueId",
                        column: x => x.QueueId,
                        principalTable: "TrackQueueDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackQueueItemDb_TrackQueueItemDb_NextItemId",
                        column: x => x.NextItemId,
                        principalTable: "TrackQueueItemDb",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TrackQueueItemDb_TrackQueueItemDb_PrevItemId",
                        column: x => x.PrevItemId,
                        principalTable: "TrackQueueItemDb",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackDb_ExternalUrl",
                table: "TrackDb",
                column: "ExternalUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackQueueDb_CurrentTrackId",
                table: "TrackQueueDb",
                column: "CurrentTrackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackQueueDb_GuildId",
                table: "TrackQueueDb",
                column: "GuildId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackQueueItemDb_NextItemId",
                table: "TrackQueueItemDb",
                column: "NextItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackQueueItemDb_PrevItemId",
                table: "TrackQueueItemDb",
                column: "PrevItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrackQueueItemDb_QueueId",
                table: "TrackQueueItemDb",
                column: "QueueId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackQueueItemDb_TrackId",
                table: "TrackQueueItemDb",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackQueueDb_TrackQueueItemDb_CurrentTrackId",
                table: "TrackQueueDb",
                column: "CurrentTrackId",
                principalTable: "TrackQueueItemDb",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackQueueDb_TrackQueueItemDb_CurrentTrackId",
                table: "TrackQueueDb");

            migrationBuilder.DropTable(
                name: "TrackQueueItemDb");

            migrationBuilder.DropTable(
                name: "TrackDb");

            migrationBuilder.DropTable(
                name: "TrackQueueDb");
        }
    }
}
