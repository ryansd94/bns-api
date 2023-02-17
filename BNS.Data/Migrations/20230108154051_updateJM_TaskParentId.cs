using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_TaskParentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Tasks_JM_Tasks_IssueParentId",
                table: "JM_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_JM_Tasks_IssueParentId",
                table: "JM_Tasks");

            migrationBuilder.DropColumn(
                name: "IssueParentId",
                table: "JM_Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Tasks_ParentId",
                table: "JM_Tasks",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Tasks_JM_Tasks_ParentId",
                table: "JM_Tasks",
                column: "ParentId",
                principalTable: "JM_Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Tasks_JM_Tasks_ParentId",
                table: "JM_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_JM_Tasks_ParentId",
                table: "JM_Tasks");

            migrationBuilder.AddColumn<Guid>(
                name: "IssueParentId",
                table: "JM_Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JM_Tasks_IssueParentId",
                table: "JM_Tasks",
                column: "IssueParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Tasks_JM_Tasks_IssueParentId",
                table: "JM_Tasks",
                column: "IssueParentId",
                principalTable: "JM_Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
