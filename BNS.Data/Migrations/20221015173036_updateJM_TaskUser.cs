using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_TaskUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Accounts_JM_Tasks_JM_TaskId",
                table: "JM_Accounts");

            migrationBuilder.DropIndex(
                name: "IX_JM_Accounts_JM_TaskId",
                table: "JM_Accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JM_Accounts_JM_TaskId",
                table: "JM_Accounts",
                column: "JM_TaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Accounts_JM_Tasks_JM_TaskId",
                table: "JM_Accounts",
                column: "JM_TaskId",
                principalTable: "JM_Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
