using BNS.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BNS.Data.Configurations
{
    public class Sys_RoleConfig: IEntityTypeConfiguration<Sys_Role>
    {
        public void Configure(EntityTypeBuilder<Sys_Role> builder)
        {
            builder.ToTable("Sys_Role");
        }
    }
}
