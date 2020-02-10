using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bulb",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bulb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HomeLightSystem",
                columns: table => new
                {
                    LocalLightingSystemId = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeLightSystem", x => x.LocalLightingSystemId);
                });

            migrationBuilder.CreateTable(
                name: "LightPoint",
                columns: table => new
                {
                    LightPointId = table.Column<Guid>(nullable: false),
                    LocalLightingSystemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightPoint", x => x.LightPointId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bulb");

            migrationBuilder.DropTable(
                name: "HomeLightSystem");

            migrationBuilder.DropTable(
                name: "LightPoint");
        }
    }
}
