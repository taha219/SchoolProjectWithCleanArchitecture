using System.Collections.Concurrent;
using System.Globalization;
using Hangfire;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProject.Core;
using SchoolProject.Data.Entities.Identity;
using SchoolProject.Data.Helpers;
using SchoolProject.Data.Results;
using SchoolProject.Infrastructure;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure.Seeder;
using SchoolProject.Services;
using SchoolProject.Services.Abstract;
using SchoolProject.Services.Hubs;
using TestRESTAPI.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AcceptLanguageHeaderOperationFilter>();
});

var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("jwtSettings").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddScoped<SoftDeleteInterceptor>();

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetRequiredService<SoftDeleteInterceptor>();

    options.UseSqlServer(builder.Configuration.GetConnectionString("mycon"))
           .AddInterceptors(interceptor);
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();

// Configure Identity
// *Note=> AddIdentity register usermanager + role_manager + signin_manager , but if you use  AddIdentityCore it will not include only signin_manager 
builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

////////////////////////////////////////////////////////////////////////
// notice that you must add these two lines after configuring identity
builder.Services.AddSwaggerGenJwtAuth();
builder.Services.AddCustomJwtAuth(builder.Configuration);
//////////////////////////////////////////////////////////////////////

builder.Services.AddSingleton<ConcurrentDictionary<string, RefreshToken>>();
///////////////////////////////////////////////////////////////////////////
builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();


////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Hangfire configuration service
builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("mycon")));
builder.Services.AddHangfireServer();
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // SignalR needs this
    });
});

#region Dependency Injection
builder.Services.AddInfrastructureDependencies()
                .AddServicesDependencies()
                .AddCoreDependencies();
#endregion

#region Localization
builder.Services.AddControllersWithViews();
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-EG")
    };

    options.DefaultRequestCulture = new RequestCulture("ar-EG");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
#endregion

// seeding roles and admin user if there is no roles or users in the database
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedAsync(roleManager);
    await UserSeeder.SeedAsync(userManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion

app.UseCors();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHangfireDashboard("/Dashboard"); // optional: you can protect it with roles

RecurringJob.AddOrUpdate<IScheduledJobService>(
    "DeleteExpiredOtpsJob",
    job => job.DeleteExpiredOtpsAsync(),
    "0 12 * * 6" // saturday (6)  12:00
);
RecurringJob.AddOrUpdate<IScheduledJobService>(
    "NotifyInactiveUsersJob",
    job => job.NotifyInactiveUsersAsync(),
    "0 16 */3 * *" // every 3 days 4:00 pm
);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
