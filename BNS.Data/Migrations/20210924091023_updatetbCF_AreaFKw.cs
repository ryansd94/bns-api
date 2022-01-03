using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updatetbCF_AreaFKw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<Guid>(
                name: "CF_AccountId",
                table: "CF_Area",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CF_Area_CF_AccountId",
                table: "CF_Area",
                column: "CF_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Area_CF_Account_CF_AccountId",
                table: "CF_Area",
                column: "CF_AccountId",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Area_CF_Account_CF_AccountId",
                table: "CF_Area");

            migrationBuilder.DropIndex(
                name: "IX_CF_Area_CF_AccountId",
                table: "CF_Area");

            migrationBuilder.DropColumn(
                name: "CF_AccountId",
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
