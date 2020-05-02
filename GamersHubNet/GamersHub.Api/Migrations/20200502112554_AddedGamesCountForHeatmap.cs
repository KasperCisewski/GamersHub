using Microsoft.EntityFrameworkCore.Migrations;

namespace GamersHub.Api.Migrations
{
    public partial class AddedGamesCountForHeatmap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GamesCount",
                table: "GeneratedHeatMaps",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GamesCount",
                table: "GeneratedHeatMaps");
        }
    }
}
