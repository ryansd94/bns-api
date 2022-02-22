using BNS.Application.Implement;
using BNS.Application.Interface;
using BNS.Data.Entities;
using BNS.Data.EntityContext;
using BNS.Utilities.Implement;
using BNS.Utilities.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using GraphQL;
using static BNS.Application.Features.CreateJM_TeamCommand;

namespace BNS.Api
{
  public static  class DependencyInjection
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddIdentity<CF_Account, Sys_Role>().AddEntityFrameworkStores<BNSDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<ICF_AccountService, CF_AccountService>();
            services.AddTransient<ISYS_ControlService, SYS_ControlService>();
            services.AddTransient<ISys_RoleService, Sys_RoleService>();
            services.AddTransient<ISys_RoleClaimService, Sys_RoleClaimService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ICacheData, CacheData>();
            services.AddSingleton<ICaptcha, Captcha>();
            services.AddSingleton<IMemoryCache, MemoryCache>();



            services.AddTransient<UserManager<CF_Account>, UserManager<CF_Account>>();
            services.AddTransient<SignInManager<CF_Account>, SignInManager<CF_Account>>();
            services.AddTransient<RoleManager<Sys_Role>, RoleManager<Sys_Role>>();




            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddMediatR(typeof(CreateTeamCommandHandler));

            services.AddScoped<ICipherService, CipherService>();

            //services.AddIdentity<CF_Account, Sys_Role>(ops =>
            //{
            //    //--- other code
            //    ops.Password.RequireDigit = false;
            //    ops.Password.RequireUppercase = false;
            //});
        }
    }
}
