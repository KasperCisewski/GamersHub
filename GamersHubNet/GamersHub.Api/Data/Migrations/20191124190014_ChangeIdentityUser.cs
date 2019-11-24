using Microsoft.EntityFrameworkCore.Migrations;

namespace GamersHub.Api.Data.Migrations
{
    public partial class ChangeIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GamersHubUserId",
                table: "Game",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GamersHubUserId1",
                table: "Game",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GamersHubUserId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Game_GamersHubUserId",
                table: "Game",
                column: "GamersHubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GamersHubUserId1",
                table: "Game",
                column: "GamersHubUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GamersHubUserId",
                table: "AspNetUsers",
                column: "GamersHubUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_GamersHubUserId",
                table: "AspNetUsers",
                column: "GamersHubUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_AspNetUsers_GamersHubUserId",
                table: "Game",
                column: "GamersHubUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_AspNetUsers_GamersHubUserId1",
                table: "Game",
                column: "GamersHubUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_GamersHubUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_AspNetUsers_GamersHubUserId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_AspNetUsers_GamersHubUserId1",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_GamersHubUserId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_GamersHubUserId1",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GamersHubUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GamersHubUserId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GamersHubUserId1",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GamersHubUserId",
                table: "AspNetUsers");
        }
    }
}
