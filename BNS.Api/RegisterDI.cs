using BNS.Data.Entities;
using BNS.Data.Entities.JM_Entities;
using BNS.Data.EntityContext;
using BNS.Domain;
using BNS.Domain.Commands;
using BNS.Domain.Interface;
using BNS.Domain.Queries;
using BNS.Domain.Responses;
using BNS.Service.Features;
using BNS.Service.Hubs;
using BNS.Service.Implement;
using BNS.Service.Notify;
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
        public static void AddRepository(this IServiceCollection services, MyConfiguration appSettings)
        {
            services.AddMediatR(typeof(GetTaskQuery));
            services.AddMediatR(typeof(IRequestHandler<>));
            services.AddMediatR(typeof(IRequestHandler<,>));
            services.AddMediatR(typeof(IRequestHandler<SendMailAddUserRequest, ApiResult<Guid>>));

            services.AddIdentity<JM_Account, Sys_Role>().AddEntityFrameworkStores<BNSDbContext>()
               .AddDefaultTokenProviders();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ICaptcha, Captcha>();
            services.AddSingleton<IMemoryCache, MemoryCache>();
            services.AddTransient<RoleManager<Sys_Role>, RoleManager<Sys_Role>>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAttachedFileService, AttachedFileService>();
            services.AddScoped<ICipherService, CipherService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<INotifyService, NotifyService>();
            services.AddScoped<INotifyGateway, NotifyGateway>();
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IRequestHandler<GetTaskRequest, ApiResultList<TaskItem>>, GetTaskQuery>();
            
            services.AddScoped<IRequestHandler<UpdateProjectRequest, ApiResult<Guid>>, UpdateProjectCommand>();
            services.AddScoped<IRequestHandler<UpdateTaskTypeRequest, ApiResult<Guid>>, UpdateTaskTypeCommand>();
            services.AddScoped<IRequestHandler<UpdateStatusRequest, ApiResult<Guid>>, UpdateStatusCommand>();
            services.AddScoped<IRequestHandler<UpdateTagRequest, ApiResult<Guid>>, UpdateTagCommand>();
            services.AddScoped<IRequestHandler<UpdatePriorityRequest, ApiResult<Guid>>, UpdatePriorityCommand>();
            services.AddScoped<IRequestHandler<ReadNotifyRequest, ApiResult<Guid>>, ReadNotifyCommand>();

            services.AddScoped<IRequestHandler<DeletePriorityRequest, ApiResult<Guid>>, DeletePriorityCommand>();
            services.AddScoped<IRequestHandler<GetProjectByUserIdRequest, ApiResultList<ProjectResponseItem>>, GetProjectByUserIdQuery>();
            services.AddScoped<IRequestHandler<GetProjectByIdRequest, ApiResult<ProjectResponseItem>>, GetProjectByIdQuery>();
            services.AddScoped<IRequestHandler<GetViewPermissionRequest, ApiResultList<ViewPermissionResponseItem>>, GetViewPermissionQuery>();
            services.AddScoped<IRequestHandler<GetViewPermissionByIdRequest, ApiResult<ViewPermissionByIdResponse>>, GetViewPermissionByIdQuery>();
            services.AddTransient<BNSDbContext>();
            services.AddDbContext<BNSDbContext>();
            services.AddScoped(typeof(IConnectionMapping<>), typeof(ConnectionMapping<>));
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddStackExchangeRedisCache(options =>
               {
                   options.Configuration = appSettings.ConnectionStrings.redisConnection;
                   //options.InstanceName = "your_redis_instance_name"; // Tùy chọn
               });
            //services.AddIdentity<CF_Account, Sys_Role>(ops =>
            //{
            //    //--- other code
            //    ops.Password.RequireDigit = false;
            //    ops.Password.RequireUppercase = false;
            //});
        }
    }
}
