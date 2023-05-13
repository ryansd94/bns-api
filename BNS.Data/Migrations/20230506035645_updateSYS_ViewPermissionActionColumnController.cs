using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateSYS_ViewPermissionActionColumnController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Action",
                table: "SYS_ViewPermissionActions");

            migrationBuilder.AddColumn<int>(
                name: "Controller",
                table: "SYS_ViewPermissionActions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Controller",
                table: "SYS_ViewPermissionActions");

            migrationBuilder.AddColumn<string>(
                name: "Action",
                table: "SYS_ViewPermissionActions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
