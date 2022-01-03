using BNS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Configurations
{
    public class SYS_ShopConfig : IEntityTypeConfiguration<SYS_Shop>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SYS_Shop> builder)
        {

            builder.ToTable("SYS_Shop");
            builder.HasKey(s => s.Index);
            builder.Property(s => s.Name).IsRequired();
            builder.Property(s => s.Code).IsRequired().IsUnicode(false).HasMaxLength(50);
        }
    }
}
