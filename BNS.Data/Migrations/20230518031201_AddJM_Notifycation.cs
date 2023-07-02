using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class AddJM_Notifycation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JM_Notifycations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Notifycations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Notifycations_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_NotifycationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NotifycationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserReceivedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_NotifycationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_NotifycationUsers_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_NotifycationUsers_JM_Accounts_UserReceivedId",
                        column: x => x.UserReceivedId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_NotifycationUsers_JM_Notifycations_NotifycationId",
                        column: x => x.NotifycationId,
                        principalTable: "JM_Notifycations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_Notifycations_CreatedUserId",
                table: "JM_Notifycations",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_NotifycationUsers_CreatedUserId",
                table: "JM_NotifycationUsers",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_NotifycationUsers_NotifycationId",
                table: "JM_NotifycationUsers",
                column: "NotifycationId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_NotifycationUsers_UserReceivedId",
                table: "JM_NotifycationUsers",
                column: "UserReceivedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JM_NotifycationUsers");

            migrationBuilder.DropTable(
                name: "JM_Notifycations");
        }
    }
}
