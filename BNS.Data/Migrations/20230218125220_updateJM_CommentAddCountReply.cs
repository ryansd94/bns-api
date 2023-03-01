using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_CommentAddCountReply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "JM_CommentTasks");

            migrationBuilder.AddColumn<int>(
                name: "CountReply",
                table: "JM_Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JM_Comments_ParentId",
                table: "JM_Comments",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Comments_JM_Comments_ParentId",
                table: "JM_Comments",
                column: "ParentId",
                principalTable: "JM_Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Comments_JM_Comments_ParentId",
                table: "JM_Comments");

            migrationBuilder.DropIndex(
                name: "IX_JM_Comments_ParentId",
                table: "JM_Comments");

            migrationBuilder.DropColumn(
                name: "CountReply",
                table: "JM_Comments");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "JM_CommentTasks",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
