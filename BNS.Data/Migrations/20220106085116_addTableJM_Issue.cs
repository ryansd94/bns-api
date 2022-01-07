using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class addTableJM_Issue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Sprint_JM_Company_CompanyIndex",
                table: "JM_Sprint");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Sprint_JM_Projects_JM_ProjectId",
                table: "JM_Sprint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JM_Sprint",
                table: "JM_Sprint");

            migrationBuilder.RenameTable(
                name: "JM_Sprint",
                newName: "JM_Sprints");

            migrationBuilder.RenameIndex(
                name: "IX_JM_Sprint_JM_ProjectId",
                table: "JM_Sprints",
                newName: "IX_JM_Sprints_JM_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_Sprint_CompanyIndex",
                table: "JM_Sprints",
                newName: "IX_JM_Sprints_CompanyIndex");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JM_Sprints",
                table: "JM_Sprints",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "JM_Issues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueType = table.Column<int>(type: "int", nullable: false),
                    AssignUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReporterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SprintId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OriginalTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RemainingTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssueParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IssueStatus = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Issues_JM_Accounts_AssignUserId",
                        column: x => x.AssignUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_Issues_JM_Accounts_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JM_Issues_JM_Company_CompanyIndex",
                        column: x => x.CompanyIndex,
                        principalTable: "JM_Company",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_Issues_JM_Issues_IssueParentId",
                        column: x => x.IssueParentId,
                        principalTable: "JM_Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_Issues_JM_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "JM_Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JM_Issues_JM_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "JM_Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_Issues_AssignUserId",
                table: "JM_Issues",
                column: "AssignUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Issues_CompanyIndex",
                table: "JM_Issues",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Issues_IssueParentId",
                table: "JM_Issues",
                column: "IssueParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Issues_ProjectId",
                table: "JM_Issues",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Issues_ReporterId",
                table: "JM_Issues",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Issues_SprintId",
                table: "JM_Issues",
                column: "SprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Sprints_JM_Company_CompanyIndex",
                table: "JM_Sprints",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Sprints_JM_Projects_JM_ProjectId",
                table: "JM_Sprints",
                column: "JM_ProjectId",
                principalTable: "JM_Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Sprints_JM_Company_CompanyIndex",
                table: "JM_Sprints");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Sprints_JM_Projects_JM_ProjectId",
                table: "JM_Sprints");

            migrationBuilder.DropTable(
                name: "JM_Issues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JM_Sprints",
                table: "JM_Sprints");

            migrationBuilder.RenameTable(
                name: "JM_Sprints",
                newName: "JM_Sprint");

            migrationBuilder.RenameIndex(
                name: "IX_JM_Sprints_JM_ProjectId",
                table: "JM_Sprint",
                newName: "IX_JM_Sprint_JM_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_Sprints_CompanyIndex",
                table: "JM_Sprint",
                newName: "IX_JM_Sprint_CompanyIndex");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JM_Sprint",
                table: "JM_Sprint",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Sprint_JM_Company_CompanyIndex",
                table: "JM_Sprint",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Sprint_JM_Projects_JM_ProjectId",
                table: "JM_Sprint",
                column: "JM_ProjectId",
                principalTable: "JM_Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
