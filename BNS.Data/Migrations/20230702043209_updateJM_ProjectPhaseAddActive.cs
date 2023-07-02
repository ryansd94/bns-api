using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_ProjectPhaseAddActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "JM_ProjectPhase");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "JM_ProjectPhase",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "JM_ProjectPhase");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "JM_ProjectPhase",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
