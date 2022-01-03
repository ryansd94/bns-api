using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BNS.DataDBFrist.Models
{
    public partial class bns_dbtestContext : DbContext
    {
        public bns_dbtestContext()
        {
        }

        public bns_dbtestContext(DbContextOptions<bns_dbtestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CfAccount> CfAccounts { get; set; }
        public virtual DbSet<CfArea> CfAreas { get; set; }
        public virtual DbSet<CfBill> CfBills { get; set; }
        public virtual DbSet<CfBillDetail> CfBillDetails { get; set; }
        public virtual DbSet<CfBookInfo> CfBookInfos { get; set; }
        public virtual DbSet<CfBookInfoRoomDetail> CfBookInfoRoomDetails { get; set; }
        public virtual DbSet<CfBranch> CfBranches { get; set; }
        public virtual DbSet<CfConfig> CfConfigs { get; set; }
        public virtual DbSet<CfCustomer> CfCustomers { get; set; }
        public virtual DbSet<CfDecentralization> CfDecentralizations { get; set; }
        public virtual DbSet<CfDepartment> CfDepartments { get; set; }
        public virtual DbSet<CfDiscount> CfDiscounts { get; set; }
        public virtual DbSet<CfEmployee> CfEmployees { get; set; }
        public virtual DbSet<CfEmployeeWorkingInfo> CfEmployeeWorkingInfos { get; set; }
        public virtual DbSet<CfHistory> CfHistories { get; set; }
        public virtual DbSet<CfKitchen> CfKitchens { get; set; }
        public virtual DbSet<CfOrder> CfOrders { get; set; }
        public virtual DbSet<CfPayment> CfPayments { get; set; }
        public virtual DbSet<CfPosition> CfPositions { get; set; }
        public virtual DbSet<CfPriceWithArea> CfPriceWithAreas { get; set; }
        public virtual DbSet<CfProduct> CfProducts { get; set; }
        public virtual DbSet<CfProductElement> CfProductElements { get; set; }
        public virtual DbSet<CfProductGroup> CfProductGroups { get; set; }
        public virtual DbSet<CfProductSub> CfProductSubs { get; set; }
        public virtual DbSet<CfPurchaseOrder> CfPurchaseOrders { get; set; }
        public virtual DbSet<CfPurchaseOrderDetail> CfPurchaseOrderDetails { get; set; }
        public virtual DbSet<CfRoom> CfRooms { get; set; }
        public virtual DbSet<CfSecurityQuestion> CfSecurityQuestions { get; set; }
        public virtual DbSet<CfShift> CfShifts { get; set; }
        public virtual DbSet<CfShiftSettingByDepartment> CfShiftSettingByDepartments { get; set; }
        public virtual DbSet<CfShiftSettingByEmployee> CfShiftSettingByEmployees { get; set; }
        public virtual DbSet<CfShiftSettingByPosition> CfShiftSettingByPositions { get; set; }
        public virtual DbSet<CfShop> CfShops { get; set; }
        public virtual DbSet<CfShopIpsetting> CfShopIpsettings { get; set; }
        public virtual DbSet<CfShopPrintSetting> CfShopPrintSettings { get; set; }
        public virtual DbSet<CfShopSetting> CfShopSettings { get; set; }
        public virtual DbSet<CfTableOrder> CfTableOrders { get; set; }
        public virtual DbSet<CfUnit> CfUnits { get; set; }
        public virtual DbSet<CfVendor> CfVendors { get; set; }
        public virtual DbSet<CfVendorGroup> CfVendorGroups { get; set; }
        public virtual DbSet<ClAnalysis> ClAnalyses { get; set; }
        public virtual DbSet<ClAnalysisGroup> ClAnalysisGroups { get; set; }
        public virtual DbSet<ClMedicalList> ClMedicalLists { get; set; }
        public virtual DbSet<ClMedicine> ClMedicines { get; set; }
        public virtual DbSet<ClMedicineType> ClMedicineTypes { get; set; }
        public virtual DbSet<ClPatient> ClPatients { get; set; }
        public virtual DbSet<ClPrescription> ClPrescriptions { get; set; }
        public virtual DbSet<ClPrescriptionDetail> ClPrescriptionDetails { get; set; }
        public virtual DbSet<ClUnit> ClUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=207.148.94.95\\SQLTEST;Database=bns_dbtest;User ID=sa;Password=develop@123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<CfAccount>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Account");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.SecurityAnswer).HasMaxLength(50);

                entity.Property(e => e.ShopCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CfArea>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Area");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfBill>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Bill");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.OrderIndex)
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.PaymentUser).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfBillDetail>(entity =>
            {
                entity.ToTable("CF_BillDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderUser).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(200);
            });

            modelBuilder.Entity<CfBookInfo>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_BookInfo");

                entity.Property(e => e.DateBook).HasColumnType("datetime");

                entity.Property(e => e.Note)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.NumberPeople)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfBookInfoRoomDetail>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_BookInfoRoomDetail");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfBranch>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Branch");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfConfig>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CF_Config");

                entity.Property(e => e.VersionDate).HasColumnType("date");
            });

            modelBuilder.Entity<CfCustomer>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Customer");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.BrithDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Cmnd)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CMND");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameFacebook).HasMaxLength(500);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfDecentralization>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Decentralization");

                entity.Property(e => e.ByEmployeeCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Page).HasColumnType("ntext");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CfDepartment>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Department");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfDiscount>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Discount");

                entity.Property(e => e.ApplyForBill).HasComment("Áp dụng khi hóa đơn thanh toán có giá trị lớn hơn");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DateApplyType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Ngày áp dụng (tất cả/ theo ngày)");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.MoneyBill).HasComment("Số tiền áp dụng");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.ObjectType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Đối tượng ap dụng (Giảm giá trên tổng tiền thanh toán/Giảm giá trên từng món)");

                entity.Property(e => e.ProductGroupValue).IsUnicode(false);

                entity.Property(e => e.ProductValue).IsUnicode(false);

                entity.Property(e => e.ReduceType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Giảm theo tỷ lệ (%)/Giảm bằng số tiề");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeeCode);

                entity.ToTable("CF_Employee");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.BrithDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.DateOfNric)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("DateOfNRIC");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EmployeeImage).HasColumnType("ntext");

                entity.Property(e => e.EmployeeName).HasMaxLength(200);

                entity.Property(e => e.JoinedDate).HasColumnType("smalldatetime");

                entity.Property(e => e.Nric)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("NRIC");

                entity.Property(e => e.PermanentAddress).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.PlaceOfNric)
                    .HasMaxLength(200)
                    .HasColumnName("PlaceOfNRIC");

                entity.Property(e => e.TemporaryAddress).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfEmployeeWorkingInfo>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_EmployeeWorkingInfo");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfHistory>(entity =>
            {
                entity.HasKey(e => e.Index)
                    .HasName("PK_CF_History_1");

                entity.ToTable("CF_History");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FormName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Function).HasMaxLength(100);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfKitchen>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Kitchen");

                entity.Property(e => e.DoneDate).HasColumnType("datetime");

                entity.Property(e => e.FinishedDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);

                entity.Property(e => e.WaitProcessDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CfOrder>(entity =>
            {
                entity.HasKey(e => e.Index)
                    .HasName("PK_CF_Invoice");

                entity.ToTable("CF_Order");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfPayment>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Payment");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("smalldatetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Payer).HasMaxLength(500);

                entity.Property(e => e.TypeName).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfPosition>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Position");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfPriceWithArea>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_PriceWithArea");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfProduct>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Product");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Image1).HasColumnType("ntext");

                entity.Property(e => e.Image2).HasColumnType("ntext");

                entity.Property(e => e.Image3).HasColumnType("ntext");

                entity.Property(e => e.Image4).HasColumnType("ntext");

                entity.Property(e => e.Image5).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfProductElement>(entity =>
            {
                entity.HasKey(e => new { e.ProductEleIndex, e.ProductIndex });

                entity.ToTable("CF_ProductElement");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfProductGroup>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_ProductGroup");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfProductSub>(entity =>
            {
                entity.HasKey(e => new { e.ProductSubIndex, e.ProductIndex });

                entity.ToTable("CF_ProductSub");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfPurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_PurchaseOrder");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.PayVendor).HasColumnType("money");

                entity.Property(e => e.Sale).HasColumnType("money");

                entity.Property(e => e.ShoudPayVendor).HasColumnType("money");

                entity.Property(e => e.TotalMoney).HasColumnType("money");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);

                entity.Property(e => e.VendorPay).HasColumnType("money");

                entity.Property(e => e.VendorShoudPay).HasColumnType("money");
            });

            modelBuilder.Entity<CfPurchaseOrderDetail>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_PurchaseOrderDetail");

                entity.Property(e => e.ImportPrice).HasColumnType("money");

                entity.Property(e => e.Note).HasColumnType("ntext");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Sale).HasColumnType("money");

                entity.Property(e => e.TotalMoney).HasColumnType("money");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfRoom>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Room");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfSecurityQuestion>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_SecurityQuestion");

                entity.Property(e => e.Question).HasColumnType("ntext");
            });

            modelBuilder.Entity<CfShift>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Shift");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.TimeEnd)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.TimeStart)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfShiftSettingByDepartment>(entity =>
            {
                entity.ToTable("CF_ShiftSettingByDepartment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateApply)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfShiftSettingByEmployee>(entity =>
            {
                entity.ToTable("CF_ShiftSettingByEmployee");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateApply)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeAtid)
                    .HasMaxLength(50)
                    .HasColumnName("EmployeeATID");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfShiftSettingByPosition>(entity =>
            {
                entity.ToTable("CF_ShiftSettingByPosition");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateApply)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfShop>(entity =>
            {
                entity.HasKey(e => new { e.Index, e.Code });

                entity.ToTable("CF_Shop");

                entity.Property(e => e.Index).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasColumnType("ntext");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.LicenseText).HasColumnType("ntext");

                entity.Property(e => e.LogoImage).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(500);

                entity.Property(e => e.RegisterDate).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<CfShopIpsetting>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_ShopIPSetting");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfShopPrintSetting>(entity =>
            {
                entity.HasKey(e => e.Index)
                    .HasName("PK_CF_ShopPrintSetting_1");

                entity.ToTable("CF_ShopPrintSetting");

                entity.Property(e => e.Note).HasMaxLength(4000);

                entity.Property(e => e.PrintBillIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PrintBillIP");

                entity.Property(e => e.PrintBillName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PrintBillPort)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceIp)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ServiceIP");

                entity.Property(e => e.ServicePort)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CfShopSetting>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_ShopSetting");

                entity.Property(e => e.MsgChanIpngoaiDanhSach).HasColumnName("MSG_ChanIPNgoaiDanhSach");

                entity.Property(e => e.MsgGoiMonKhiHetTonKho).HasColumnName("MSG_GoiMonKhiHetTonKho");

                entity.Property(e => e.MsgKhongSuDungTinhNangBep).HasColumnName("MSG_KhongSuDungTinhNangBep");

                entity.Property(e => e.MsgKiemTraCaLamViec).HasColumnName("MSG_KiemTraCaLamViec");

                entity.Property(e => e.MsgTatCaHangHoaChuyenBepKhiThongBao).HasColumnName("MSG_TatCaHangHoaChuyenBepKhiThongBao");

                entity.Property(e => e.MsgThanhToanCacMonDaOrder).HasColumnName("MSG_ThanhToanCacMonDaOrder");
            });

            modelBuilder.Entity<CfTableOrder>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_TableOrder");

                entity.Property(e => e.PrintedDate).HasColumnType("datetime");

                entity.Property(e => e.PrintedUser).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfUnit>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Unit");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfVendor>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Vendor");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Company).HasMaxLength(200);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(200);

                entity.Property(e => e.TaxCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CfVendorGroup>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_VendorGroup");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<ClAnalysis>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CL_Analysis");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<ClAnalysisGroup>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CL_AnalysisGroup");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<ClMedicalList>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CL_MedicalList");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<ClMedicine>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CL_Medicine");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Image1).HasColumnType("ntext");

                entity.Property(e => e.Image2).HasColumnType("ntext");

                entity.Property(e => e.Inventory).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RestMax).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RestMin).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);

                entity.Property(e => e.Used).HasMaxLength(4000);
            });

            modelBuilder.Entity<ClMedicineType>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CL_MedicineType");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<ClPatient>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CL_Patient");

                entity.Property(e => e.Address).HasMaxLength(500);

                entity.Property(e => e.Cmnd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CMND");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.ExaminaCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<ClPrescription>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CL_Prescription");

                entity.Property(e => e.ChanDoan).HasColumnType("ntext");

                entity.Property(e => e.ExaminaMedicine).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ExaminaMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.NgayKhamBenh).HasColumnType("date");

                entity.Property(e => e.Note).HasMaxLength(2000);

                entity.Property(e => e.PrescriptionCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TrieuChung).HasColumnType("ntext");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<ClPrescriptionDetail>(entity =>
            {
                entity.HasKey(e => e.Index)
                    .HasName("PK_CL_CL_PrescriptionDetail");

                entity.ToTable("CL_PrescriptionDetail");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TotalMoney).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<ClUnit>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CL_Unit");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.NameInEng).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
