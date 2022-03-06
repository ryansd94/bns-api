using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTableJM_AccountCompany4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "JM_AccountCompanys");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "JM_Accounts",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "JM_Accounts");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "JM_AccountCompanys",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}
