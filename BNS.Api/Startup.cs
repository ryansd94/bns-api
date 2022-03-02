using AspNetCoreRateLimit;
using AutoMapper;
using BNS.Api.AutoMapper;
using BNS.Service.Extensions;
using BNS.Service.Middleware;
using BNS.Service.Subcriber;
using BNS.Data.EntityContext;
using BNS.Infrastructure.Messaging;
using BNS.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BNS.Api
{
    public class Startup
    {
        public Startup(Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //// Configure Config

            services.Configure<MyConfiguration>(Configuration.GetSection("DefaultConfig"));
            var appSettingsSection = Configuration.GetSection("DefaultConfig");
            var appSettings = appSettingsSection.Get<MyConfiguration>();

            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            //var appSettings = new MyConfiguration();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("AllowAll",
            //        builder =>
            //        {
            //            builder
            //            .AllowAnyOrigin()
            //            .AllowAnyMethod()
            //            .AllowAnyHeader()
            //            .AllowCredentials();
            //        });
            //});
            services.AddDbContext<BNSDbContext>(
            options => options.UseSqlServer(appSettings.ConnectionStrings.bnsConnection)
            );
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            //services.AddDbContext<BNSDbContext>((serviceProvider, dbContextBuilder) =>
            //{
            //    var connectionStringPlaceHolder = appSettings.ConnectionStrings.bnsConnection;
            //    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
            //    var dbName = httpContextAccessor.HttpContext.Request.Headers["tenantId"].First();
            //    var connectionString = connectionStringPlaceHolder.Replace("{dbName}", dbName);
            //    dbContextBuilder.UseSqlServer(connectionString);
            //});

            services.AddDataProtection();

            services.AddRepository();
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var cultures = new[]
                    {
                        new CultureInfo("vi-VN"),
                        new CultureInfo("en-EN"),
                    };
                    options.DefaultRequestCulture = new RequestCulture(cultures[0], cultures[0]);
                    options.SupportedCultures = cultures;
                    options.SupportedUICultures = cultures;
                });

            services.AddLocalization(opts => opts.ResourcesPath = "Resources");
            services.AddMvc()
            .AddViewLocalization(
              LanguageViewLocationExpanderFormat.Suffix,
              opts => opts.ResourcesPath = "Resources")
         .AddDataAnnotationsLocalization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "BNS Api",
                    Version = "v1"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
            });

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);



            string issuer = appSettings.Tokens.Issuer;
            string signingKey = appSettings.Tokens.Key;
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });


            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            // Services
            services.AddTransient<MyConfiguration>();
            services.AddElasticsearch(appSettings);

            services.AddRabbitMq(Configuration);
            //services.AddGraphQLServer();


            #region Middleware
            services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();
            #endregion            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
            var supportedCultures = new List<CultureInfo>
            {
                   new CultureInfo("vi-VN"),
                   new CultureInfo("sv-SE"),
                   new CultureInfo("da-DK")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("vi-VN"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BNSDbContext>();
                context.Database.Migrate();
            }
            app.UseStaticFiles();

            // global error handler
            app.UseMiddleware<RequestCultureMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            //app.Use(async (context, nextMiddleware) =>
            //{

            //    await nextMiddleware.Invoke();
            //    //await context.Response.WriteAsync("<div>this is return Middleware 1</div>");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute(
                //   name: "default2",
                //   pattern: "api/{name?}/{controller=Home}/{action=Index}/{id?}");
            });

        }

        #region Middleware Filter
        public class RequestSetOptionsStartupFilter : IStartupFilter
        {
            public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            {
                return builder =>
                {
                    builder.UseStaticFiles();
                    builder.UseIpRateLimiting();

                    builder.UseRabbitMq().SubscribeEvent<CreateJM_TeamSubcriberMQ>();
                    builder.UseRabbitMq().SubscribeEvent<SendMailSubcriberMQ>();

                    next(builder);
                };
            }
        }
        #endregion
    }
}
