﻿using Microsoft.EntityFrameworkCore;
using System;
using BNS.Data.Entities;
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
                options.UseSqlServer("Data Source=DESKTOP-096QE0I\\MSSQLSERVER01;Initial Catalog=CACore_NotDelete;User ID=test;Password=123456");
            }
        }
        public virtual DbSet<T> Repository<T>() where T : class
        {
            return Set<T>();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("Sys_UserClaim");
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("Sys_UserRole").HasKey(s => new
            {
                s.UserId,
                s.RoleId
            });
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("Sys_UserLogin").HasKey(s => s.UserId);
            //modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("Sys_RoleClaim");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("Sys_UserToken").HasKey(s => s.UserId);



            modelBuilder.Entity<JM_AccountCompany>(entity =>
            {
                entity.HasOne(s => s.JM_Company).WithMany(s => s.JM_AccountCompanys).HasForeignKey(s => s.CompanyId);
                entity.HasOne(s => s.Account).WithMany(s => s.AccountCompanys).HasForeignKey(s => s.UserId);

            });


            modelBuilder.Entity<JM_Project>().HasMany(x => x.JM_ProjectTeams);
            modelBuilder.Entity<JM_Project>().HasMany(x => x.JM_ProjectMembers);
            modelBuilder.Entity<JM_Project>().HasMany(x => x.JM_Sprints);
            modelBuilder.Entity<JM_Project>().HasMany(x => x.JM_Issues);
            modelBuilder.Entity<JM_Sprint>().HasMany(x => x.JM_Issues);

            modelBuilder.Entity<JM_Template>().HasMany(x => x.TemplateStatus);

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


            modelBuilder.Entity<Sys_RoleClaim>(entity =>
            {
                entity.HasKey(e => e.Index);

                entity.ToTable("Sys_RoleClaim");
            });

            

        }

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
        public virtual DbSet<JM_Sprint> JM_Sprints { get; set; }
        public virtual DbSet<JM_Task> JM_Tasks { get; set; }
        public virtual DbSet<JM_Company> JM_Companys { get; set; }
        public virtual DbSet<JM_AccountCompany> JM_AccountCompanys { get; set; }
        public virtual DbSet<SYS_FilterConfig> SYS_FilterConfigs { get; set; }
        public virtual DbSet<JM_TemplateStatus> JM_TemplateStatus { get; set; }
        public virtual DbSet<JM_Status> JM_Status { get; set; }
        public virtual DbSet<JM_TaskType> JM_TaskTypies { get; set; }
        public virtual DbSet<JM_CustomColumn> JM_CustomColumns { get; set; }
        public virtual DbSet<JM_TemplateDetail> JM_TemplateDetails { get; set; }
        public virtual DbSet<JM_TaskCustomColumnValue> JM_TaskCustomColumns { get; set; }
        public virtual DbSet<JM_TaskUser> JM_TaskUsers { get; set; }
    }
}
