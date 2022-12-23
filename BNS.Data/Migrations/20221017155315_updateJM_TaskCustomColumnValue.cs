using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_TaskCustomColumnValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TemplateDetailId",
                table: "JM_TaskCustomColumns",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskCustomColumns_TemplateDetailId",
                table: "JM_TaskCustomColumns",
                column: "TemplateDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_TemplateDetails_TemplateDetailId",
                table: "JM_TaskCustomColumns",
                column: "TemplateDetailId",
                principalTable: "JM_TemplateDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_TemplateDetails_TemplateDetailId",
                table: "JM_TaskCustomColumns");

            migrationBuilder.DropIndex(
                name: "IX_JM_TaskCustomColumns_TemplateDetailId",
                table: "JM_TaskCustomColumns");

            migrationBuilder.DropColumn(
                name: "TemplateDetailId",
                table: "JM_TaskCustomColumns");
        }
    }
}
