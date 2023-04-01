using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class addTableJM_Priority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PriorityId",
                table: "JM_Tasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JM_Priorities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Priorities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Priorities_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_Tasks_PriorityId",
                table: "JM_Tasks",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Priorities_CreatedUserId",
                table: "JM_Priorities",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Tasks_JM_Priorities_PriorityId",
                table: "JM_Tasks",
                column: "PriorityId",
                principalTable: "JM_Priorities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Tasks_JM_Priorities_PriorityId",
                table: "JM_Tasks");

            migrationBuilder.DropTable(
                name: "JM_Priorities");

            migrationBuilder.DropIndex(
                name: "IX_JM_Tasks_PriorityId",
                table: "JM_Tasks");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "JM_Tasks");
        }
    }
}
