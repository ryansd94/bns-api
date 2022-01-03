using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updatecf_areaFK3333 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Area_CF_Account_UpdatedUser",
                table: "CF_Area");

            migrationBuilder.DropForeignKey(
                name: "FK_CF_Department_CF_Account_CreatedUser",
                table: "CF_Department");

            migrationBuilder.DropIndex(
                name: "IX_CF_Department_CreatedUser",
                table: "CF_Department");

            migrationBuilder.DropIndex(
                name: "IX_CF_Area_UpdatedUser",
                table: "CF_Area");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CF_Department_CreatedUser",
                table: "CF_Department",
                column: "CreatedUser");

            migrationBuilder.CreateIndex(
                name: "IX_CF_Area_UpdatedUser",
                table: "CF_Area",
                column: "UpdatedUser");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Area_CF_Account_UpdatedUser",
                table: "CF_Area",
                column: "UpdatedUser",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Department_CF_Account_CreatedUser",
                table: "CF_Department",
                column: "CreatedUser",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
