using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateCF_Account3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CF_Employee_AccountIndex",
                table: "CF_Employee",
                column: "AccountIndex",
                unique: true,
                filter: "[AccountIndex] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Employee_CF_Account_AccountIndex",
                table: "CF_Employee",
                column: "AccountIndex",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Employee_CF_Account_AccountIndex",
                table: "CF_Employee");

            migrationBuilder.DropIndex(
                name: "IX_CF_Employee_AccountIndex",
                table: "CF_Employee");
        }
    }
}
