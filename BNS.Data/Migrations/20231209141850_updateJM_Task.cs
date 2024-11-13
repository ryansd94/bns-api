using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_Task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Tasks_JM_Accounts_AssignUserId",
                table: "JM_Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Tasks_JM_AccountCompanys_AssignUserId",
                table: "JM_Tasks",
                column: "AssignUserId",
                principalTable: "JM_AccountCompanys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Tasks_JM_AccountCompanys_AssignUserId",
                table: "JM_Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Tasks_JM_Accounts_AssignUserId",
                table: "JM_Tasks",
                column: "AssignUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
