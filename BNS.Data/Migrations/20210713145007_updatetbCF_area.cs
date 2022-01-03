using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updatetbCF_area : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UpdatedUser",
                table: "CF_Employee",
                type: "uniqueidentifier",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CF_Employee",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUser",
                table: "CF_Employee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "CF_Employee",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CF_Area_ShopIndex",
                table: "CF_Area",
                column: "ShopIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Area_CF_Shop_ShopIndex",
                table: "CF_Area",
                column: "ShopIndex",
                principalTable: "CF_Shop",
                principalColumn: "Index",
                onDelete: ReferentialAction.Cascade);


            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CF_Account",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUser",
                table: "CF_Account",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Area_CF_Shop_ShopIndex",
                table: "CF_Area");

            migrationBuilder.DropIndex(
                name: "IX_CF_Area_ShopIndex",
                table: "CF_Area");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CF_Employee");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "CF_Employee");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "CF_Employee");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedUser",
                table: "CF_Employee",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.DropColumn(
               name: "CreatedDate",
               table: "CF_Account");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "CF_Account");
        }
    }
}
