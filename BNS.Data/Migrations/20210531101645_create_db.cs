using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BNS.Data.Migrations
{
    public partial class create_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CF_Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecurityQuestion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SecurityAnswer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsMainAccount = table.Column<bool>(type: "bit", nullable: true),
                    ShopCode = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_CF_Account", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CF_Area",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Area", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Bill",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TotalMoney = table.Column<double>(type: "float", nullable: true),
                    Sale = table.Column<double>(type: "float", nullable: true),
                    CustomerNeedPay = table.Column<double>(type: "float", nullable: true),
                    CustomerPaying = table.Column<double>(type: "float", nullable: true),
                    OrderIndex = table.Column<string>(type: "varchar(4000)", unicode: false, maxLength: 4000, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "date", nullable: true),
                    TotalCost = table.Column<double>(type: "float", nullable: true),
                    IsExample = table.Column<bool>(type: "bit", nullable: true),
                    RoomIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPayment = table.Column<bool>(type: "bit", nullable: true),
                    PaymentUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustomerIndex = table.Column<long>(type: "bigint", nullable: true),
                    TableOrderIndex = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Bill", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_BillDetail",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    Sale = table.Column<double>(type: "float", nullable: true),
                    TotalMoney = table.Column<double>(type: "float", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    OrderUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 200, nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 200, nullable: false),
                    BillID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_BillDetail", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CF_BookInfo",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateBook = table.Column<DateTime>(type: "datetime", nullable: true),
                    NumberPeople = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Note = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsBookDish = table.Column<bool>(type: "bit", nullable: true),
                    TableOrderIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_BookInfo", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_BookInfoRoomDetail",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_BookInfoRoomDetail", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Branch",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Branch", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Config",
                columns: table => new
                {
                    VersionDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "CF_Customer",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CMND = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    BrithDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CustomerType = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameFacebook = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Customer", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Decentralization",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Page = table.Column<string>(type: "ntext", nullable: true),
                    ByEmployeeCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ByPosition = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ByDepartment = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", unicode: false, maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Decentralization", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Department",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Department", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Discount",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateApplyType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Ngày áp dụng (tất cả/ theo ngày)"),
                    ForMon = table.Column<bool>(type: "bit", nullable: true),
                    ForTue = table.Column<bool>(type: "bit", nullable: true),
                    ForWed = table.Column<bool>(type: "bit", nullable: true),
                    ForThu = table.Column<bool>(type: "bit", nullable: true),
                    ForFri = table.Column<bool>(type: "bit", nullable: true),
                    ForSat = table.Column<bool>(type: "bit", nullable: true),
                    ForSun = table.Column<bool>(type: "bit", nullable: true),
                    ObjectType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Đối tượng ap dụng (Giảm giá trên tổng tiền thanh toán/Giảm giá trên từng món)"),
                    ReduceType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true, comment: "Giảm theo tỷ lệ (%)/Giảm bằng số tiề"),
                    RatioReduce = table.Column<double>(type: "float", nullable: true),
                    MoneyReduce = table.Column<double>(type: "float", nullable: true),
                    ApplyForBill = table.Column<bool>(type: "bit", nullable: true, comment: "Áp dụng khi hóa đơn thanh toán có giá trị lớn hơn"),
                    MoneyBill = table.Column<double>(type: "float", nullable: true, comment: "Số tiền áp dụng"),
                    ApplyForProductGroup = table.Column<bool>(type: "bit", nullable: true),
                    ApplyForProduct = table.Column<bool>(type: "bit", nullable: true),
                    ProductGroupValue = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ProductValue = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    ApplyTime = table.Column<bool>(type: "bit", nullable: true),
                    FromTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ToTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Discount", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Employee",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    NRIC = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    DateOfNRIC = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PlaceOfNRIC = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BrithDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    JoinedDate = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    PermanentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TemporaryAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmployeeImage = table.Column<string>(type: "ntext", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMainAccount = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Employee", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_EmployeeWorkingInfo",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeIndex = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: false),
                    PositionIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_EmployeeWorkingInfo", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_History",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FormName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Function = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_History_1", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Kitchen",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderIndex = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    QuantityWaitProcess = table.Column<int>(type: "int", nullable: true),
                    QuantityDone = table.Column<int>(type: "int", nullable: true),
                    QuantityUnProcess = table.Column<int>(type: "int", nullable: true),
                    WaitProcessDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DoneDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsPrioritize = table.Column<bool>(type: "bit", nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QuantityFinished = table.Column<int>(type: "int", nullable: true),
                    FinishedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Kitchen", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Order",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Sale = table.Column<double>(type: "float", nullable: true),
                    TotalMoney = table.Column<double>(type: "float", nullable: true),
                    IsAnnounced = table.Column<bool>(type: "bit", nullable: true),
                    IsDone = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QuantityOld = table.Column<int>(type: "int", nullable: true),
                    IsInvalid = table.Column<bool>(type: "bit", nullable: true),
                    QuantityAnnounced = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true),
                    IsPrioritize = table.Column<bool>(type: "bit", nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TableOrderIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    IsBook = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Invoice", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Payment",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TypeName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Payer = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VendorIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Value = table.Column<double>(type: "float", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    IsExample = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Payment", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Position",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Position", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_PriceWithArea",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RoomIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_PriceWithArea", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Product",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProductType = table.Column<int>(type: "int", nullable: true),
                    GroupIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    Inventory = table.Column<double>(type: "float", nullable: true),
                    UnitIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDirectSales = table.Column<bool>(type: "bit", nullable: true),
                    IsSubElement = table.Column<bool>(type: "bit", nullable: true),
                    IsBusiness = table.Column<int>(type: "int", nullable: true),
                    RestMin = table.Column<double>(type: "float", nullable: true),
                    RestMax = table.Column<double>(type: "float", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Image1 = table.Column<string>(type: "ntext", nullable: true),
                    Image2 = table.Column<string>(type: "ntext", nullable: true),
                    Image3 = table.Column<string>(type: "ntext", nullable: true),
                    Image4 = table.Column<string>(type: "ntext", nullable: true),
                    Image5 = table.Column<string>(type: "ntext", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    InventoryTemp = table.Column<double>(type: "float", nullable: true),
                    InventoryRest = table.Column<double>(type: "float", nullable: true),
                    InventoryRestOld = table.Column<double>(type: "float", nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Product", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_ProductElement",
                columns: table => new
                {
                    ProductEleIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    Cost = table.Column<double>(type: "float", nullable: true),
                    TotalMoney = table.Column<double>(type: "float", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_ProductElement", x => new { x.ProductEleIndex, x.ProductIndex });
                });

            migrationBuilder.CreateTable(
                name: "CF_ProductGroup",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    Parent = table.Column<int>(type: "int", nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_ProductGroup", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_ProductSub",
                columns: table => new
                {
                    ProductSubIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_ProductSub", x => new { x.ProductSubIndex, x.ProductIndex });
                });

            migrationBuilder.CreateTable(
                name: "CF_PurchaseOrder",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendorIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    QuantityProduct = table.Column<double>(type: "float", nullable: true),
                    TotalMoney = table.Column<decimal>(type: "money", nullable: true),
                    Sale = table.Column<decimal>(type: "money", nullable: true),
                    ShoudPayVendor = table.Column<decimal>(type: "money", nullable: true),
                    PayVendor = table.Column<decimal>(type: "money", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    VendorShoudPay = table.Column<decimal>(type: "money", nullable: true),
                    VendorPay = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_PurchaseOrder", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_PurchaseOrderDetail",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: true),
                    Sale = table.Column<decimal>(type: "money", nullable: true),
                    TotalMoney = table.Column<decimal>(type: "money", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ImportPrice = table.Column<decimal>(type: "money", nullable: true),
                    Note = table.Column<string>(type: "ntext", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_PurchaseOrderDetail", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Room",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AreaIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Room", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_SecurityQuestion",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "ntext", nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_SecurityQuestion", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Shift",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TimeStart = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    TimeEnd = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Shift", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_ShiftSettingByDepartment",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateApply = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    DepartmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_ShiftSettingByDepartment", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CF_ShiftSettingByEmployee",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateApply = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    EmployeeATID = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_ShiftSettingByEmployee", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CF_ShiftSettingByPosition",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateApply = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PositionID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_ShiftSettingByPosition", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CF_Shop",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address = table.Column<string>(type: "ntext", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NumberOfBranch = table.Column<int>(type: "int", nullable: true),
                    FromDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LogoImage = table.Column<string>(type: "ntext", nullable: true),
                    IsCreatedDataExample = table.Column<bool>(type: "bit", nullable: true),
                    IsShowCreateDataExampleMenu = table.Column<bool>(type: "bit", nullable: true),
                    NotShowDataExample = table.Column<bool>(type: "bit", nullable: true),
                    NotShowHelp = table.Column<bool>(type: "bit", nullable: true),
                    VersionType = table.Column<int>(type: "int", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LicenseText = table.Column<string>(type: "ntext", nullable: true),
                    Module = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Shop", x => new { x.Index, x.Code });
                });

            migrationBuilder.CreateTable(
                name: "CF_ShopIPSetting",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    IpAddress = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_ShopIPSetting", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_ShopPrintSetting",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrintBillName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PrintBillIP = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PrintBillPort = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ServiceIP = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ServicePort = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrintType = table.Column<int>(type: "int", nullable: true),
                    IsPayAndPrintBill = table.Column<bool>(type: "bit", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_ShopPrintSetting_1", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_ShopSetting",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MSG_GoiMonKhiHetTonKho = table.Column<bool>(type: "bit", nullable: true),
                    MSG_ThanhToanCacMonDaOrder = table.Column<bool>(type: "bit", nullable: true),
                    MSG_KhongSuDungTinhNangBep = table.Column<bool>(type: "bit", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MSG_TatCaHangHoaChuyenBepKhiThongBao = table.Column<bool>(type: "bit", nullable: true),
                    MSG_KiemTraCaLamViec = table.Column<bool>(type: "bit", nullable: true),
                    MSG_ChanIPNgoaiDanhSach = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_ShopSetting", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_TableOrder",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CustomerIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsBook = table.Column<bool>(type: "bit", nullable: true),
                    IsPrintBill = table.Column<bool>(type: "bit", nullable: true),
                    PrintedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PrintedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_TableOrder", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Unit",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Unit", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_Vendor",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Company = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TaxCode = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    VendorGroupIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Vendor", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CF_VendorGroup",
                columns: table => new
                {
                    Index = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<Guid>(type: "uniqueidentifier", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BranchIndex = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_VendorGroup", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CL_Analysis",
                columns: table => new
                {
                    Index = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AnalysisGroupIndex = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<int>(type: "int", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_Analysis", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CL_AnalysisGroup",
                columns: table => new
                {
                    Index = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<int>(type: "int", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_AnalysisGroup", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CL_MedicalList",
                columns: table => new
                {
                    index = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientIndex = table.Column<long>(type: "bigint", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<int>(type: "int", nullable: true),
                    ShopIndex = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_MedicalList", x => x.index);
                });

            migrationBuilder.CreateTable(
                name: "CL_Medicine",
                columns: table => new
                {
                    Index = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MedicineType = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Inventory = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    UnitIndex = table.Column<int>(type: "int", nullable: true),
                    RestMin = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    RestMax = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Image1 = table.Column<string>(type: "ntext", nullable: true),
                    Image2 = table.Column<string>(type: "ntext", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<int>(type: "int", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<int>(type: "int", nullable: true),
                    Used = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_Medicine", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CL_MedicineType",
                columns: table => new
                {
                    Index = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<int>(type: "int", nullable: true),
                    IsExample = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_MedicineType", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CL_Patient",
                columns: table => new
                {
                    Index = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ExaminaCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    CMND = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<int>(type: "int", nullable: true),
                    ShopIndex = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_Patient", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CL_Prescription",
                columns: table => new
                {
                    Index = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PatientIndex = table.Column<long>(type: "bigint", nullable: true),
                    MedicalListIndex = table.Column<long>(type: "bigint", nullable: true),
                    ExaminaMoney = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    ExaminaMedicine = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    TotalMoney = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    NgayKhamBenh = table.Column<DateTime>(type: "date", nullable: true),
                    TrieuChung = table.Column<string>(type: "ntext", nullable: true),
                    BacSyKhamIndex = table.Column<long>(type: "bigint", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<int>(type: "int", nullable: true),
                    ShopIndex = table.Column<int>(type: "int", nullable: true),
                    ChanDoan = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_Prescription", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "CL_PrescriptionDetail",
                columns: table => new
                {
                    index = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionIndex = table.Column<long>(type: "bigint", nullable: true),
                    PatientIndex = table.Column<long>(type: "bigint", nullable: true),
                    MedicalListIndex = table.Column<long>(type: "bigint", nullable: true),
                    MedicineIndex = table.Column<long>(type: "bigint", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TotalMoney = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDelete = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<int>(type: "int", nullable: true),
                    ShopIndex = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_CL_PrescriptionDetail", x => x.index);
                });

            migrationBuilder.CreateTable(
                name: "CL_Unit",
                columns: table => new
                {
                    Index = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShopIndex = table.Column<int>(type: "int", nullable: true),
                    BranchIndex = table.Column<int>(type: "int", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CL_Unit", x => x.Index);
                });

            migrationBuilder.CreateTable(
                name: "Sys_Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sys_RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_RoleClaim", x => x.Id);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CF_Account");

            migrationBuilder.DropTable(
                name: "CF_Area");

            migrationBuilder.DropTable(
                name: "CF_Bill");

            migrationBuilder.DropTable(
                name: "CF_BillDetail");

            migrationBuilder.DropTable(
                name: "CF_BookInfo");

            migrationBuilder.DropTable(
                name: "CF_BookInfoRoomDetail");

            migrationBuilder.DropTable(
                name: "CF_Branch");

            migrationBuilder.DropTable(
                name: "CF_Config");

            migrationBuilder.DropTable(
                name: "CF_Customer");

            migrationBuilder.DropTable(
                name: "CF_Decentralization");

            migrationBuilder.DropTable(
                name: "CF_Department");

            migrationBuilder.DropTable(
                name: "CF_Discount");

            migrationBuilder.DropTable(
                name: "CF_Employee");

            migrationBuilder.DropTable(
                name: "CF_EmployeeWorkingInfo");

            migrationBuilder.DropTable(
                name: "CF_History");

            migrationBuilder.DropTable(
                name: "CF_Kitchen");

            migrationBuilder.DropTable(
                name: "CF_Order");

            migrationBuilder.DropTable(
                name: "CF_Payment");

            migrationBuilder.DropTable(
                name: "CF_Position");

            migrationBuilder.DropTable(
                name: "CF_PriceWithArea");

            migrationBuilder.DropTable(
                name: "CF_Product");

            migrationBuilder.DropTable(
                name: "CF_ProductElement");

            migrationBuilder.DropTable(
                name: "CF_ProductGroup");

            migrationBuilder.DropTable(
                name: "CF_ProductSub");

            migrationBuilder.DropTable(
                name: "CF_PurchaseOrder");

            migrationBuilder.DropTable(
                name: "CF_PurchaseOrderDetail");

            migrationBuilder.DropTable(
                name: "CF_Room");

            migrationBuilder.DropTable(
                name: "CF_SecurityQuestion");

            migrationBuilder.DropTable(
                name: "CF_Shift");

            migrationBuilder.DropTable(
                name: "CF_ShiftSettingByDepartment");

            migrationBuilder.DropTable(
                name: "CF_ShiftSettingByEmployee");

            migrationBuilder.DropTable(
                name: "CF_ShiftSettingByPosition");

            migrationBuilder.DropTable(
                name: "CF_Shop");

            migrationBuilder.DropTable(
                name: "CF_ShopIPSetting");

            migrationBuilder.DropTable(
                name: "CF_ShopPrintSetting");

            migrationBuilder.DropTable(
                name: "CF_ShopSetting");

            migrationBuilder.DropTable(
                name: "CF_TableOrder");

            migrationBuilder.DropTable(
                name: "CF_Unit");

            migrationBuilder.DropTable(
                name: "CF_Vendor");

            migrationBuilder.DropTable(
                name: "CF_VendorGroup");

            migrationBuilder.DropTable(
                name: "CL_Analysis");

            migrationBuilder.DropTable(
                name: "CL_AnalysisGroup");

            migrationBuilder.DropTable(
                name: "CL_MedicalList");

            migrationBuilder.DropTable(
                name: "CL_Medicine");

            migrationBuilder.DropTable(
                name: "CL_MedicineType");

            migrationBuilder.DropTable(
                name: "CL_Patient");

            migrationBuilder.DropTable(
                name: "CL_Prescription");

            migrationBuilder.DropTable(
                name: "CL_PrescriptionDetail");

            migrationBuilder.DropTable(
                name: "CL_Unit");

            migrationBuilder.DropTable(
                name: "Sys_Role");

            migrationBuilder.DropTable(
                name: "Sys_RoleClaim");

            migrationBuilder.DropTable(
                name: "Sys_UserClaim");

            migrationBuilder.DropTable(
                name: "Sys_UserLogin");

            migrationBuilder.DropTable(
                name: "Sys_UserRole");

            migrationBuilder.DropTable(
                name: "Sys_UserToken");
        }
    }
}
