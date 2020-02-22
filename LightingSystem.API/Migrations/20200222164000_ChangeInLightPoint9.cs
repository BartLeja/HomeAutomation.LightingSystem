using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class ChangeInLightPoint9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bulb_lightpoint_LightPointId",
                table: "bulb");

            migrationBuilder.RenameColumn(
                name: "LightPointId",
                table: "bulb",
                newName: "lightpointid");

            migrationBuilder.RenameIndex(
                name: "IX_bulb_LightPointId",
                table: "bulb",
                newName: "IX_bulb_lightpointid");

            migrationBuilder.AddForeignKey(
                name: "FK_bulb_lightpoint_lightpointid",
                table: "bulb",
                column: "lightpointid",
                principalTable: "lightpoint",
                principalColumn: "lightpointid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bulb_lightpoint_lightpointid",
                table: "bulb");

            migrationBuilder.RenameColumn(
                name: "lightpointid",
                table: "bulb",
                newName: "LightPointId");

            migrationBuilder.RenameIndex(
                name: "IX_bulb_lightpointid",
                table: "bulb",
                newName: "IX_bulb_LightPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_bulb_lightpoint_LightPointId",
                table: "bulb",
                column: "LightPointId",
                principalTable: "lightpoint",
                principalColumn: "lightpointid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
