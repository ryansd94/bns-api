using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class UpdateJM_StatusAddActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "JM_Status",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApplyAll",
                table: "JM_Status",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAutomaticAdd",
                table: "JM_Status",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "JM_Status");

            migrationBuilder.DropColumn(
                name: "IsApplyAll",
                table: "JM_Status");

            migrationBuilder.DropColumn(
                name: "IsAutomaticAdd",
                table: "JM_Status");
        }
    }
}
