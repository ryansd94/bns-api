using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTaskAddNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedDate",
                table: "JM_TaskUsers",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedDate",
                table: "JM_TaskUsers",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "JM_TaskUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "JM_Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskUsers_CreatedUserId",
                table: "JM_TaskUsers",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskUsers_JM_Accounts_CreatedUserId",
                table: "JM_TaskUsers",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskUsers_JM_Accounts_CreatedUserId",
                table: "JM_TaskUsers");

            migrationBuilder.DropIndex(
                name: "IX_JM_TaskUsers_CreatedUserId",
                table: "JM_TaskUsers");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "JM_Tasks");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedDate",
                table: "JM_TaskUsers",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "JM_TaskUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<Guid>(
                name: "CompanyId",
                table: "JM_TaskUsers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
