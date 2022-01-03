using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateSYS_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Sys_Role",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUser",
                table: "Sys_Role",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Sys_Role",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Permission",
                table: "Sys_Role",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShopIndex",
                table: "Sys_Role",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Sys_Role",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUser",
                table: "Sys_Role",
                type: "uniqueidentifier",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Sys_Role");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "Sys_Role");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Sys_Role");

            migrationBuilder.DropColumn(
                name: "Permission",
                table: "Sys_Role");

            migrationBuilder.DropColumn(
                name: "ShopIndex",
                table: "Sys_Role");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Sys_Role");

            migrationBuilder.DropColumn(
                name: "UpdatedUser",
                table: "Sys_Role");
        }
    }
}
