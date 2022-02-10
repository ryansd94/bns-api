using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateTableJM_AccountCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "JM_AccountCompanys",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "JM_AccountCompanys");
        }
    }
}
