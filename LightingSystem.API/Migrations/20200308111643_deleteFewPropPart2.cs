using Microsoft.EntityFrameworkCore.Migrations;

namespace LightingSystem.API.Migrations
{
    public partial class deleteFewPropPart2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MqttId",
                table: "lightpoint");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "lightbulb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MqttId",
                table: "lightpoint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "lightbulb",
                nullable: false,
                defaultValue: 0);
        }
    }
}
