using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTBCF_position : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ShopIndex",
                table: "CF_Position",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "CF_Position",
                type: "bit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BranchIndex",
                table: "CF_Position",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CF_Position",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUser",
                table: "CF_Position",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "CF_Position",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CF_Position_ShopIndex",
                table: "CF_Position",
                column: "ShopIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Position_CF_Shop_ShopIndex",
                table: "CF_Position",
                column: "ShopIndex",
                principalTable: "CF_Shop",
                principalColumn: "Index",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Position_CF_Shop_ShopIndex",
                table: "CF_Position");

            migrationBuilder.DropIndex(
                name: "IX_CF_Position_ShopIndex",
                table: "CF_Position");

            migrationBuilder.DropColumn(
                name: "BranchIndex",
                table: "CF_Position");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CF_Position");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "CF_Position");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "CF_Position");

            migrationBuilder.AlterColumn<Guid>(
                name: "ShopIndex",
                table: "CF_Position",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "IsDelete",
                table: "CF_Position",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
