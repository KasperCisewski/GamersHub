using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamersHub.Api.Migrations
{
    public partial class AddCachingRecommendedGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GamesRecommendationId",
                table: "Games",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GamesRecommendations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    GeneratedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesRecommendations", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GamesRecommendations_GamesRecommendationId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "GamesRecommendations");

            migrationBuilder.DropIndex(
                name: "IX_Games_GamesRecommendationId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "GamesRecommendationId",
                table: "Games");
        }
    }
}
