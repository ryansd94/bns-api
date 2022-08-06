using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_Template : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeIssueStatus",
                table: "JM_Templates");

            migrationBuilder.RenameColumn(
                name: "ReporterIssueStatus",
                table: "JM_Templates",
                newName: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "JM_Templates",
                newName: "ReporterIssueStatus");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeIssueStatus",
                table: "JM_Templates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
