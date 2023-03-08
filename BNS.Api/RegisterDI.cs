using BNS.Data.Entities;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Interface;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Service.Features;
using BNS.Service.Implement;
using BNS.Utilities.Implement;
using BNS.Utilities.Interface;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BNS.Api
{
    public static class RegisterDI
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetTaskQuery));

            //services.AddMediatR(typeof(TaskItem));
            //services.AddMediatR(typeof(GetTaskRequest));
            //services.AddMediatR(typeof(GetRequestHandler<,>));
            //services.AddMediatR(typeof(ApiResultList<>));
            services.AddMediatR(typeof(IRequestHandler<>));
            services.AddMediatR(typeof(IRequestHandler<,>));
            services.AddMediatR(typeof(IRequestHandler<SendMailAddUserRequest,ApiResult<Guid>>));

            //services.AddMediatR(typeof(TaskItem).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(GetTaskQuery).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(GetTaskRequest).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(GetRequestHandler<,>).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(ApiResultList<>).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(IRequestHandler<>).GetTypeInfo().Assembly);
            //services.AddMediatR(typeof(IRequestHandler<,>).GetTypeInfo().Assembly);

            services.AddIdentity<JM_Account, Sys_Role>().AddEntityFrameworkStores<BNSDbContext>()
               .AddDefaultTokenProviders();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ICacheData, CacheData>();
            services.AddSingleton<ICaptcha, Captcha>();
            services.AddSingleton<IMemoryCache, MemoryCache>();



            services.AddTransient<RoleManager<Sys_Role>, RoleManager<Sys_Role>>();


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //services.AddScoped(typeof(ICipherService), typeof(CipherService));
            services.AddScoped<IAttachedFileService, AttachedFileService>();
            services.AddScoped<ICipherService, CipherService>();
            services.AddScoped<IRequestHandler<GetTaskRequest, ApiResultList<TaskItem>>, GetTaskQuery>();
            services.AddScoped<IRequestHandler<UpdateTaskTypeRequest, ApiResult<Guid>>, UpdateTaskTypeCommand>();
            services.AddScoped<IRequestHandler<UpdateStatusRequest, ApiResult<Guid>>, UpdateTagCommand>();

            //services.AddScoped(typeof(IRequestHandler<,>), typeof(GetRequestHandler<,>));
            //services.AddMediatR(typeof(IRequestHandler<,>), typeof(GetRequestHandler<,>));

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
