using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class addTb_JM_ProjectMember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JM_ProjectMembers",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_JM_ProjectMembers", x => x.Index);
                    table.ForeignKey(
                        name: "FK_JM_ProjectMembers_JM_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JM_ProjectMembers_JM_Company_CompanyIndex",
                        column: x => x.CompanyIndex,
                        principalTable: "JM_Company",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_ProjectMembers_JM_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "JM_Project",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectMembers_CompanyIndex",
                table: "JM_ProjectMembers",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectMembers_ProjectId",
                table: "JM_ProjectMembers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectMembers_UserId",
                table: "JM_ProjectMembers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JM_ProjectMembers");
        }
    }
}
