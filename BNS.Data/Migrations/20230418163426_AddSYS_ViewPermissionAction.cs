using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class AddSYS_ViewPermissionAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SYS_ViewPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_ViewPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissions_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SYS_ViewPermissionActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewPermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_ViewPermissionActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissionActions_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissionActions_SYS_ViewPermissions_ViewPermissionId",
                        column: x => x.ViewPermissionId,
                        principalTable: "SYS_ViewPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SYS_ViewPermissionActionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionType = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<bool>(type: "bit", nullable: true),
                    ViewPermissionActionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SYS_ViewPermissionActionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_ViewPermissionActionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissionActionDetails_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissionActionDetails_SYS_ViewPermissionActions_SYS_ViewPermissionActionId",
                        column: x => x.SYS_ViewPermissionActionId,
                        principalTable: "SYS_ViewPermissionActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SYS_ViewPermissionActionDetails_CreatedUserId",
                table: "SYS_ViewPermissionActionDetails",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_ViewPermissionActionDetails_SYS_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails",
                column: "SYS_ViewPermissionActionId");

            migrationBuilder.CreateIndex(
                name: "Nidx_SYS_ViewPermissionActionDetail_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails",
                column: "ViewPermissionActionId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_ViewPermissionActions_CreatedUserId",
                table: "SYS_ViewPermissionActions",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "Nidx_SYS_ViewPermissionAction_ViewPermissionId",
                table: "SYS_ViewPermissionActions",
                column: "ViewPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_ViewPermissions_CreatedUserId",
                table: "SYS_ViewPermissions",
                column: "CreatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYS_ViewPermissionActionDetails");

            migrationBuilder.DropTable(
                name: "SYS_ViewPermissionActions");

            migrationBuilder.DropTable(
                name: "SYS_ViewPermissions");
        }
    }
}
