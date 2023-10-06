using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JM_Companys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Long = table.Column<double>(type: "float", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    Scale = table.Column<int>(type: "int", nullable: false),
                    ManagementType = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Companys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_RoleClaim",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_RoleClaim", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "Sys_RoleGroup",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Number = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_RoleGroup", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Permission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserClaim", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserLogin",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserLogin", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserRole", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Sys_UserToken",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_UserToken", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "SYS_VersionConfig",
                columns: table => new
                {
                    VersionType = table.Column<int>(type: "int", nullable: false),
                    Menu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_VersionConfig", x => x.VersionType);
                });

            migrationBuilder.CreateTable(
                name: "JM_Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    GoogleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JM_CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JM_TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Setting = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_JM_Accounts_JM_Companys_JM_CompanyId",
                        column: x => x.JM_CompanyId,
                        principalTable: "JM_Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JM_Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "ntext", nullable: true),
                    CountReply = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Comments_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_Comments_JM_Comments_ParentId",
                        column: x => x.ParentId,
                        principalTable: "JM_Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JM_CustomColumns",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ControlType = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_CustomColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_CustomColumns_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Files_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_Notifycations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
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
                    ObjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserReceivedId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "JM_Priorities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Priorities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Priorities_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avartar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Projects_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_Status",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsStatusStart = table.Column<bool>(type: "bit", nullable: false),
                    IsStatusEnd = table.Column<bool>(type: "bit", nullable: false),
                    IsAutomaticAdd = table.Column<bool>(type: "bit", nullable: false),
                    IsApplyAll = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Status", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Status_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Tags_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Teams_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_Teams_JM_Teams_ParentId",
                        column: x => x.ParentId,
                        principalTable: "JM_Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JM_Templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssueType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Templates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Templates_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SYS_FilterConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilterData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    View = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_FilterConfigs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_FilterConfigs_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SYS_FilterConfigs_JM_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SYS_ViewPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_ViewPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissions_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_AttachedFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_AttachedFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_AttachedFiles_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_AttachedFiles_JM_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "JM_Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_ProjectMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCreated = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_ProjectMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_ProjectMembers_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_ProjectMembers_JM_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_ProjectMembers_JM_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "JM_Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_ProjectPhase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_ProjectPhase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_ProjectPhase_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_ProjectPhase_JM_ProjectPhase_ParentId",
                        column: x => x.ParentId,
                        principalTable: "JM_ProjectPhase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_ProjectPhase_JM_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "JM_Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_AccountCompanys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsMainAccount = table.Column<bool>(type: "bit", nullable: false),
                    EmailTimestamp = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_AccountCompanys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_AccountCompanys_JM_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_AccountCompanys_JM_Companys_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "JM_Companys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_AccountCompanys_JM_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "JM_Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JM_ProjectTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_ProjectTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_ProjectTeams_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_ProjectTeams_JM_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "JM_Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_ProjectTeams_JM_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "JM_Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_TaskTypies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorFilter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: true),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_TaskTypies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_TaskTypies_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TaskTypies_JM_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "JM_Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JM_TemplateDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ColumnPosition = table.Column<int>(type: "int", nullable: false),
                    CustomColumnId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColumnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColumnTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_TemplateDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_TemplateDetails_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TemplateDetails_JM_CustomColumns_CustomColumnId",
                        column: x => x.CustomColumnId,
                        principalTable: "JM_CustomColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_TemplateDetails_JM_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "JM_Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_TemplateStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_TemplateStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_TemplateStatus_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TemplateStatus_JM_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "JM_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TemplateStatus_JM_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "JM_Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SYS_ViewPermissionActions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Controller = table.Column<int>(type: "int", nullable: false),
                    ViewPermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_ViewPermissionActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissionActions_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissionActions_SYS_ViewPermissions_ViewPermissionId",
                        column: x => x.ViewPermissionId,
                        principalTable: "SYS_ViewPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SYS_ViewPermissionObject",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ObjectType = table.Column<int>(type: "int", nullable: false),
                    ViewPermissionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_ViewPermissionObject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissionObject_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissionObject_SYS_ViewPermissions_ViewPermissionId",
                        column: x => x.ViewPermissionId,
                        principalTable: "SYS_ViewPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaskTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssignUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReporterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SprintId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RemainingTime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PriorityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Estimatedhour = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_Tasks_JM_Accounts_AssignUserId",
                        column: x => x.AssignUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_Tasks_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_Tasks_JM_Priorities_PriorityId",
                        column: x => x.PriorityId,
                        principalTable: "JM_Priorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_Tasks_JM_ProjectPhase_SprintId",
                        column: x => x.SprintId,
                        principalTable: "JM_ProjectPhase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_Tasks_JM_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "JM_Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_Tasks_JM_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "JM_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_Tasks_JM_Tasks_ParentId",
                        column: x => x.ParentId,
                        principalTable: "JM_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JM_Tasks_JM_TaskTypies_TaskTypeId",
                        column: x => x.TaskTypeId,
                        principalTable: "JM_TaskTypies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "SYS_ViewPermissionActionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<bool>(type: "bit", nullable: true),
                    ViewPermissionActionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_ViewPermissionActionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissionActionDetails_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SYS_ViewPermissionActionDetails_SYS_ViewPermissionActions_ViewPermissionActionId",
                        column: x => x.ViewPermissionActionId,
                        principalTable: "SYS_ViewPermissionActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_CommentTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_CommentTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_CommentTasks_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_CommentTasks_JM_Comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "JM_Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_CommentTasks_JM_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "JM_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_TaskCustomColumnValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomColumnId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TemplateDetailId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "ntext", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_TaskCustomColumnValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_TaskCustomColumnValues_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TaskCustomColumnValues_JM_CustomColumns_CustomColumnId",
                        column: x => x.CustomColumnId,
                        principalTable: "JM_CustomColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TaskCustomColumnValues_JM_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "JM_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TaskCustomColumnValues_JM_TemplateDetails_TemplateDetailId",
                        column: x => x.TemplateDetailId,
                        principalTable: "JM_TemplateDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_TaskTag",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_TaskTag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_TaskTag_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TaskTag_JM_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "JM_Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TaskTag_JM_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "JM_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "JM_TaskUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JM_TaskUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JM_TaskUsers_JM_Accounts_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TaskUsers_JM_Accounts_UserId",
                        column: x => x.UserId,
                        principalTable: "JM_Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_JM_TaskUsers_JM_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "JM_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JM_AccountCompanys_CompanyId",
                table: "JM_AccountCompanys",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_AccountCompanys_TeamId",
                table: "JM_AccountCompanys",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_AccountCompanys_UserId",
                table: "JM_AccountCompanys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Accounts_JM_CompanyId",
                table: "JM_Accounts",
                column: "JM_CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_AttachedFiles_CreatedUserId",
                table: "JM_AttachedFiles",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_AttachedFiles_FileId",
                table: "JM_AttachedFiles",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Comments_CreatedUserId",
                table: "JM_Comments",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Comments_ParentId",
                table: "JM_Comments",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_CommentTasks_CommentId",
                table: "JM_CommentTasks",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_CommentTasks_CreatedUserId",
                table: "JM_CommentTasks",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_CommentTasks_TaskId",
                table: "JM_CommentTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_CustomColumns_CreatedUserId",
                table: "JM_CustomColumns",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Files_CreatedUserId",
                table: "JM_Files",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Notifycations_CreatedUserId",
                table: "JM_Notifycations",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_NotifycationUsers_CreatedUserId",
                table: "JM_NotifycationUsers",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_NotifycationUsers_UserReceivedId",
                table: "JM_NotifycationUsers",
                column: "UserReceivedId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Priorities_CreatedUserId",
                table: "JM_Priorities",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectMembers_CreatedUserId",
                table: "JM_ProjectMembers",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectMembers_ProjectId",
                table: "JM_ProjectMembers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectMembers_UserId",
                table: "JM_ProjectMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectPhase_CreatedUserId",
                table: "JM_ProjectPhase",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectPhase_ParentId",
                table: "JM_ProjectPhase",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectPhase_ProjectId",
                table: "JM_ProjectPhase",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Projects_CreatedUserId",
                table: "JM_Projects",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectTeams_CreatedUserId",
                table: "JM_ProjectTeams",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectTeams_ProjectId",
                table: "JM_ProjectTeams",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_ProjectTeams_TeamId",
                table: "JM_ProjectTeams",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Status_CreatedUserId",
                table: "JM_Status",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Tags_CreatedUserId",
                table: "JM_Tags",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskCustomColumnValues_CreatedUserId",
                table: "JM_TaskCustomColumnValues",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskCustomColumnValues_CustomColumnId",
                table: "JM_TaskCustomColumnValues",
                column: "CustomColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskCustomColumnValues_TaskId",
                table: "JM_TaskCustomColumnValues",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskCustomColumnValues_TemplateDetailId",
                table: "JM_TaskCustomColumnValues",
                column: "TemplateDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Tasks_CreatedUserId",
                table: "JM_Tasks",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "Nidx_JM_Task_AssignUserId",
                table: "JM_Tasks",
                column: "AssignUserId");

            migrationBuilder.CreateIndex(
                name: "Nidx_JM_Task_ParentId",
                table: "JM_Tasks",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "Nidx_JM_Task_PriorityId",
                table: "JM_Tasks",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "Nidx_JM_Task_ProjectId",
                table: "JM_Tasks",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "Nidx_JM_Task_SprintId",
                table: "JM_Tasks",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "Nidx_JM_Task_StatusId",
                table: "JM_Tasks",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "Nidx_JM_Task_TaskTypeId",
                table: "JM_Tasks",
                column: "TaskTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskTag_CreatedUserId",
                table: "JM_TaskTag",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskTag_TagId",
                table: "JM_TaskTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskTag_TaskId",
                table: "JM_TaskTag",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskTypies_CreatedUserId",
                table: "JM_TaskTypies",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskTypies_TemplateId",
                table: "JM_TaskTypies",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskUsers_CreatedUserId",
                table: "JM_TaskUsers",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskUsers_TaskId",
                table: "JM_TaskUsers",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TaskUsers_UserId",
                table: "JM_TaskUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_CreatedUserId",
                table: "JM_Teams",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Teams_ParentId",
                table: "JM_Teams",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateDetails_CreatedUserId",
                table: "JM_TemplateDetails",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateDetails_CustomColumnId",
                table: "JM_TemplateDetails",
                column: "CustomColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateDetails_TemplateId",
                table: "JM_TemplateDetails",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_Templates_CreatedUserId",
                table: "JM_Templates",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateStatus_CreatedUserId",
                table: "JM_TemplateStatus",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateStatus_StatusId",
                table: "JM_TemplateStatus",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JM_TemplateStatus_TemplateId",
                table: "JM_TemplateStatus",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_FilterConfigs_CreatedUserId",
                table: "SYS_FilterConfigs",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_FilterConfigs_UserId",
                table: "SYS_FilterConfigs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_ViewPermissionActionDetails_CreatedUserId",
                table: "SYS_ViewPermissionActionDetails",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "Nidx_SYS_ViewPermissionActionDetail_ViewPermissionActionId",
                table: "SYS_ViewPermissionActionDetails",
                column: "ViewPermissionActionId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_ViewPermissionActions_CreatedUserId",
                table: "SYS_ViewPermissionActions",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "Nidx_SYS_ViewPermissionAction_ViewPermissionId",
                table: "SYS_ViewPermissionActions",
                column: "ViewPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_ViewPermissionObject_CreatedUserId",
                table: "SYS_ViewPermissionObject",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "Nidx_SYS_ViewPermissionObject_ViewPermissionId",
                table: "SYS_ViewPermissionObject",
                column: "ViewPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SYS_ViewPermissions_CreatedUserId",
                table: "SYS_ViewPermissions",
                column: "CreatedUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JM_AccountCompanys");

            migrationBuilder.DropTable(
                name: "JM_AttachedFiles");

            migrationBuilder.DropTable(
                name: "JM_CommentTasks");

            migrationBuilder.DropTable(
                name: "JM_Notifycations");

            migrationBuilder.DropTable(
                name: "JM_NotifycationUsers");

            migrationBuilder.DropTable(
                name: "JM_ProjectMembers");

            migrationBuilder.DropTable(
                name: "JM_ProjectTeams");

            migrationBuilder.DropTable(
                name: "JM_TaskCustomColumnValues");

            migrationBuilder.DropTable(
                name: "JM_TaskTag");

            migrationBuilder.DropTable(
                name: "JM_TaskUsers");

            migrationBuilder.DropTable(
                name: "JM_TemplateStatus");

            migrationBuilder.DropTable(
                name: "SYS_FilterConfigs");

            migrationBuilder.DropTable(
                name: "Sys_RoleClaim");

            migrationBuilder.DropTable(
                name: "Sys_RoleGroup");

            migrationBuilder.DropTable(
                name: "Sys_Roles");

            migrationBuilder.DropTable(
                name: "Sys_UserClaim");

            migrationBuilder.DropTable(
                name: "Sys_UserLogin");

            migrationBuilder.DropTable(
                name: "Sys_UserRole");

            migrationBuilder.DropTable(
                name: "Sys_UserToken");

            migrationBuilder.DropTable(
                name: "SYS_VersionConfig");

            migrationBuilder.DropTable(
                name: "SYS_ViewPermissionActionDetails");

            migrationBuilder.DropTable(
                name: "SYS_ViewPermissionObject");

            migrationBuilder.DropTable(
                name: "JM_Files");

            migrationBuilder.DropTable(
                name: "JM_Comments");

            migrationBuilder.DropTable(
                name: "JM_Teams");

            migrationBuilder.DropTable(
                name: "JM_TemplateDetails");

            migrationBuilder.DropTable(
                name: "JM_Tags");

            migrationBuilder.DropTable(
                name: "JM_Tasks");

            migrationBuilder.DropTable(
                name: "SYS_ViewPermissionActions");

            migrationBuilder.DropTable(
                name: "JM_CustomColumns");

            migrationBuilder.DropTable(
                name: "JM_Priorities");

            migrationBuilder.DropTable(
                name: "JM_ProjectPhase");

            migrationBuilder.DropTable(
                name: "JM_Status");

            migrationBuilder.DropTable(
                name: "JM_TaskTypies");

            migrationBuilder.DropTable(
                name: "SYS_ViewPermissions");

            migrationBuilder.DropTable(
                name: "JM_Projects");

            migrationBuilder.DropTable(
                name: "JM_Templates");

            migrationBuilder.DropTable(
                name: "JM_Accounts");

            migrationBuilder.DropTable(
                name: "JM_Companys");
        }
    }
}
