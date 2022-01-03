using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updatetbCF_AreaFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateIndex(
                name: "IX_CF_Department_CreatedUser",
                table: "CF_Department",
                column: "CreatedUser");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Department_CF_Account_CreatedUser",
                table: "CF_Department",
                column: "CreatedUser",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Department_CF_Account_CreatedUser",
                table: "CF_Department");

            migrationBuilder.DropIndex(
                name: "IX_CF_Department_CreatedUser",
                table: "CF_Department");

            migrationBuilder.AddColumn<Guid>(
                name: "CF_AccountId",
                table: "CF_Position",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CF_AccountId",
                table: "CF_Department",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CF_Position_CF_AccountId",
                table: "CF_Position",
                column: "CF_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_CF_Department_CF_AccountId",
                table: "CF_Department",
                column: "CF_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Department_CF_Account_CF_AccountId",
                table: "CF_Department",
                column: "CF_AccountId",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Position_CF_Account_CF_AccountId",
                table: "CF_Position",
                column: "CF_AccountId",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
