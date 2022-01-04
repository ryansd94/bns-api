using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTbJM_Template : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeIssueStatus",
                table: "JM_ProjectSprints");

            migrationBuilder.DropColumn(
                name: "IssueType",
                table: "JM_ProjectSprints");

            migrationBuilder.DropColumn(
                name: "ReporterIssueStatus",
                table: "JM_ProjectSprints");

            migrationBuilder.AddColumn<Guid>(
                name: "JM_TemplateId",
                table: "JM_Project",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "JM_Templates",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReporterIssueStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssigneeIssueStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Templates", x => x.Index);
                    table.ForeignKey(
                        name: "FK_JM_Templates_JM_Company_CompanyIndex",
                        column: x => x.CompanyIndex,
                        principalTable: "JM_Company",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_Project_JM_TemplateId",
                table: "JM_Project",
                column: "JM_TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Templates_CompanyIndex",
                table: "JM_Templates",
                column: "CompanyIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Project_JM_Templates_JM_TemplateId",
                table: "JM_Project",
                column: "JM_TemplateId",
                principalTable: "JM_Templates",
                principalColumn: "Index",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Project_JM_Templates_JM_TemplateId",
                table: "JM_Project");

            migrationBuilder.DropTable(
                name: "JM_Templates");

            migrationBuilder.DropIndex(
                name: "IX_JM_Project_JM_TemplateId",
                table: "JM_Project");

            migrationBuilder.DropColumn(
                name: "JM_TemplateId",
                table: "JM_Project");

            migrationBuilder.AddColumn<string>(
                name: "AssigneeIssueStatus",
                table: "JM_ProjectSprints",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssueType",
                table: "JM_ProjectSprints",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReporterIssueStatus",
                table: "JM_ProjectSprints",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
