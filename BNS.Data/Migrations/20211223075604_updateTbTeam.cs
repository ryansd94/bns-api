using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTbTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_JM_Teams_CF_Account_CreatedUser",
                table: "JM_Teams",
                column: "CreatedUser",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_CF_Account_UpdatedUser",
                table: "JM_Teams",
                column: "UpdatedUser",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_CF_Account_CreatedUser",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_CF_Account_UpdatedUser",
                table: "JM_Teams");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_CreatedUser",
                table: "JM_Teams");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_UpdatedUser",
                table: "JM_Teams");
        }
    }
}
