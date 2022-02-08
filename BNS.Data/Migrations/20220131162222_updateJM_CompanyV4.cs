using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_CompanyV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Accounts_JM_Company_CompanyId",
                table: "JM_Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Issues_JM_Company_CompanyIndex",
                table: "JM_Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectMembers_JM_Company_CompanyIndex",
                table: "JM_ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Projects_JM_Company_CompanyIndex",
                table: "JM_Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectSprints_JM_Company_CompanyIndex",
                table: "JM_ProjectSprints");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectTeams_JM_Company_CompanyIndex",
                table: "JM_ProjectTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Sprints_JM_Company_CompanyIndex",
                table: "JM_Sprints");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_JM_Company_CompanyIndex",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Templates_JM_Company_CompanyIndex",
                table: "JM_Templates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JM_Company",
                table: "JM_Company");

            migrationBuilder.RenameTable(
                name: "JM_Company",
                newName: "JM_Companys");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JM_Companys",
                table: "JM_Companys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Accounts_JM_Companys_CompanyId",
                table: "JM_Accounts",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Accounts_JM_Companys_CompanyId",
                table: "JM_Accounts");

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
                name: "FK_JM_Teams_JM_Companys_CompanyIndex",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Templates_JM_Companys_CompanyIndex",
                table: "JM_Templates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JM_Companys",
                table: "JM_Companys");

            migrationBuilder.RenameTable(
                name: "JM_Companys",
                newName: "JM_Company");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JM_Company",
                table: "JM_Company",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Accounts_JM_Company_CompanyId",
                table: "JM_Accounts",
                column: "CompanyId",
                principalTable: "JM_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Issues_JM_Company_CompanyIndex",
                table: "JM_Issues",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectMembers_JM_Company_CompanyIndex",
                table: "JM_ProjectMembers",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Projects_JM_Company_CompanyIndex",
                table: "JM_Projects",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectSprints_JM_Company_CompanyIndex",
                table: "JM_ProjectSprints",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectTeams_JM_Company_CompanyIndex",
                table: "JM_ProjectTeams",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Sprints_JM_Company_CompanyIndex",
                table: "JM_Sprints",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_JM_Company_CompanyIndex",
                table: "JM_Teams",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Templates_JM_Company_CompanyIndex",
                table: "JM_Templates",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
