using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class updateCreatedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "SYS_FilterConfigs",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "SYS_FilterConfigs",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_TemplateStatus",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_TemplateStatus",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_Templates",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_Templates",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_TemplateDetails",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_TemplateDetails",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_Teams",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_Teams",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_TaskUsers",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_TaskUsers",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_TaskTypies",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_TaskTypies",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_Tasks",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_Tasks",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_TaskCustomColumns",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_TaskCustomColumns",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_Status",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_Status",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_Sprints",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_Sprints",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_ProjectTeams",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_ProjectTeams",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_ProjectSprints",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_ProjectSprints",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_Projects",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_Projects",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_ProjectMembers",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_ProjectMembers",
                newName: "CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "JM_CustomColumns",
                newName: "UpdatedUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "JM_CustomColumns",
                newName: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_FilterConfigs_CreatedUserId",
                table: "SYS_FilterConfigs",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateStatus_CreatedUserId",
                table: "JM_TemplateStatus",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Templates_CreatedUserId",
                table: "JM_Templates",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateDetails_CreatedUserId",
                table: "JM_TemplateDetails",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_CreatedUserId",
                table: "JM_Teams",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskTypies_CreatedUserId",
                table: "JM_TaskTypies",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Tasks_CreatedUserId",
                table: "JM_Tasks",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskCustomColumns_CreatedUserId",
                table: "JM_TaskCustomColumns",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Status_CreatedUserId",
                table: "JM_Status",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Sprints_CreatedUserId",
                table: "JM_Sprints",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectTeams_CreatedUserId",
                table: "JM_ProjectTeams",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectSprints_CreatedUserId",
                table: "JM_ProjectSprints",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Projects_CreatedUserId",
                table: "JM_Projects",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectMembers_CreatedUserId",
                table: "JM_ProjectMembers",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_CustomColumns_CreatedUserId",
                table: "JM_CustomColumns",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JM_CustomColumns_JM_Accounts_CreatedUserId",
                table: "JM_CustomColumns",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectMembers_JM_Accounts_CreatedUserId",
                table: "JM_ProjectMembers",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Projects_JM_Accounts_CreatedUserId",
                table: "JM_Projects",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectSprints_JM_Accounts_CreatedUserId",
                table: "JM_ProjectSprints",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_ProjectTeams_JM_Accounts_CreatedUserId",
                table: "JM_ProjectTeams",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Sprints_JM_Accounts_CreatedUserId",
                table: "JM_Sprints",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Status_JM_Accounts_CreatedUserId",
                table: "JM_Status",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_Accounts_CreatedUserId",
                table: "JM_TaskCustomColumns",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Tasks_JM_Accounts_CreatedUserId",
                table: "JM_Tasks",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TaskTypies_JM_Accounts_CreatedUserId",
                table: "JM_TaskTypies",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Teams_JM_Accounts_CreatedUserId",
                table: "JM_Teams",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_TemplateDetails_JM_Accounts_CreatedUserId",
                table: "JM_TemplateDetails",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_JM_Templates_JM_Accounts_CreatedUserId",
                table: "JM_Templates",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);


            migrationBuilder.AddForeignKey(
                name: "FK_SYS_FilterConfigs_JM_Accounts_CreatedUserId",
                table: "SYS_FilterConfigs",
                column: "CreatedUserId",
                principalTable: "JM_Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JM_CustomColumns_JM_Accounts_CreatedUserId",
                table: "JM_CustomColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectMembers_JM_Accounts_CreatedUserId",
                table: "JM_ProjectMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Projects_JM_Accounts_CreatedUserId",
                table: "JM_Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectSprints_JM_Accounts_CreatedUserId",
                table: "JM_ProjectSprints");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_ProjectTeams_JM_Accounts_CreatedUserId",
                table: "JM_ProjectTeams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Sprints_JM_Accounts_CreatedUserId",
                table: "JM_Sprints");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Status_JM_Accounts_CreatedUserId",
                table: "JM_Status");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskCustomColumns_JM_Accounts_CreatedUserId",
                table: "JM_TaskCustomColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Tasks_JM_Accounts_CreatedUserId",
                table: "JM_Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TaskTypies_JM_Accounts_CreatedUserId",
                table: "JM_TaskTypies");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Teams_JM_Accounts_CreatedUserId",
                table: "JM_Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TemplateDetails_JM_Accounts_CreatedUserId",
                table: "JM_TemplateDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_Templates_JM_Accounts_CreatedUserId",
                table: "JM_Templates");

            migrationBuilder.DropForeignKey(
                name: "FK_JM_TemplateStatus_JM_Accounts_CreatedUserId",
                table: "JM_TemplateStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_SYS_FilterConfigs_JM_Accounts_CreatedUserId",
                table: "SYS_FilterConfigs");

            migrationBuilder.DropIndex(
                name: "IX_SYS_FilterConfigs_CreatedUserId",
                table: "SYS_FilterConfigs");

            migrationBuilder.DropIndex(
                name: "IX_JM_TemplateStatus_CreatedUserId",
                table: "JM_TemplateStatus");

            migrationBuilder.DropIndex(
                name: "IX_JM_Templates_CreatedUserId",
                table: "JM_Templates");

            migrationBuilder.DropIndex(
                name: "IX_JM_TemplateDetails_CreatedUserId",
                table: "JM_TemplateDetails");

            migrationBuilder.DropIndex(
                name: "IX_JM_Teams_CreatedUserId",
                table: "JM_Teams");

            migrationBuilder.DropIndex(
                name: "IX_JM_TaskTypies_CreatedUserId",
                table: "JM_TaskTypies");

            migrationBuilder.DropIndex(
                name: "IX_JM_Tasks_CreatedUserId",
                table: "JM_Tasks");

            migrationBuilder.DropIndex(
                name: "IX_JM_TaskCustomColumns_CreatedUserId",
                table: "JM_TaskCustomColumns");

            migrationBuilder.DropIndex(
                name: "IX_JM_Status_CreatedUserId",
                table: "JM_Status");

            migrationBuilder.DropIndex(
                name: "IX_JM_Sprints_CreatedUserId",
                table: "JM_Sprints");

            migrationBuilder.DropIndex(
                name: "IX_JM_ProjectTeams_CreatedUserId",
                table: "JM_ProjectTeams");

            migrationBuilder.DropIndex(
                name: "IX_JM_ProjectSprints_CreatedUserId",
                table: "JM_ProjectSprints");

            migrationBuilder.DropIndex(
                name: "IX_JM_Projects_CreatedUserId",
                table: "JM_Projects");

            migrationBuilder.DropIndex(
                name: "IX_JM_ProjectMembers_CreatedUserId",
                table: "JM_ProjectMembers");

            migrationBuilder.DropIndex(
                name: "IX_JM_CustomColumns_CreatedUserId",
                table: "JM_CustomColumns");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "SYS_FilterConfigs",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "SYS_FilterConfigs",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_TemplateStatus",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_TemplateStatus",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_Templates",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_Templates",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_TemplateDetails",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_TemplateDetails",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_Teams",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_Teams",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_TaskUsers",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_TaskUsers",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_TaskTypies",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_TaskTypies",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_Tasks",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_Tasks",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_TaskCustomColumns",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_TaskCustomColumns",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_Status",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_Status",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_Sprints",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_Sprints",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_ProjectTeams",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_ProjectTeams",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_ProjectSprints",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_ProjectSprints",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_Projects",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_Projects",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_ProjectMembers",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_ProjectMembers",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "UpdatedUserId",
                table: "JM_CustomColumns",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "JM_CustomColumns",
                newName: "CreatedUser");
        }
    }
}
