using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_Task : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssueStatus",
                table: "JM_Issues");

            migrationBuilder.DropColumn(
                name: "IssueType",
                table: "JM_Issues");

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "JM_Issues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TaskParentId",
                table: "JM_Issues",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TaskTypeId",
                table: "JM_Issues",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JM_Issues_StatusId",
                table: "JM_Issues",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Issues_JM_Status_StatusId",
                table: "JM_Issues",
                column: "StatusId",
                principalTable: "JM_Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Issues_JM_Status_StatusId",
                table: "JM_Issues");

            migrationBuilder.DropIndex(
                name: "IX_JM_Issues_StatusId",
                table: "JM_Issues");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "JM_Issues");

            migrationBuilder.DropColumn(
                name: "TaskParentId",
                table: "JM_Issues");

            migrationBuilder.DropColumn(
                name: "TaskTypeId",
                table: "JM_Issues");

            migrationBuilder.AddColumn<int>(
                name: "IssueStatus",
                table: "JM_Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IssueType",
                table: "JM_Issues",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
