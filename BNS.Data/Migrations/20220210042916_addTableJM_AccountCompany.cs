using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class addTableJM_AccountCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Accounts_JM_Companys_CompanyId",
                table: "JM_Accounts");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "JM_Accounts",
                newName: "JM_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_Accounts_CompanyId",
                table: "JM_Accounts",
                newName: "IX_JM_Accounts_JM_CompanyId");

            migrationBuilder.CreateTable(
                name: "JM_AccountCompanys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_AccountCompanys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_AccountCompanys_JM_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JM_AccountCompanys_JM_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "JM_Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_AccountCompanys_CompanyId",
                table: "JM_AccountCompanys",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_AccountCompanys_UserId",
                table: "JM_AccountCompanys",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Accounts_JM_Companys_JM_CompanyId",
                table: "JM_Accounts",
                column: "JM_CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Accounts_JM_Companys_JM_CompanyId",
                table: "JM_Accounts");

            migrationBuilder.DropTable(
                name: "JM_AccountCompanys");

            migrationBuilder.RenameColumn(
                name: "JM_CompanyId",
                table: "JM_Accounts",
                newName: "CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_Accounts_JM_CompanyId",
                table: "JM_Accounts",
                newName: "IX_JM_Accounts_CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Accounts_JM_Companys_CompanyId",
                table: "JM_Accounts",
                column: "CompanyId",
                principalTable: "JM_Companys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
