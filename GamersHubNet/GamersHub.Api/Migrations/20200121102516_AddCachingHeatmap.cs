using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamersHub.Api.Migrations
{
    public partial class AddCachingHeatmap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratedHeatMaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    HeatMap = table.Column<byte[]>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    GeneratedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedHeatmaps", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneratedHeatMaps");
        }
    }
}
