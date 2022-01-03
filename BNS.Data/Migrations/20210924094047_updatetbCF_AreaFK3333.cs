using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updatetbCF_AreaFK3333 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Area_CF_Account_UpdatedUser",
                table: "CF_Area");

            migrationBuilder.DropIndex(
                name: "IX_CF_Area_UpdatedUser",
                table: "CF_Area");

            migrationBuilder.CreateIndex(
                name: "IX_CF_Area_CreatedUser",
                table: "CF_Area",
                column: "CreatedUser");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Area_CF_Account_CreatedUser",
                table: "CF_Area",
                column: "CreatedUser",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
