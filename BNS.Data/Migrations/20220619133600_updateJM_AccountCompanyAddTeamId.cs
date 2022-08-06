using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_AccountCompanyAddTeamId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "JM_AccountCompanys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JM_AccountCompanys_TeamId",
                table: "JM_AccountCompanys",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_AccountCompanys_JM_Teams_TeamId",
                table: "JM_AccountCompanys",
                column: "TeamId",
                principalTable: "JM_Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_AccountCompanys_JM_Teams_TeamId",
                table: "JM_AccountCompanys");

            migrationBuilder.DropIndex(
                name: "IX_JM_AccountCompanys_TeamId",
                table: "JM_AccountCompanys");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "JM_AccountCompanys");
        }
    }
}
