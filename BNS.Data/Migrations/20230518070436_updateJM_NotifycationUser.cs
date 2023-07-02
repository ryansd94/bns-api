using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_NotifycationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_NotifycationUsers_JM_Notifycations_NotifycationId",
                table: "JM_NotifycationUsers");

            migrationBuilder.DropIndex(
                name: "IX_JM_NotifycationUsers_NotifycationId",
                table: "JM_NotifycationUsers");

            migrationBuilder.DropColumn(
                name: "NotifycationId",
                table: "JM_NotifycationUsers");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "JM_NotifycationUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ObjectId",
                table: "JM_NotifycationUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "JM_NotifycationUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "JM_NotifycationUsers");

            migrationBuilder.DropColumn(
                name: "ObjectId",
                table: "JM_NotifycationUsers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "JM_NotifycationUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "NotifycationId",
                table: "JM_NotifycationUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JM_NotifycationUsers_NotifycationId",
                table: "JM_NotifycationUsers",
                column: "NotifycationId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_NotifycationUsers_JM_Notifycations_NotifycationId",
                table: "JM_NotifycationUsers",
                column: "NotifycationId",
                principalTable: "JM_Notifycations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
