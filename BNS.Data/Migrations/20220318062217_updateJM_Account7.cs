using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_Account7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_JM_Accounts_CreatedUser",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_JM_Accounts_UpdatedUser",
                table: "JM_Teams");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_CreatedUser",
                table: "JM_Teams");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_UpdatedUser",
                table: "JM_Teams");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_CreatedUser",
                table: "JM_Teams",
                column: "CreatedUser");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_UpdatedUser",
                table: "JM_Teams",
                column: "UpdatedUser");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_JM_Accounts_CreatedUser",
                table: "JM_Teams",
                column: "CreatedUser",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_JM_Accounts_UpdatedUser",
                table: "JM_Teams",
                column: "UpdatedUser",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
