using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateJM_TaskCustomColumnValue2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_CustomColumns_CustomColumnId",
                table: "JM_TaskCustomColumns");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomColumnId",
                table: "JM_TaskCustomColumns",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_CustomColumns_CustomColumnId",
                table: "JM_TaskCustomColumns",
                column: "CustomColumnId",
                principalTable: "JM_CustomColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_CustomColumns_CustomColumnId",
                table: "JM_TaskCustomColumns");

            migrationBuilder.AlterColumn<Guid>(
                name: "CustomColumnId",
                table: "JM_TaskCustomColumns",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_CustomColumns_CustomColumnId",
                table: "JM_TaskCustomColumns",
                column: "CustomColumnId",
                principalTable: "JM_CustomColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
