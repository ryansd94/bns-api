using BNS.Utilities.Constant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BNS.Data.EntityContext
{
    public class BNSContextFactory : IDesignTimeDbContextFactory<BNSDbContext>
    {
        public BNSDbContext CreateDbContext(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString(Constants.AppSettings.MainConnectionString);

            var optionsBuilder = new DbContextOptionsBuilder<BNSDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new BNSDbContext(optionsBuilder.Options);
        }
    }
}
