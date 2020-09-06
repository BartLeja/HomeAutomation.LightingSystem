using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class NewStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "homelightsystem",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homelightsystem", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lightsgroup",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    LightGroupName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lightsgroup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "lightpoint",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    lightsgroupid = table.Column<Guid>(nullable: false),
                    CustomName = table.Column<string>(nullable: true),
                    IsAvailable = table.Column<bool>(nullable: false),
                    homelightsystemid = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lightpoint", x => x.id);
                    table.ForeignKey(
                        name: "FK_lightpoint_homelightsystem_homelightsystemid",
                        column: x => x.homelightsystemid,
                        principalTable: "homelightsystem",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lightpoint_lightsgroup_lightsgroupid",
                        column: x => x.lightsgroupid,
                        principalTable: "lightsgroup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lightbulb",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    lightpointid = table.Column<Guid>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_lightpoint_homelightsystemid",
                table: "lightpoint",
                column: "homelightsystemid");

            migrationBuilder.CreateIndex(
                name: "IX_lightpoint_lightsgroupid",
                table: "lightpoint",
                column: "lightsgroupid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lightbulb");

            migrationBuilder.DropTable(
                name: "lightpoint");

            migrationBuilder.DropTable(
                name: "homelightsystem");

            migrationBuilder.DropTable(
                name: "lightsgroup");
        }
    }
}
