using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTBCF_Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentIndex",
                table: "CF_Employee",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PositionIndex",
                table: "CF_Employee",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkingDate",
                table: "CF_Employee",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CF_Employee_DepartmentIndex",
                table: "CF_Employee",
                column: "DepartmentIndex");

            migrationBuilder.CreateIndex(
                name: "IX_CF_Employee_PositionIndex",
                table: "CF_Employee",
                column: "PositionIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Employee_CF_Department_DepartmentIndex",
                table: "CF_Employee",
                column: "DepartmentIndex",
                principalTable: "CF_Department",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CF_Employee_CF_Position_PositionIndex",
                table: "CF_Employee",
                column: "PositionIndex",
                principalTable: "CF_Position",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CF_Employee_CF_Department_DepartmentIndex",
                table: "CF_Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_CF_Employee_CF_Position_PositionIndex",
                table: "CF_Employee");

            migrationBuilder.DropIndex(
                name: "IX_CF_Employee_DepartmentIndex",
                table: "CF_Employee");

            migrationBuilder.DropIndex(
                name: "IX_CF_Employee_PositionIndex",
                table: "CF_Employee");

            migrationBuilder.DropColumn(
                name: "DepartmentIndex",
                table: "CF_Employee");

            migrationBuilder.DropColumn(
                name: "PositionIndex",
                table: "CF_Employee");

            migrationBuilder.DropColumn(
                name: "WorkingDate",
                table: "CF_Employee");
        }
    }
}
