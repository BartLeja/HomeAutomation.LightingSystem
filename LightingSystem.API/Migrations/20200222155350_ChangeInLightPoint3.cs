using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class ChangeInLightPoint3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bulb_LightPoint_LightPointId",
                table: "Bulb");

            migrationBuilder.DropForeignKey(
                name: "FK_LightPoint_homelightsystem_HomeLightSystemLocalLightingSyst~",
                table: "LightPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LightPoint",
                table: "LightPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bulb",
                table: "Bulb");

            migrationBuilder.RenameTable(
                name: "LightPoint",
                newName: "lightPoint");

            migrationBuilder.RenameTable(
                name: "Bulb",
                newName: "bulb");

            migrationBuilder.RenameIndex(
                name: "IX_LightPoint_HomeLightSystemLocalLightingSystemId",
                table: "lightPoint",
                newName: "IX_lightPoint_HomeLightSystemLocalLightingSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_Bulb_LightPointId",
                table: "bulb",
                newName: "IX_bulb_LightPointId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lightPoint",
                table: "lightPoint",
                column: "LightPointId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bulb",
                table: "bulb",
                column: "Id");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_bulb",
                table: "bulb");

            migrationBuilder.RenameTable(
                name: "lightPoint",
                newName: "LightPoint");

            migrationBuilder.RenameTable(
                name: "bulb",
                newName: "Bulb");

            migrationBuilder.RenameIndex(
                name: "IX_lightPoint_HomeLightSystemLocalLightingSystemId",
                table: "LightPoint",
                newName: "IX_LightPoint_HomeLightSystemLocalLightingSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_bulb_LightPointId",
                table: "Bulb",
                newName: "IX_Bulb_LightPointId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LightPoint",
                table: "LightPoint",
                column: "LightPointId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bulb",
                table: "Bulb",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bulb_LightPoint_LightPointId",
                table: "Bulb",
                column: "LightPointId",
                principalTable: "LightPoint",
                principalColumn: "LightPointId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LightPoint_homelightsystem_HomeLightSystemLocalLightingSyst~",
                table: "LightPoint",
                column: "HomeLightSystemLocalLightingSystemId",
                principalTable: "homelightsystem",
                principalColumn: "locallightingsystemid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
