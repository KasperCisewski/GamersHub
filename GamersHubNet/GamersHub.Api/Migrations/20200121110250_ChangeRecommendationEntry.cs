using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamersHub.Api.Migrations
{
    public partial class ChangeRecommendationEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecommendationEntry_GamesRecommendations_GameId",
                table: "RecommendationEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendationEntry_Games_GameId1",
                table: "RecommendationEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecommendationEntry",
                table: "RecommendationEntry");

            migrationBuilder.DropIndex(
                name: "IX_RecommendationEntry_GameId1",
                table: "RecommendationEntry");

            migrationBuilder.DropColumn(
                name: "GameId1",
                table: "RecommendationEntry");

            migrationBuilder.AlterColumn<Guid>(
                name: "GamesRecommendationId",
                table: "RecommendationEntry",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RecommendationEntry",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecommendationEntry",
                table: "RecommendationEntry",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationEntry_GameId",
                table: "RecommendationEntry",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationEntry_GamesRecommendationId",
                table: "RecommendationEntry",
                column: "GamesRecommendationId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendationEntry_Games_GameId",
                table: "RecommendationEntry",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendationEntry_GamesRecommendations_GamesRecommendationId",
                table: "RecommendationEntry",
                column: "GamesRecommendationId",
                principalTable: "GamesRecommendations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecommendationEntry_Games_GameId",
                table: "RecommendationEntry");

            migrationBuilder.DropForeignKey(
                name: "FK_RecommendationEntry_GamesRecommendations_GamesRecommendationId",
                table: "RecommendationEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecommendationEntry",
                table: "RecommendationEntry");

            migrationBuilder.DropIndex(
                name: "IX_RecommendationEntry_GameId",
                table: "RecommendationEntry");

            migrationBuilder.DropIndex(
                name: "IX_RecommendationEntry_GamesRecommendationId",
                table: "RecommendationEntry");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecommendationEntry");

            migrationBuilder.AlterColumn<Guid>(
                name: "GamesRecommendationId",
                table: "RecommendationEntry",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GameId1",
                table: "RecommendationEntry",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecommendationEntry",
                table: "RecommendationEntry",
                columns: new[] { "GameId", "GamesRecommendationId" });

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationEntry_GameId1",
                table: "RecommendationEntry",
                column: "GameId1");

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendationEntry_GamesRecommendations_GameId",
                table: "RecommendationEntry",
                column: "GameId",
                principalTable: "GamesRecommendations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendationEntry_Games_GameId1",
                table: "RecommendationEntry",
                column: "GameId1",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
