using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class ChangeInLightPoint4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bulb_lightPoint_LightPointId",
                table: "bulb");

            migrationBuilder.DropForeignKey(
                name: "FK_lightPoint_homelightsystem_HomeLightSystemLocalLightingSyst~",
                table: "lightPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lightPoint",
                table: "lightPoint");

            migrationBuilder.RenameTable(
                name: "lightPoint",
                newName: "lightpoint");

            migrationBuilder.RenameIndex(
                name: "IX_lightPoint_HomeLightSystemLocalLightingSystemId",
                table: "lightpoint",
                newName: "IX_lightpoint_HomeLightSystemLocalLightingSystemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lightpoint",
                table: "lightpoint",
                column: "LightPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_bulb_lightpoint_LightPointId",
                table: "bulb",
                column: "LightPointId",
                principalTable: "lightpoint",
                principalColumn: "LightPointId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lightpoint_homelightsystem_HomeLightSystemLocalLightingSyst~",
                table: "lightpoint",
                column: "HomeLightSystemLocalLightingSystemId",
                principalTable: "homelightsystem",
                principalColumn: "locallightingsystemid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bulb_lightpoint_LightPointId",
                table: "bulb");

            migrationBuilder.DropForeignKey(
                name: "FK_lightpoint_homelightsystem_HomeLightSystemLocalLightingSyst~",
                table: "lightpoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lightpoint",
                table: "lightpoint");

            migrationBuilder.RenameTable(
                name: "lightpoint",
                newName: "lightPoint");

            migrationBuilder.RenameIndex(
                name: "IX_lightpoint_HomeLightSystemLocalLightingSystemId",
                table: "lightPoint",
                newName: "IX_lightPoint_HomeLightSystemLocalLightingSystemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lightPoint",
                table: "lightPoint",
                column: "LightPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_bulb_lightPoint_LightPointId",
                table: "bulb",
                column: "LightPointId",
                principalTable: "lightPoint",
                principalColumn: "LightPointId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lightPoint_homelightsystem_HomeLightSystemLocalLightingSyst~",
                table: "lightPoint",
                column: "HomeLightSystemLocalLightingSystemId",
                principalTable: "homelightsystem",
                principalColumn: "locallightingsystemid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
