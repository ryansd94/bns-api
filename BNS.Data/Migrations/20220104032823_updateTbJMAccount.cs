using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTbJMAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_CF_Account_CreatedUser",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_CF_Account_UpdatedUser",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_CF_Shop_CF_ShopIndex",
                table: "JM_Teams");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_CF_ShopIndex",
                table: "JM_Teams");

            migrationBuilder.DropColumn(
                name: "CF_ShopIndex",
                table: "JM_Teams");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "JM_Teams",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "JM_Company",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Long = table.Column<double>(type: "float", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Company", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "JM_Project",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avartar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Project", x => x.Index);
                    table.ForeignKey(
                        name: "FK_JM_Project_JM_Company_CompanyIndex",
                        column: x => x.CompanyIndex,
                        principalTable: "JM_Company",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JM_ProjectTeams",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_ProjectTeams", x => x.Index);
                    table.ForeignKey(
                        name: "FK_JM_ProjectTeams_JM_Company_CompanyIndex",
                        column: x => x.CompanyIndex,
                        principalTable: "JM_Company",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_ProjectTeams_JM_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "JM_Project",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JM_ProjectTeams_JM_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "JM_Teams",
                        principalColumn: "Index",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_CompanyIndex",
                table: "JM_Teams",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Project_CompanyIndex",
                table: "JM_Project",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectTeams_CompanyIndex",
                table: "JM_ProjectTeams",
                column: "CompanyIndex");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectTeams_ProjectId",
                table: "JM_ProjectTeams",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectTeams_TeamId",
                table: "JM_ProjectTeams",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_JM_Accounts_CreatedUser",
                table: "JM_Teams",
                column: "CreatedUser",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_JM_Accounts_UpdatedUser",
                table: "JM_Teams",
                column: "UpdatedUser",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_JM_Company_CompanyIndex",
                table: "JM_Teams",
                column: "CompanyIndex",
                principalTable: "JM_Company",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_JM_Accounts_CreatedUser",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_JM_Accounts_UpdatedUser",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_JM_Company_CompanyIndex",
                table: "JM_Teams");

            migrationBuilder.DropTable(
                name: "JM_ProjectTeams");

            migrationBuilder.DropTable(
                name: "JM_Project");

            migrationBuilder.DropTable(
                name: "JM_Company");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_CompanyIndex",
                table: "JM_Teams");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDelete",
                table: "JM_Teams",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<Guid>(
                name: "CF_ShopIndex",
                table: "JM_Teams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_CF_ShopIndex",
                table: "JM_Teams",
                column: "CF_ShopIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_CF_Account_CreatedUser",
                table: "JM_Teams",
                column: "CreatedUser",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_CF_Account_UpdatedUser",
                table: "JM_Teams",
                column: "UpdatedUser",
                principalTable: "CF_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_CF_Shop_CF_ShopIndex",
                table: "JM_Teams",
                column: "CF_ShopIndex",
                principalTable: "CF_Shop",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
