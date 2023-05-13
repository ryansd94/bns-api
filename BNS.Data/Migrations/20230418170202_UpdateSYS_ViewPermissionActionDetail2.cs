using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class UpdateSYS_ViewPermissionActionDetail2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SYS_ViewPermissionActionDetails_SYS_ViewPermissionActions_SYS_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails");

            migrationBuilder.DropIndex(
                name: "IX_SYS_ViewPermissionActionDetails_SYS_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails");

            migrationBuilder.DropColumn(
                name: "SYS_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_SYS_ViewPermissionActionDetails_SYS_ViewPermissionActions_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails",
                column: "ViewPermissionActionId",
                principalTable: "SYS_ViewPermissionActions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SYS_ViewPermissionActionDetails_SYS_ViewPermissionActions_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "SYS_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SYS_ViewPermissionActionDetails_SYS_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails",
                column: "SYS_ViewPermissionActionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SYS_ViewPermissionActionDetails_SYS_ViewPermissionActions_SYS_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails",
                column: "SYS_ViewPermissionActionId",
                principalTable: "SYS_ViewPermissionActions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
