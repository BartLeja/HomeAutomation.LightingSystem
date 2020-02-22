using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "homelightsystem",
                columns: table => new
                {
                    locallightingsystemid = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_homelightsystem", x => x.locallightingsystemid);
                });

            migrationBuilder.CreateTable(
                name: "LightPoint",
                columns: table => new
                {
                    LightPointId = table.Column<Guid>(nullable: false),
                    CustomName = table.Column<string>(nullable: true),
                    HomeLightSystemLocalLightingSystemId = table.Column<Guid>(nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    MqttId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightPoint", x => x.LightPointId);
                    table.ForeignKey(
                        name: "FK_LightPoint_homelightsystem_HomeLightSystemLocalLightingSyst~",
                        column: x => x.HomeLightSystemLocalLightingSystemId,
                        principalTable: "homelightsystem",
                        principalColumn: "locallightingsystemid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bulb",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LightPointId = table.Column<Guid>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bulb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bulb_LightPoint_LightPointId",
                        column: x => x.LightPointId,
                        principalTable: "LightPoint",
                        principalColumn: "LightPointId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bulb_LightPointId",
                table: "Bulb",
                column: "LightPointId");

            migrationBuilder.CreateIndex(
                name: "IX_LightPoint_HomeLightSystemLocalLightingSystemId",
                table: "LightPoint",
                column: "HomeLightSystemLocalLightingSystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bulb");

            migrationBuilder.DropTable(
                name: "LightPoint");

            migrationBuilder.DropTable(
                name: "homelightsystem");
        }
    }
}
