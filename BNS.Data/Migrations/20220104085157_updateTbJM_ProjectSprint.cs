using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTbJM_ProjectSprint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssigneeIssueStatus",
                table: "JM_ProjectSprints",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssueType",
                table: "JM_ProjectSprints",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReporterIssueStatus",
                table: "JM_ProjectSprints",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeIssueStatus",
                table: "JM_ProjectSprints");

            migrationBuilder.DropColumn(
                name: "IssueType",
                table: "JM_ProjectSprints");

            migrationBuilder.DropColumn(
                name: "ReporterIssueStatus",
                table: "JM_ProjectSprints");
        }
    }
}
