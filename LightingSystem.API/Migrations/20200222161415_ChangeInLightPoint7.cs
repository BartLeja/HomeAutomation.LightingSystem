using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class ChangeInLightPoint7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LightPointId",
                table: "lightpoint",
                newName: "lightpointid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lightpointid",
                table: "lightpoint",
                newName: "LightPointId");
        }
    }
}
