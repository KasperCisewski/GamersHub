using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamersHub.Api.Data.Migrations
{
    public partial class AddJointTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_GamersHubUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_GameImages_CoverGameImageId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_AspNetUsers_GamersHubUserId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_AspNetUsers_GamersHubUserId1",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_GameImages_Game_GameId",
                table: "GameImages");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOffer_Game_GameId",
                table: "GameOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOffer_Store_StoreId",
                table: "GameOffer");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Game_GameId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_GamersHubUserId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Store",
                table: "Store");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameOffer",
                table: "GameOffer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_GamersHubUserId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_GamersHubUserId1",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GamersHubUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GamersHubUserId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GamersHubUserId1",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Store",
                newName: "Stores");

            migrationBuilder.RenameTable(
                name: "GameOffer",
                newName: "GameOffers");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_GameOffer_StoreId",
                table: "GameOffers",
                newName: "IX_GameOffers_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_GameOffer_GameId",
                table: "GameOffers",
                newName: "IX_GameOffers_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Game_CoverGameImageId",
                table: "Games",
                newName: "IX_Games_CoverGameImageId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stores",
                table: "Stores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameOffers",
                table: "GameOffers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    CurrentUserId = table.Column<string>(nullable: false),
                    FriendId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.CurrentUserId, x.FriendId });
                });

            migrationBuilder.CreateTable(
                name: "UserGame",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGame", x => new { x.GameId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserGame_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGame_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishListEntry",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    GameId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListEntry", x => new { x.GameId, x.UserId });
                    table.ForeignKey(
                        name: "FK_WishListEntry_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListEntry_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGame_UserId",
                table: "UserGame",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListEntry_UserId",
                table: "WishListEntry",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameImages_Games_GameId",
                table: "GameImages",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameOffers_Games_GameId",
                table: "GameOffers",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameOffers_Stores_StoreId",
                table: "GameOffers",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameImages_CoverGameImageId",
                table: "Games",
                column: "CoverGameImageId",
                principalTable: "GameImages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Games_GameId",
                table: "Videos",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameImages_Games_GameId",
                table: "GameImages");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOffers_Games_GameId",
                table: "GameOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_GameOffers_Stores_StoreId",
                table: "GameOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameImages_CoverGameImageId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Games_GameId",
                table: "Videos");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "UserGame");

            migrationBuilder.DropTable(
                name: "WishListEntry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stores",
                table: "Stores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameOffers",
                table: "GameOffers");

            migrationBuilder.RenameTable(
                name: "Stores",
                newName: "Store");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameTable(
                name: "GameOffers",
                newName: "GameOffer");

            migrationBuilder.RenameIndex(
                name: "IX_Games_CoverGameImageId",
                table: "Game",
                newName: "IX_Game_CoverGameImageId");

            migrationBuilder.RenameIndex(
                name: "IX_GameOffers_StoreId",
                table: "GameOffer",
                newName: "IX_GameOffer_StoreId");

            migrationBuilder.RenameIndex(
                name: "IX_GameOffers_GameId",
                table: "GameOffer",
                newName: "IX_GameOffer_GameId");

            migrationBuilder.AddColumn<string>(
                name: "GamersHubUserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GamersHubUserId",
                table: "Game",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GamersHubUserId1",
                table: "Game",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Store",
                table: "Store",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameOffer",
                table: "GameOffer",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GamersHubUserId",
                table: "AspNetUsers",
                column: "GamersHubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GamersHubUserId",
                table: "Game",
                column: "GamersHubUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GamersHubUserId1",
                table: "Game",
                column: "GamersHubUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_GamersHubUserId",
                table: "AspNetUsers",
                column: "GamersHubUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_GameImages_CoverGameImageId",
                table: "Game",
                column: "CoverGameImageId",
                principalTable: "GameImages",
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

            migrationBuilder.AddForeignKey(
                name: "FK_GameImages_Game_GameId",
                table: "GameImages",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GameOffer_Game_GameId",
                table: "GameOffer",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameOffer_Store_StoreId",
                table: "GameOffer",
                column: "StoreId",
                principalTable: "Store",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Game_GameId",
                table: "Videos",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
