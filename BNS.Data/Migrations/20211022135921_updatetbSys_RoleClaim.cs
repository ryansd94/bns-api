using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updatetbSys_RoleClaim : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sys_RoleClaim",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "ClaimType",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "ClaimValue",
                table: "Sys_RoleClaim");

            migrationBuilder.AddColumn<Guid>(
                name: "Index",
                table: "Sys_RoleClaim",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Sys_RoleClaim");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Sys_RoleClaim",
                type: "int",
                nullable: false);


            migrationBuilder.AddColumn<Guid>(
                name: "BranchIndex",
                table: "Sys_RoleClaim",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Sys_RoleClaim",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedUser",
                table: "Sys_RoleClaim",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DataId",
                table: "Sys_RoleClaim",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Sys_RoleClaim",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ShopIndex",
                table: "Sys_RoleClaim",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Sys_RoleClaim",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedUser",
                table: "Sys_RoleClaim",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sys_RoleClaim",
                table: "Sys_RoleClaim",
                column: "Index");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sys_RoleClaim",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "Index",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "BranchIndex",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "DataId",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "ShopIndex",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Sys_RoleClaim");

            migrationBuilder.DropColumn(
                name: "UpdatedUser",
                table: "Sys_RoleClaim");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Sys_RoleClaim",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Sys_RoleClaim",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ClaimType",
                table: "Sys_RoleClaim",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClaimValue",
                table: "Sys_RoleClaim",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sys_RoleClaim",
                table: "Sys_RoleClaim",
                column: "Id");
        }
    }
}
