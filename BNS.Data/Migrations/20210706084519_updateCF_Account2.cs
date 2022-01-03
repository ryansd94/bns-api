using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateCF_Account2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CF_Shop",
                table: "CF_Shop");


            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CF_Shop",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<Guid>(
                name: "ShopIndex",
                table: "CF_Employee",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountIndex",
                table: "CF_Employee",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "CF_Area",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

         


            migrationBuilder.AddPrimaryKey(
                name: "PK_CF_Shop",
                table: "CF_Shop",
                column: "Index");

            migrationBuilder.CreateIndex(
                name: "IX_CF_Employee_ShopIndex",
                table: "CF_Employee",
                column: "ShopIndex");

            migrationBuilder.CreateIndex(
                name: "IX_CF_Account_ShopIndex",
                table: "CF_Account",
                column: "ShopIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Account_CF_Shop_ShopIndex",
                table: "CF_Account",
                column: "ShopIndex",
                principalTable: "CF_Shop",
                principalColumn: "Index",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Employee_CF_Shop_ShopIndex",
                table: "CF_Employee",
                column: "ShopIndex",
                principalTable: "CF_Shop",
                principalColumn: "Index",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Account_CF_Shop_ShopIndex",
                table: "CF_Account");

            migrationBuilder.DropForeignKey(
                name: "FK_CF_Employee_CF_Shop_ShopIndex",
                table: "CF_Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CF_Shop",
                table: "CF_Shop");

            migrationBuilder.DropIndex(
                name: "IX_CF_Employee_ShopIndex",
                table: "CF_Employee");

            migrationBuilder.DropIndex(
                name: "IX_CF_Account_ShopIndex",
                table: "CF_Account");

            migrationBuilder.DropColumn(
                name: "AccountIndex",
                table: "CF_Employee");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CF_Area");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "CF_Area");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CF_Account");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "CF_Account");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "CF_Shop",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ShopIndex",
                table: "CF_Employee",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "CF_Employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IsDelete",
                table: "CF_Area",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CF_Shop",
                table: "CF_Shop",
                columns: new[] { "Index", "Code" });
        }
    }
}
