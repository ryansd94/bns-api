using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateIndexToId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "JM_Templates");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "JM_Templates",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "JM_Teams",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "JM_ProjectTeams",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "JM_ProjectSprints",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "JM_ProjectMembers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Index",
                table: "JM_Project",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JM_Templates",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JM_Teams",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JM_ProjectTeams",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JM_ProjectSprints",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JM_ProjectMembers",
                newName: "Index");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JM_Project",
                newName: "Index");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "JM_Templates",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
