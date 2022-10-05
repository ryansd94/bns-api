﻿using BNS.Data.Entities;
using BNS.Data.EntityContext;
using BNS.Utilities.Implement;
using BNS.Utilities.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using BNS.Domain;
using BNS.Service.Implement;
using System.Reflection;
using BNS.Service.Features;
using BNS.Domain.Commands;
using BNS.Data.Entities.JM_Entities;

namespace BNS.Api
{
  public static  class RegisterDI
    {
        public static void AddRepository(this IServiceCollection services)
        {
            //services.AddMediatR(typeof(SendMailAddJM_UserCommand));
            services.AddMediatR(typeof(CreateTeamCommand).GetTypeInfo().Assembly);
            services.AddIdentity<JM_Account, Sys_Role>().AddEntityFrameworkStores<BNSDbContext>()
               .AddDefaultTokenProviders();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ICacheData, CacheData>();
            services.AddSingleton<ICaptcha, Captcha>();
            services.AddSingleton<IMemoryCache, MemoryCache>();



            services.AddTransient<RoleManager<Sys_Role>, RoleManager<Sys_Role>>();


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICipherService, CipherService>();
            services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            //services.AddIdentity<CF_Account, Sys_Role>(ops =>
            //{
            //    //--- other code
            //    ops.Password.RequireDigit = false;
            //    ops.Password.RequireUppercase = false;
            //});
        }
    }
}
