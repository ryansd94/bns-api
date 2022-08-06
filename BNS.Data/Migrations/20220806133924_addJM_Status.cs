using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class addJM_Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JM_Status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Status_JM_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "JM_Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JM_TemplateStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_TemplateStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_TemplateStatus_JM_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "JM_Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_TemplateStatus_JM_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "JM_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JM_TemplateStatus_JM_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "JM_Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_Status_CompanyId",
                table: "JM_Status",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateStatus_CompanyId",
                table: "JM_TemplateStatus",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateStatus_StatusId",
                table: "JM_TemplateStatus",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateStatus_TemplateId",
                table: "JM_TemplateStatus",
                column: "TemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JM_TemplateStatus");

            migrationBuilder.DropTable(
                name: "JM_Status");
        }
    }
}
