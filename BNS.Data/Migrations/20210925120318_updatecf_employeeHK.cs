using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updatecf_employeeHK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CF_Employee_BranchIndex",
                table: "CF_Employee",
                column: "BranchIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Employee_CF_Branch_BranchIndex",
                table: "CF_Employee",
                column: "BranchIndex",
                principalTable: "CF_Branch",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Employee_CF_Branch_BranchIndex",
                table: "CF_Employee");

            migrationBuilder.DropIndex(
                name: "IX_CF_Employee_BranchIndex",
                table: "CF_Employee");
        }
    }
}
