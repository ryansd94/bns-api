using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class AddJM_Comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JM_Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "ntext", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Comments_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_CommentTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_CommentTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_CommentTasks_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_CommentTasks_JM_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "JM_Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_CommentTasks_JM_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "JM_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_Comments_CreatedUserId",
                table: "JM_Comments",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_CommentTasks_CommentId",
                table: "JM_CommentTasks",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_CommentTasks_CreatedUserId",
                table: "JM_CommentTasks",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_CommentTasks_TaskId",
                table: "JM_CommentTasks",
                column: "TaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JM_CommentTasks");

            migrationBuilder.DropTable(
                name: "JM_Comments");
        }
    }
}
