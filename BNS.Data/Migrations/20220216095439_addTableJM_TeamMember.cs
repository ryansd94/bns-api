using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class addTableJM_TeamMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JM_TeamMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_TeamMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_TeamMembers_JM_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TeamMembers_JM_Companys_CompanyIndex",
                        column: x => x.CompanyIndex,
                        principalTable: "JM_Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TeamMembers_JM_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "JM_Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_TeamMembers_CompanyIndex",
                table: "JM_TeamMembers",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TeamMembers_TeamId",
                table: "JM_TeamMembers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TeamMembers_UserId",
                table: "JM_TeamMembers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JM_TeamMembers");
        }
    }
}
