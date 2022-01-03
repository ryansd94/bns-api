using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class addTbJM_Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_CF_Shop_ShopIndex",
                table: "JM_Teams");
             

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_ShopIndex",
                table: "JM_Teams");

            migrationBuilder.DropColumn(
                name: "ShopIndex",
                table: "JM_Teams");

            migrationBuilder.RenameColumn(
                name: "BranchIndex",
                table: "JM_Teams",
                newName: "CompanyIndex");

            migrationBuilder.AddColumn<Guid>(
                name: "CF_ShopIndex",
                table: "JM_Teams",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JM_Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsMainAccount = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Accounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_CF_ShopIndex",
                table: "JM_Teams",
                column: "CF_ShopIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_CF_Shop_CF_ShopIndex",
                table: "JM_Teams",
                column: "CF_ShopIndex",
                principalTable: "CF_Shop",
                principalColumn: "Index",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_CF_Shop_CF_ShopIndex",
                table: "JM_Teams");

            migrationBuilder.DropTable(
                name: "JM_Accounts");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_CF_ShopIndex",
                table: "JM_Teams");

            migrationBuilder.DropColumn(
                name: "CF_ShopIndex",
                table: "JM_Teams");

            migrationBuilder.RenameColumn(
                name: "CompanyIndex",
                table: "JM_Teams",
                newName: "BranchIndex");

            migrationBuilder.AddColumn<Guid>(
                name: "ShopIndex",
                table: "JM_Teams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
             
            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_ShopIndex",
                table: "JM_Teams",
                column: "ShopIndex");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_CF_Shop_ShopIndex",
                table: "JM_Teams",
                column: "ShopIndex",
                principalTable: "CF_Shop",
                principalColumn: "Index",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
