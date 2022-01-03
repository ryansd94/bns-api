using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateCF_Branch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchIndex",
                table: "CF_Branch");

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "CF_Branch",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CF_Branch_ShopIndex",
                table: "CF_Branch",
                column: "ShopIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Branch_CF_Shop_ShopIndex",
                table: "CF_Branch",
                column: "ShopIndex",
                principalTable: "CF_Shop",
                principalColumn: "Index",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Branch_CF_Shop_ShopIndex",
                table: "CF_Branch");

            migrationBuilder.DropIndex(
                name: "IX_CF_Branch_ShopIndex",
                table: "CF_Branch");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CF_Branch");

            migrationBuilder.AddColumn<Guid>(
                name: "BranchIndex",
                table: "CF_Branch",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
