using Microsoft.EntityFrameworkCore.Migrations;

namespace GamersHub.Api.Data.Migrations
{
    public partial class AddOfferUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OfferUrl",
                table: "GameOffers",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferUrl",
                table: "GameOffers");
        }
    }
}
