using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class ChangeInLightPoint10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lightpoint_homelightsystem_homelightsystemlocallightingsyst~",
                table: "lightpoint");

            migrationBuilder.DropTable(
                name: "bulb");

            migrationBuilder.RenameColumn(
                name: "homelightsystemlocallightingsystemid",
                table: "lightpoint",
                newName: "homelightsystemid");

            migrationBuilder.RenameColumn(
                name: "lightpointid",
                table: "lightpoint",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_lightpoint_homelightsystemlocallightingsystemid",
                table: "lightpoint",
                newName: "IX_lightpoint_homelightsystemid");

            migrationBuilder.RenameColumn(
                name: "locallightingsystemid",
                table: "homelightsystem",
                newName: "id");

            migrationBuilder.CreateTable(
                name: "lightbulb",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    lightpointid = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lightbulb", x => x.id);
                    table.ForeignKey(
                        name: "FK_lightbulb_lightpoint_lightpointid",
                        column: x => x.lightpointid,
                        principalTable: "lightpoint",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lightbulb_lightpointid",
                table: "lightbulb",
                column: "lightpointid");

            migrationBuilder.AddForeignKey(
                name: "FK_lightpoint_homelightsystem_homelightsystemid",
                table: "lightpoint",
                column: "homelightsystemid",
                principalTable: "homelightsystem",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lightpoint_homelightsystem_homelightsystemid",
                table: "lightpoint");

            migrationBuilder.DropTable(
                name: "lightbulb");

            migrationBuilder.RenameColumn(
                name: "homelightsystemid",
                table: "lightpoint",
                newName: "homelightsystemlocallightingsystemid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "lightpoint",
                newName: "lightpointid");

            migrationBuilder.RenameIndex(
                name: "IX_lightpoint_homelightsystemid",
                table: "lightpoint",
                newName: "IX_lightpoint_homelightsystemlocallightingsystemid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "homelightsystem",
                newName: "locallightingsystemid");

            migrationBuilder.CreateTable(
                name: "bulb",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    lightpointid = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bulb", x => x.id);
                    table.ForeignKey(
                        name: "FK_bulb_lightpoint_lightpointid",
                        column: x => x.lightpointid,
                        principalTable: "lightpoint",
                        principalColumn: "lightpointid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bulb_lightpointid",
                table: "bulb",
                column: "lightpointid");

            migrationBuilder.AddForeignKey(
                name: "FK_lightpoint_homelightsystem_homelightsystemlocallightingsyst~",
                table: "lightpoint",
                column: "homelightsystemlocallightingsystemid",
                principalTable: "homelightsystem",
                principalColumn: "locallightingsystemid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
