using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateCompanyIndex2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Issues_JM_Companys_CompanyIndex",
                table: "JM_Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectMembers_JM_Companys_CompanyIndex",
                table: "JM_ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Projects_JM_Companys_CompanyIndex",
                table: "JM_Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectSprints_JM_Companys_CompanyIndex",
                table: "JM_ProjectSprints");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectTeams_JM_Companys_CompanyIndex",
                table: "JM_ProjectTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Sprints_JM_Companys_CompanyIndex",
                table: "JM_Sprints");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TeamMembers_JM_Companys_CompanyIndex",
                table: "JM_TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_JM_Companys_CompanyIndex",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Templates_JM_Companys_CompanyIndex",
                table: "JM_Templates");

            migrationBuilder.DropIndex(
                name: "IX_JM_Templates_CompanyIndex",
                table: "JM_Templates");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_CompanyIndex",
                table: "JM_Teams");

            migrationBuilder.DropIndex(
                name: "IX_JM_TeamMembers_CompanyIndex",
                table: "JM_TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_JM_Sprints_CompanyIndex",
                table: "JM_Sprints");

            migrationBuilder.DropIndex(
                name: "IX_JM_ProjectTeams_CompanyIndex",
                table: "JM_ProjectTeams");

            migrationBuilder.DropIndex(
                name: "IX_JM_ProjectSprints_CompanyIndex",
                table: "JM_ProjectSprints");

            migrationBuilder.DropIndex(
                name: "IX_JM_Projects_CompanyIndex",
                table: "JM_Projects");

            migrationBuilder.DropIndex(
                name: "IX_JM_ProjectMembers_CompanyIndex",
                table: "JM_ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_JM_Issues_CompanyIndex",
                table: "JM_Issues");

            migrationBuilder.DropColumn(
                name: "CompanyIndex",
                table: "JM_Templates");

            migrationBuilder.DropColumn(
                name: "CompanyIndex",
                table: "JM_Teams");

            migrationBuilder.DropColumn(
                name: "CompanyIndex",
                table: "JM_TeamMembers");

            migrationBuilder.DropColumn(
                name: "CompanyIndex",
                table: "JM_Sprints");

            migrationBuilder.DropColumn(
                name: "CompanyIndex",
                table: "JM_ProjectTeams");

            migrationBuilder.DropColumn(
                name: "CompanyIndex",
                table: "JM_ProjectSprints");

            migrationBuilder.DropColumn(
                name: "CompanyIndex",
                table: "JM_Projects");

            migrationBuilder.DropColumn(
                name: "CompanyIndex",
                table: "JM_ProjectMembers");

            migrationBuilder.DropColumn(
                name: "CompanyIndex",
                table: "JM_Issues");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Templates_CompanyId",
                table: "JM_Templates",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_CompanyId",
                table: "JM_Teams",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TeamMembers_CompanyId",
                table: "JM_TeamMembers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Sprints_CompanyId",
                table: "JM_Sprints",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectTeams_CompanyId",
                table: "JM_ProjectTeams",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectSprints_CompanyId",
                table: "JM_ProjectSprints",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Projects_CompanyId",
                table: "JM_Projects",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectMembers_CompanyId",
                table: "JM_ProjectMembers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Issues_CompanyId",
                table: "JM_Issues",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Issues_JM_Companys_CompanyId",
                table: "JM_Issues",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectMembers_JM_Companys_CompanyId",
                table: "JM_ProjectMembers",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Projects_JM_Companys_CompanyId",
                table: "JM_Projects",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectSprints_JM_Companys_CompanyId",
                table: "JM_ProjectSprints",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectTeams_JM_Companys_CompanyId",
                table: "JM_ProjectTeams",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Sprints_JM_Companys_CompanyId",
                table: "JM_Sprints",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TeamMembers_JM_Companys_CompanyId",
                table: "JM_TeamMembers",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_JM_Companys_CompanyId",
                table: "JM_Teams",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Templates_JM_Companys_CompanyId",
                table: "JM_Templates",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Issues_JM_Companys_CompanyId",
                table: "JM_Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectMembers_JM_Companys_CompanyId",
                table: "JM_ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Projects_JM_Companys_CompanyId",
                table: "JM_Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectSprints_JM_Companys_CompanyId",
                table: "JM_ProjectSprints");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectTeams_JM_Companys_CompanyId",
                table: "JM_ProjectTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Sprints_JM_Companys_CompanyId",
                table: "JM_Sprints");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TeamMembers_JM_Companys_CompanyId",
                table: "JM_TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_JM_Companys_CompanyId",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Templates_JM_Companys_CompanyId",
                table: "JM_Templates");

            migrationBuilder.DropIndex(
                name: "IX_JM_Templates_CompanyId",
                table: "JM_Templates");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_CompanyId",
                table: "JM_Teams");

            migrationBuilder.DropIndex(
                name: "IX_JM_TeamMembers_CompanyId",
                table: "JM_TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_JM_Sprints_CompanyId",
                table: "JM_Sprints");

            migrationBuilder.DropIndex(
                name: "IX_JM_ProjectTeams_CompanyId",
                table: "JM_ProjectTeams");

            migrationBuilder.DropIndex(
                name: "IX_JM_ProjectSprints_CompanyId",
                table: "JM_ProjectSprints");

            migrationBuilder.DropIndex(
                name: "IX_JM_Projects_CompanyId",
                table: "JM_Projects");

            migrationBuilder.DropIndex(
                name: "IX_JM_ProjectMembers_CompanyId",
                table: "JM_ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_JM_Issues_CompanyId",
                table: "JM_Issues");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIndex",
                table: "JM_Templates",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIndex",
                table: "JM_Teams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIndex",
                table: "JM_TeamMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIndex",
                table: "JM_Sprints",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIndex",
                table: "JM_ProjectTeams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIndex",
                table: "JM_ProjectSprints",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIndex",
                table: "JM_Projects",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIndex",
                table: "JM_ProjectMembers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIndex",
                table: "JM_Issues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JM_Templates_CompanyIndex",
                table: "JM_Templates",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_CompanyIndex",
                table: "JM_Teams",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TeamMembers_CompanyIndex",
                table: "JM_TeamMembers",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Sprints_CompanyIndex",
                table: "JM_Sprints",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectTeams_CompanyIndex",
                table: "JM_ProjectTeams",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectSprints_CompanyIndex",
                table: "JM_ProjectSprints",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Projects_CompanyIndex",
                table: "JM_Projects",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectMembers_CompanyIndex",
                table: "JM_ProjectMembers",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Issues_CompanyIndex",
                table: "JM_Issues",
                column: "CompanyIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Issues_JM_Companys_CompanyIndex",
                table: "JM_Issues",
                column: "CompanyIndex",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectMembers_JM_Companys_CompanyIndex",
                table: "JM_ProjectMembers",
                column: "CompanyIndex",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Projects_JM_Companys_CompanyIndex",
                table: "JM_Projects",
                column: "CompanyIndex",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectSprints_JM_Companys_CompanyIndex",
                table: "JM_ProjectSprints",
                column: "CompanyIndex",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectTeams_JM_Companys_CompanyIndex",
                table: "JM_ProjectTeams",
                column: "CompanyIndex",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Sprints_JM_Companys_CompanyIndex",
                table: "JM_Sprints",
                column: "CompanyIndex",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TeamMembers_JM_Companys_CompanyIndex",
                table: "JM_TeamMembers",
                column: "CompanyIndex",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_JM_Companys_CompanyIndex",
                table: "JM_Teams",
                column: "CompanyIndex",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Templates_JM_Companys_CompanyIndex",
                table: "JM_Templates",
                column: "CompanyIndex",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
