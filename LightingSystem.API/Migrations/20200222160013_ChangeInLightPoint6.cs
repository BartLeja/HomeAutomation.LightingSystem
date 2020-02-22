using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class ChangeInLightPoint6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lightpoint_homelightsystem_homelightsystemlocallightinglyst~",
                table: "lightpoint");

            migrationBuilder.RenameColumn(
                name: "homelightsystemlocallightinglystemid",
                table: "lightpoint",
                newName: "homelightsystemlocallightingsystemid");

            migrationBuilder.RenameIndex(
                name: "IX_lightpoint_homelightsystemlocallightinglystemid",
                table: "lightpoint",
                newName: "IX_lightpoint_homelightsystemlocallightingsystemid");

            migrationBuilder.AddForeignKey(
                name: "FK_lightpoint_homelightsystem_homelightsystemlocallightingsyst~",
                table: "lightpoint",
                column: "homelightsystemlocallightingsystemid",
                principalTable: "homelightsystem",
                principalColumn: "locallightingsystemid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lightpoint_homelightsystem_homelightsystemlocallightingsyst~",
                table: "lightpoint");

            migrationBuilder.RenameColumn(
                name: "homelightsystemlocallightingsystemid",
                table: "lightpoint",
                newName: "homelightsystemlocallightinglystemid");

            migrationBuilder.RenameIndex(
                name: "IX_lightpoint_homelightsystemlocallightingsystemid",
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
    }
}
