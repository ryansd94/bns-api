using BNS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Configurations
{
    public class CF_AccountConfig : IEntityTypeConfiguration<CF_Account>
    {
        public void Configure(EntityTypeBuilder<CF_Account> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("CF_Account");


            //builder.Property(e => e.Password).IsRequired().HasMaxLength(50);

            builder.Property(e => e.SecurityAnswer).HasMaxLength(50);

            builder.Property(e => e.ShopCode)
                    .HasMaxLength(200)
                    .IsUnicode(false);

            builder.Property(e => e.UpdatedDate).HasColumnType("datetime");

            builder.Property(e => e.UpdatedUser).HasMaxLength(50);

            builder.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

        }
    }
}
