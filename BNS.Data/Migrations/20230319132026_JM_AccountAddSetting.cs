using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class JM_AccountAddSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Setting",
                table: "JM_Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JM_Tasks_AssignUserId",
                table: "JM_Tasks",
                column: "AssignUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Tasks_JM_Accounts_AssignUserId",
                table: "JM_Tasks",
                column: "AssignUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Tasks_JM_Accounts_AssignUserId",
                table: "JM_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_JM_Tasks_AssignUserId",
                table: "JM_Tasks");

            migrationBuilder.DropColumn(
                name: "Setting",
                table: "JM_Accounts");
        }
    }
}
