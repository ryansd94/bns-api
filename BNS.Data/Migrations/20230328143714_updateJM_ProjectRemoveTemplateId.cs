using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_ProjectRemoveTemplateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Projects_JM_Templates_JM_TemplateId",
                table: "JM_Projects");

            migrationBuilder.DropColumn(
                name: "JM_TemplateId",
                table: "JM_Projects");

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "JM_Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                table: "JM_Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "JM_TemplateId",
                table: "JM_Projects",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JM_Projects_JM_TemplateId",
                table: "JM_Projects",
                column: "JM_TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Projects_JM_Templates_JM_TemplateId",
                table: "JM_Projects",
                column: "JM_TemplateId",
                principalTable: "JM_Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
