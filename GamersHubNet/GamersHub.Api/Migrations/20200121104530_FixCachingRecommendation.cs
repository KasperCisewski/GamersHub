using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamersHub.Api.Migrations
{
    public partial class FixCachingRecommendation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GamesRecommendations_GamesRecommendationId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_GamesRecommendationId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GamesRecommendationId",
                table: "Games");

            migrationBuilder.CreateTable(
                name: "RecommendationEntry",
                columns: table => new
                {
                    GamesRecommendationId = table.Column<Guid>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false),
                    GameId1 = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationEntry", x => new { x.GameId, x.GamesRecommendationId });
                    table.ForeignKey(
                        name: "FK_RecommendationEntry_GamesRecommendations_GameId",
                        column: x => x.GameId,
                        principalTable: "GamesRecommendations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecommendationEntry_Games_GameId1",
                        column: x => x.GameId1,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationEntry_GameId1",
                table: "RecommendationEntry",
                column: "GameId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecommendationEntry");

            migrationBuilder.AddColumn<Guid>(
                name: "GamesRecommendationId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Games_GamesRecommendationId",
                table: "Games",
                column: "GamesRecommendationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GamesRecommendations_GamesRecommendationId",
                table: "Games",
                column: "GamesRecommendationId",
                principalTable: "GamesRecommendations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
