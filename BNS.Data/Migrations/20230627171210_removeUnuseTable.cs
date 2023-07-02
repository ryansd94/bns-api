using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class removeUnuseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Tasks_JM_Sprints_SprintId",
                table: "JM_Tasks");

            migrationBuilder.DropTable(
                name: "JM_ProjectSprints");

            migrationBuilder.DropTable(
                name: "JM_Sprints");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Tasks_JM_ProjectPhase_SprintId",
                table: "JM_Tasks",
                column: "SprintId",
                principalTable: "JM_ProjectPhase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Tasks_JM_ProjectPhase_SprintId",
                table: "JM_Tasks");

            migrationBuilder.CreateTable(
                name: "JM_ProjectSprints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_ProjectSprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_ProjectSprints_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JM_Sprints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    JM_ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Sprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Sprints_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JM_Sprints_JM_Projects_JM_ProjectId",
                        column: x => x.JM_ProjectId,
                        principalTable: "JM_Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectSprints_CreatedUserId",
                table: "JM_ProjectSprints",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Sprints_CreatedUserId",
                table: "JM_Sprints",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Sprints_JM_ProjectId",
                table: "JM_Sprints",
                column: "JM_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Tasks_JM_Sprints_SprintId",
                table: "JM_Tasks",
                column: "SprintId",
                principalTable: "JM_Sprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
