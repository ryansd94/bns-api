using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class DeleteJM_TeamMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "JM_TeamMembers");
             

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "JM_TeamMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_TeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_TeamMembers_JM_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JM_TeamMembers_JM_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "JM_Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_TeamMembers_JM_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "JM_Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
             
            migrationBuilder.CreateIndex(
                name: "IX_JM_TeamMembers_CompanyId",
                table: "JM_TeamMembers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TeamMembers_TeamId",
                table: "JM_TeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TeamMembers_UserId",
                table: "JM_TeamMembers",
                column: "UserId");

        }
    }
}
