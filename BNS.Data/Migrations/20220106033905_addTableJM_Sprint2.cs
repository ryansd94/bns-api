using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class addTableJM_Sprint2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Project_JM_Company_CompanyIndex",
                table: "JM_Project");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Project_JM_Templates_JM_TemplateId",
                table: "JM_Project");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectMembers_JM_Project_ProjectId",
                table: "JM_ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectTeams_JM_Project_ProjectId",
                table: "JM_ProjectTeams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JM_Project",
                table: "JM_Project");

            migrationBuilder.RenameTable(
                name: "JM_Project",
                newName: "JM_Projects");

            migrationBuilder.RenameIndex(
                name: "IX_JM_Project_JM_TemplateId",
                table: "JM_Projects",
                newName: "IX_JM_Projects_JM_TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_Project_CompanyIndex",
                table: "JM_Projects",
                newName: "IX_JM_Projects_CompanyIndex");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JM_Projects",
                table: "JM_Projects",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "JM_Sprint",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    JM_ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Sprint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Sprint_JM_Company_CompanyIndex",
                        column: x => x.CompanyIndex,
                        principalTable: "JM_Company",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_Sprint_JM_Projects_JM_ProjectId",
                        column: x => x.JM_ProjectId,
                        principalTable: "JM_Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_Sprint_CompanyIndex",
                table: "JM_Sprint",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Sprint_JM_ProjectId",
                table: "JM_Sprint",
                column: "JM_ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectMembers_JM_Projects_ProjectId",
                table: "JM_ProjectMembers",
                column: "ProjectId",
                principalTable: "JM_Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Projects_JM_Company_CompanyIndex",
                table: "JM_Projects",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Projects_JM_Templates_JM_TemplateId",
                table: "JM_Projects",
                column: "JM_TemplateId",
                principalTable: "JM_Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectTeams_JM_Projects_ProjectId",
                table: "JM_ProjectTeams",
                column: "ProjectId",
                principalTable: "JM_Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectMembers_JM_Projects_ProjectId",
                table: "JM_ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Projects_JM_Company_CompanyIndex",
                table: "JM_Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Projects_JM_Templates_JM_TemplateId",
                table: "JM_Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectTeams_JM_Projects_ProjectId",
                table: "JM_ProjectTeams");

            migrationBuilder.DropTable(
                name: "JM_Sprint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JM_Projects",
                table: "JM_Projects");

            migrationBuilder.RenameTable(
                name: "JM_Projects",
                newName: "JM_Project");

            migrationBuilder.RenameIndex(
                name: "IX_JM_Projects_JM_TemplateId",
                table: "JM_Project",
                newName: "IX_JM_Project_JM_TemplateId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_Projects_CompanyIndex",
                table: "JM_Project",
                newName: "IX_JM_Project_CompanyIndex");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JM_Project",
                table: "JM_Project",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Project_JM_Company_CompanyIndex",
                table: "JM_Project",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Project_JM_Templates_JM_TemplateId",
                table: "JM_Project",
                column: "JM_TemplateId",
                principalTable: "JM_Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectMembers_JM_Project_ProjectId",
                table: "JM_ProjectMembers",
                column: "ProjectId",
                principalTable: "JM_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectTeams_JM_Project_ProjectId",
                table: "JM_ProjectTeams",
                column: "ProjectId",
                principalTable: "JM_Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
