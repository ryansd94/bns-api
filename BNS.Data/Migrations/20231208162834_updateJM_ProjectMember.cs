using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_ProjectMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectMembers_JM_Accounts_UserId",
                table: "JM_ProjectMembers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "JM_ProjectMembers",
                newName: "AccountCompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_ProjectMembers_UserId",
                table: "JM_ProjectMembers",
                newName: "IX_JM_ProjectMembers_AccountCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectMembers_JM_AccountCompanys_AccountCompanyId",
                table: "JM_ProjectMembers",
                column: "AccountCompanyId",
                principalTable: "JM_AccountCompanys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectMembers_JM_AccountCompanys_AccountCompanyId",
                table: "JM_ProjectMembers");

            migrationBuilder.RenameColumn(
                name: "AccountCompanyId",
                table: "JM_ProjectMembers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_ProjectMembers_AccountCompanyId",
                table: "JM_ProjectMembers",
                newName: "IX_JM_ProjectMembers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectMembers_JM_Accounts_UserId",
                table: "JM_ProjectMembers",
                column: "UserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
