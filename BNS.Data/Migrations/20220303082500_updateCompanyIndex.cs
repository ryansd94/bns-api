using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateCompanyIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JM_Templates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JM_Teams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JM_TeamMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JM_Sprints",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JM_ProjectTeams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JM_ProjectSprints",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JM_Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JM_ProjectMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "JM_Issues",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JM_Templates");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JM_Teams");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JM_TeamMembers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JM_Sprints");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JM_ProjectTeams");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JM_ProjectSprints");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JM_Projects");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JM_ProjectMembers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "JM_Issues");
        }
    }
}
