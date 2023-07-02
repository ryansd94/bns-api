using BNS.Domain;
using BNS.Domain.Interface;
using BNS.Notify;
using BNS.Service.Hubs;
using BNS.Service.Implement;
using BNS.Service.Notify;
using BNS.Utilities.Implement;
using BNS.Utilities.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var a = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appSettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
    .AddEnvironmentVariables();

var configuration = a.Build();
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MyConfiguration>(configuration.GetSection("DefaultConfig"));
var appSettingsSection = configuration.GetSection("DefaultConfig");
var appSettings = appSettingsSection.Get<MyConfiguration>();

string issuer = appSettings.Tokens.Issuer;
string signingKey = appSettings.Tokens.Key;
byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
         {
             builder
                 .AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials()
                 .WithOrigins("http://localhost:3000", "http://103.121.89.96:8991", "http://103.121.89.96:8990");
         });
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
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


builder.Services.AddAuthentication(opt =>
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
builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");

builder.Services.AddScoped<INotifytHub, NotifyHub>();
builder.Services.AddScoped<INotifyGateway, NotifyGateway>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddScoped(typeof(IConnectionMapping<>), typeof(ConnectionMapping<>));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = appSettings.ConnectionStrings.redisConnection;
});
builder.Services.AddSignalR();
var app = builder.Build();

app.UseCors("AllowAll");
app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.UseMiddleware<NotifyMiddleware>();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<NotifyHub>("/notify");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "api/{controller=Home}/{action=Index}/{id?}");

});
app.Run();
