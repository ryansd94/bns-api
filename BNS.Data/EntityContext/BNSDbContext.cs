using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using BNS.Data.Entities;
using BNS.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BNS.Data.Entities.JM_Entities;

namespace BNS.Data.EntityContext
{
    public class BNSDbContext : DbContext
    {
        public BNSDbContext()
        {
        }

        public BNSDbContext(DbContextOptions<BNSDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Data Source=125.212.226.105,1968;Initial Catalog=test_bidv_2;User ID=sa;Password=TGn<@7qY");
            }
        }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CF_AccountConfig());
            modelBuilder.ApplyConfiguration(new Sys_RoleConfig());
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("Sys_UserClaim");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("Sys_UserRole").HasKey(s => new
            {
                s.UserId,
                s.RoleId
            });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("Sys_UserLogin").HasKey(s => s.UserId);
            //modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("Sys_RoleClaim");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("Sys_UserToken").HasKey(s => s.UserId);






            modelBuilder.Entity<CF_Shop>()
            .HasMany(a => a.CF_Accounts)
            .WithOne(b => b.CF_Shop)
            .HasForeignKey(b => b.ShopIndex);


            modelBuilder.Entity<CF_Shop>()
            .HasMany(a => a.Cf_Employees)
            .WithOne(b => b.CF_Shop)
            .HasForeignKey(b => b.ShopIndex);




            modelBuilder.Entity<CF_Shop>()
            .HasMany(a => a.CF_Areas)
            .WithOne(b => b.CF_Shop)
            .HasForeignKey(b => b.ShopIndex);

            modelBuilder.Entity<CF_Shop>()
            .HasMany(a => a.CF_Branchs)
            .WithOne(b => b.CF_Shop)
            .HasForeignKey(b => b.ShopIndex);

            modelBuilder.Entity<CF_Shop>()
            .HasMany(a => a.CF_Departments)
            .WithOne(b => b.CF_Shop)
            .HasForeignKey(b => b.ShopIndex);
            modelBuilder.Entity<CF_Shop>()
            .HasMany(a => a.CF_Positions)
            .WithOne(b => b.CF_Shop)
            .HasForeignKey(b => b.ShopIndex);

            modelBuilder.Entity<CF_Account>()
            .HasOne(a => a.Cf_Employee)
            .WithOne(b => b.CF_Account)
            .HasForeignKey<CF_Employee>(b => b.AccountIndex);



            modelBuilder.Entity<CF_Position>()
            .HasMany(a => a.CF_Employees)
            .WithOne(b => b.CF_Position)
            .HasForeignKey(b => b.PositionIndex);




            modelBuilder.Entity<CF_Department>()
            .HasMany(a => a.CF_Employees)
            .WithOne(b => b.CF_Department)
            .HasForeignKey(b => b.DepartmentIndex);


            modelBuilder.Entity<CF_Branch>()
            .HasMany(a => a.CF_Areas)
            .WithOne(b => b.CF_Branch)
            .HasForeignKey(b => b.BranchIndex);

            modelBuilder.Entity<CF_Branch>()
            .HasMany(a => a.CF_Employees)
            .WithOne(b => b.CF_Branch)
            .HasForeignKey(b => b.BranchIndex);



            modelBuilder.Entity<JM_Account>()
            .HasMany(a => a.JM_TeamsCreate)
            .WithOne(b => b.CreateUserAccount)
            .HasForeignKey(b => b.CreatedUser);


            modelBuilder.Entity<JM_Account>()
            .HasMany(a => a.JM_TeamsUpdate)
            .WithOne(b => b.UpdateUserAccount)
            .HasForeignKey(b => b.UpdatedUser);



            modelBuilder.Entity<JM_Project>().HasMany(x => x.JM_ProjectTeams);
            modelBuilder.Entity<JM_Project>().HasMany(x => x.JM_ProjectMembers);
            modelBuilder.Entity<Sys_RoleGroup>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("Sys_RoleGroup");

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });
            modelBuilder.Entity<SYS_VersionConfig>(entity =>
            {
                entity.HasKey(e => e.VersionType);

                entity.ToTable("SYS_VersionConfig");

            });

            modelBuilder.Entity<CF_Area>(entity =>
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

            modelBuilder.Entity<CF_Bill>(entity =>
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

                //entity.HasOne(e => e.CF_Account).WithMany(s => s.CF_Bills).HasForeignKey(s => s.UpdatedUserId);
            });

            modelBuilder.Entity<CF_BillDetail>(entity =>
            {
                entity.ToTable("CF_BillDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BillId).HasColumnName("BillID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderUser).HasMaxLength(200);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(200);
            });

            modelBuilder.Entity<CF_BookInfo>(entity =>
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

            modelBuilder.Entity<CF_BookInfoRoomDetail>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_BookInfoRoomDetail");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CF_Branch>(entity =>
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

            modelBuilder.Entity<Sys_RoleClaim>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("Sys_RoleClaim");
            });

            modelBuilder.Entity<CF_Config>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CF_Config");

                entity.Property(e => e.VersionDate).HasColumnType("date");
            });

            modelBuilder.Entity<CF_Customer>(entity =>
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

            modelBuilder.Entity<CF_Decentralization>(entity =>
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

            modelBuilder.Entity<CF_Department>(entity =>
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

            modelBuilder.Entity<CF_Discount>(entity =>
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

            modelBuilder.Entity<CF_Employee>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_Employee");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.BrithDate).HasColumnType("smalldatetime");


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

            modelBuilder.Entity<CF_EmployeeWorkingInfo>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_EmployeeWorkingInfo");

                entity.Property(e => e.EmployeeIndex).HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CF_History>(entity =>
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

            modelBuilder.Entity<CF_Kitchen>(entity =>
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

            modelBuilder.Entity<CF_Order>(entity =>
            {
                entity.HasKey(e => e.Index)
                    .HasName("PK_CF_Invoice");

                entity.ToTable("CF_Order");

                entity.Property(e => e.Note).HasMaxLength(500);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
                //entity.HasOne(e => e.CF_Account).WithMany(s => s.CF_Orders).HasForeignKey(s => s.UpdatedUserId);
            });

            modelBuilder.Entity<CF_Payment>(entity =>
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

            modelBuilder.Entity<CF_Position>(entity =>
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

            modelBuilder.Entity<CF_PriceWithArea>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_PriceWithArea");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CF_Product>(entity =>
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

            modelBuilder.Entity<CF_ProductElement>(entity =>
            {
                entity.HasKey(e => new { e.ProductEleIndex, e.ProductIndex });

                entity.ToTable("CF_ProductElement");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CF_ProductGroup>(entity =>
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

            modelBuilder.Entity<CF_ProductSub>(entity =>
            {
                entity.HasKey(e => new { e.ProductSubIndex, e.ProductIndex });

                entity.ToTable("CF_ProductSub");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CF_PurchaseOrder>(entity =>
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

            modelBuilder.Entity<CF_PurchaseOrderDetail>(entity =>
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

            modelBuilder.Entity<CF_Room>(entity =>
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

            modelBuilder.Entity<Cf_SecurityQuestion>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_SecurityQuestion");

                entity.Property(e => e.Question).HasColumnType("ntext");
            });

            modelBuilder.Entity<CF_Shift>(entity =>
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

            modelBuilder.Entity<CF_ShiftSettingByDepartment>(entity =>
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

            modelBuilder.Entity<CF_ShiftSettingByEmployee>(entity =>
            {
                entity.ToTable("CF_ShiftSettingByEmployee");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateApply)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeIndex)
                    .HasMaxLength(50)
                    .HasColumnName("EmployeeATID");

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CF_ShiftSettingByPosition>(entity =>
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

            modelBuilder.Entity<CF_Shop>(entity =>
            {
                entity.HasKey(e => new { e.Index });

                entity.ToTable("CF_Shop");

                entity.Property(e => e.Index).ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasMaxLength(25)
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

            modelBuilder.Entity<CF_ShopIpsetting>(entity =>
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

            modelBuilder.Entity<CF_ShopPrintSetting>(entity =>
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

            modelBuilder.Entity<CF_ShopSetting>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_ShopSetting");

                entity.Property(e => e.MsgChanIpngoaiDanhSach).HasColumnName("MSG_ChanIPNgoaiDanhSach");

                entity.Property(e => e.IsGoiMonKhiHetTonKho).HasColumnName("MSG_GoiMonKhiHetTonKho");

                entity.Property(e => e.MsgKhongSuDungTinhNangBep).HasColumnName("MSG_KhongSuDungTinhNangBep");

                entity.Property(e => e.MsgKiemTraCaLamViec).HasColumnName("MSG_KiemTraCaLamViec");

                entity.Property(e => e.MsgTatCaHangHoaChuyenBepKhiThongBao).HasColumnName("MSG_TatCaHangHoaChuyenBepKhiThongBao");

                entity.Property(e => e.IsThanhToanCacMonDaOrder).HasColumnName("MSG_ThanhToanCacMonDaOrder");
            });

            modelBuilder.Entity<CF_TableOrder>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("CF_TableOrder");

                entity.Property(e => e.PrintedDate).HasColumnType("datetime");

                entity.Property(e => e.PrintedUser).HasMaxLength(50);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedUser).HasMaxLength(50);
            });

            modelBuilder.Entity<CF_Unit>(entity =>
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

            modelBuilder.Entity<CF_Vendor>(entity =>
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

            modelBuilder.Entity<CF_VendorGroup>(entity =>
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
        }


        //public DbSet<SYS_Account> SYS_Accounts { get; set; }
        //public DbSet<SYS_Shop> SYS_Shops { get; set; }
        public virtual DbSet<CF_Account> CF_Accounts { get; set; }
        public virtual DbSet<CF_Area> CF_Areas { get; set; }
        public virtual DbSet<CF_Bill> CfBills { get; set; }
        public virtual DbSet<CF_BillDetail> CfBillDetails { get; set; }
        public virtual DbSet<CF_BookInfo> CfBookInfos { get; set; }
        public virtual DbSet<CF_BookInfoRoomDetail> CfBookInfoRoomDetails { get; set; }
        public virtual DbSet<CF_Branch> CF_Branches { get; set; }
        public virtual DbSet<CF_Config> CfConfigs { get; set; }
        public virtual DbSet<CF_Customer> CfCustomers { get; set; }
        public virtual DbSet<CF_Decentralization> CfDecentralizations { get; set; }
        public virtual DbSet<CF_Department> CF_Departments { get; set; }
        public virtual DbSet<CF_Discount> CfDiscounts { get; set; }
        public virtual DbSet<CF_Employee> CF_Employees { get; set; }
        public virtual DbSet<CF_EmployeeWorkingInfo> CfEmployeeWorkingInfos { get; set; }
        public virtual DbSet<CF_History> CfHistories { get; set; }
        public virtual DbSet<CF_Kitchen> CfKitchens { get; set; }
        public virtual DbSet<CF_Order> CfOrders { get; set; }
        public virtual DbSet<CF_Payment> CfPayments { get; set; }
        public virtual DbSet<CF_Position> CF_Positions { get; set; }
        public virtual DbSet<CF_PriceWithArea> CfPriceWithAreas { get; set; }
        public virtual DbSet<CF_Product> CfProducts { get; set; }
        public virtual DbSet<CF_ProductElement> CfProductElements { get; set; }
        public virtual DbSet<CF_ProductGroup> CfProductGroups { get; set; }
        public virtual DbSet<CF_ProductSub> CfProductSubs { get; set; }
        public virtual DbSet<CF_PurchaseOrder> CfPurchaseOrders { get; set; }
        public virtual DbSet<CF_PurchaseOrderDetail> CfPurchaseOrderDetails { get; set; }
        public virtual DbSet<CF_Room> CfRooms { get; set; }
        public virtual DbSet<Cf_SecurityQuestion> CfSecurityQuestions { get; set; }
        public virtual DbSet<CF_Shift> CfShifts { get; set; }
        public virtual DbSet<CF_ShiftSettingByDepartment> CfShiftSettingByDepartments { get; set; }
        public virtual DbSet<CF_ShiftSettingByEmployee> CfShiftSettingByEmployees { get; set; }
        public virtual DbSet<CF_ShiftSettingByPosition> CfShiftSettingByPositions { get; set; }
        public virtual DbSet<CF_Shop> CF_Shops { get; set; }
        public virtual DbSet<CF_ShopIpsetting> CfShopIpsettings { get; set; }
        public virtual DbSet<CF_ShopPrintSetting> CfShopPrintSettings { get; set; }
        public virtual DbSet<CF_ShopSetting> CfShopSettings { get; set; }
        public virtual DbSet<CF_TableOrder> CfTableOrders { get; set; }
        public virtual DbSet<CF_Unit> CfUnits { get; set; }
        public virtual DbSet<CF_Vendor> CfVendors { get; set; }
        public virtual DbSet<CF_VendorGroup> CfVendorGroups { get; set; }
        public virtual DbSet<ClAnalysis> ClAnalyses { get; set; }
        public virtual DbSet<ClAnalysisGroup> ClAnalysisGroups { get; set; }
        public virtual DbSet<ClMedicalList> ClMedicalLists { get; set; }
        public virtual DbSet<ClMedicine> ClMedicines { get; set; }
        public virtual DbSet<ClMedicineType> ClMedicineTypes { get; set; }
        public virtual DbSet<ClPatient> ClPatients { get; set; }
        public virtual DbSet<ClPrescription> ClPrescriptions { get; set; }
        public virtual DbSet<ClPrescriptionDetail> ClPrescriptionDetails { get; set; }
        public virtual DbSet<ClUnit> ClUnits { get; set; }
        public virtual DbSet<SYS_VersionConfig> SYS_VersionConfigs { get; set; }
        public virtual DbSet<Sys_RoleGroup> Sys_RoleGroups { get; set; }
        public virtual DbSet<Sys_Role> Sys_Roles { get; set; }
        public virtual DbSet<Sys_RoleClaim> Sys_RoleClaims { get; set; }
        public virtual DbSet<JM_Team> JM_Teams { get; set; }
        public virtual DbSet<JM_Account> JM_Accounts { get; set; }
        public virtual DbSet<JM_Project> JM_Projects { get; set; }
        public virtual DbSet<JM_ProjectTeam> JM_ProjectTeams { get; set; }
        public virtual DbSet<JM_ProjectMember> JM_ProjectMembers { get; set; }
        public virtual DbSet<JM_ProjectSprint> JM_ProjectSprints { get; set; }
        public virtual DbSet<JM_Template> JM_Templates { get; set; }

    }
}
