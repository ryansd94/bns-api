using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class UpdateJM_TaskCustomColumnValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_Accounts_CreatedUserId",
                table: "JM_TaskCustomColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_CustomColumns_CustomColumnId",
                table: "JM_TaskCustomColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_Tasks_TaskId",
                table: "JM_TaskCustomColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_TemplateDetails_TemplateDetailId",
                table: "JM_TaskCustomColumns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JM_TaskCustomColumns",
                table: "JM_TaskCustomColumns");

            migrationBuilder.RenameTable(
                name: "JM_TaskCustomColumns",
                newName: "JM_TaskCustomColumnValues");

            migrationBuilder.RenameIndex(
                name: "IX_JM_TaskCustomColumns_TemplateDetailId",
                table: "JM_TaskCustomColumnValues",
                newName: "IX_JM_TaskCustomColumnValues_TemplateDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_TaskCustomColumns_TaskId",
                table: "JM_TaskCustomColumnValues",
                newName: "IX_JM_TaskCustomColumnValues_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_TaskCustomColumns_CustomColumnId",
                table: "JM_TaskCustomColumnValues",
                newName: "IX_JM_TaskCustomColumnValues_CustomColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_TaskCustomColumns_CreatedUserId",
                table: "JM_TaskCustomColumnValues",
                newName: "IX_JM_TaskCustomColumnValues_CreatedUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "JM_TaskCustomColumnValues",
                type: "ntext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JM_TaskCustomColumnValues",
                table: "JM_TaskCustomColumnValues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumnValues_JM_Accounts_CreatedUserId",
                table: "JM_TaskCustomColumnValues",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumnValues_JM_CustomColumns_CustomColumnId",
                table: "JM_TaskCustomColumnValues",
                column: "CustomColumnId",
                principalTable: "JM_CustomColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumnValues_JM_Tasks_TaskId",
                table: "JM_TaskCustomColumnValues",
                column: "TaskId",
                principalTable: "JM_Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumnValues_JM_TemplateDetails_TemplateDetailId",
                table: "JM_TaskCustomColumnValues",
                column: "TemplateDetailId",
                principalTable: "JM_TemplateDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumnValues_JM_Accounts_CreatedUserId",
                table: "JM_TaskCustomColumnValues");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumnValues_JM_CustomColumns_CustomColumnId",
                table: "JM_TaskCustomColumnValues");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumnValues_JM_Tasks_TaskId",
                table: "JM_TaskCustomColumnValues");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumnValues_JM_TemplateDetails_TemplateDetailId",
                table: "JM_TaskCustomColumnValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JM_TaskCustomColumnValues",
                table: "JM_TaskCustomColumnValues");

            migrationBuilder.RenameTable(
                name: "JM_TaskCustomColumnValues",
                newName: "JM_TaskCustomColumns");

            migrationBuilder.RenameIndex(
                name: "IX_JM_TaskCustomColumnValues_TemplateDetailId",
                table: "JM_TaskCustomColumns",
                newName: "IX_JM_TaskCustomColumns_TemplateDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_TaskCustomColumnValues_TaskId",
                table: "JM_TaskCustomColumns",
                newName: "IX_JM_TaskCustomColumns_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_TaskCustomColumnValues_CustomColumnId",
                table: "JM_TaskCustomColumns",
                newName: "IX_JM_TaskCustomColumns_CustomColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_JM_TaskCustomColumnValues_CreatedUserId",
                table: "JM_TaskCustomColumns",
                newName: "IX_JM_TaskCustomColumns_CreatedUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "JM_TaskCustomColumns",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "ntext",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JM_TaskCustomColumns",
                table: "JM_TaskCustomColumns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_Accounts_CreatedUserId",
                table: "JM_TaskCustomColumns",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_CustomColumns_CustomColumnId",
                table: "JM_TaskCustomColumns",
                column: "CustomColumnId",
                principalTable: "JM_CustomColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_Tasks_TaskId",
                table: "JM_TaskCustomColumns",
                column: "TaskId",
                principalTable: "JM_Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_TemplateDetails_TemplateDetailId",
                table: "JM_TaskCustomColumns",
                column: "TemplateDetailId",
                principalTable: "JM_TemplateDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
