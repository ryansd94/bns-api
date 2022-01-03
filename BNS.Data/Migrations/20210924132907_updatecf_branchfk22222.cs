using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updatecf_branchfk22222 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CF_Area_BranchIndex",
                table: "CF_Area",
                column: "BranchIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Area_CF_Branch_BranchIndex",
                table: "CF_Area",
                column: "BranchIndex",
                principalTable: "CF_Branch",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Area_CF_Branch_BranchIndex",
                table: "CF_Area");

            migrationBuilder.DropIndex(
                name: "IX_CF_Area_BranchIndex",
                table: "CF_Area");
        }
    }
}
