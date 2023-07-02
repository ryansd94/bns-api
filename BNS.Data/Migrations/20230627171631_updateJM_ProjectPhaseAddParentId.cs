using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_ProjectPhaseAddParentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ParentId",
                table: "JM_ProjectPhase",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectPhase_ParentId",
                table: "JM_ProjectPhase",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectPhase_JM_ProjectPhase_ParentId",
                table: "JM_ProjectPhase",
                column: "ParentId",
                principalTable: "JM_ProjectPhase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectPhase_JM_ProjectPhase_ParentId",
                table: "JM_ProjectPhase");

            migrationBuilder.DropIndex(
                name: "IX_JM_ProjectPhase_ParentId",
                table: "JM_ProjectPhase");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "JM_ProjectPhase");
        }
    }
}
