using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class ChangeInLightPoint5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lightpoint_homelightsystem_HomeLightSystemLocalLightingSyst~",
                table: "lightpoint");

            migrationBuilder.RenameColumn(
                name: "HomeLightSystemLocalLightingSystemId",
                table: "lightpoint",
                newName: "homelightsystemlocallightinglystemid");

            migrationBuilder.RenameIndex(
                name: "IX_lightpoint_HomeLightSystemLocalLightingSystemId",
                table: "lightpoint",
                newName: "IX_lightpoint_homelightsystemlocallightinglystemid");

            migrationBuilder.AddForeignKey(
                name: "FK_lightpoint_homelightsystem_homelightsystemlocallightinglyst~",
                table: "lightpoint",
                column: "homelightsystemlocallightinglystemid",
                principalTable: "homelightsystem",
                principalColumn: "locallightingsystemid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lightpoint_homelightsystem_homelightsystemlocallightinglyst~",
                table: "lightpoint");

            migrationBuilder.RenameColumn(
                name: "homelightsystemlocallightinglystemid",
                table: "lightpoint",
                newName: "HomeLightSystemLocalLightingSystemId");

            migrationBuilder.RenameIndex(
                name: "IX_lightpoint_homelightsystemlocallightinglystemid",
                table: "lightpoint",
                newName: "IX_lightpoint_HomeLightSystemLocalLightingSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_lightpoint_homelightsystem_HomeLightSystemLocalLightingSyst~",
                table: "lightpoint",
                column: "HomeLightSystemLocalLightingSystemId",
                principalTable: "homelightsystem",
                principalColumn: "locallightingsystemid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
