using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyIndex",
                table: "JM_AccountCompanys");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyIndex",
                table: "JM_AccountCompanys",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
