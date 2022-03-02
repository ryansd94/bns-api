using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTableJM_AccountCompany3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainAccount",
                table: "JM_Accounts");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainAccount",
                table: "JM_AccountCompanys",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMainAccount",
                table: "JM_AccountCompanys");

            migrationBuilder.AddColumn<bool>(
                name: "IsMainAccount",
                table: "JM_Accounts",
                type: "bit",
                nullable: true);
        }
    }
}
