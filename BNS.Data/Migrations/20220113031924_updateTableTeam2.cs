using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTableTeam2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_ParentId",
                table: "JM_Teams",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_JM_Teams_ParentId",
                table: "JM_Teams",
                column: "ParentId",
                principalTable: "JM_Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_JM_Teams_ParentId",
                table: "JM_Teams");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_ParentId",
                table: "JM_Teams");
        }
    }
}
