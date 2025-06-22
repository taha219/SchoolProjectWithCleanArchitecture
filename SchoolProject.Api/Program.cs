using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SchoolProject.Core;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helpers;
using SchoolProject.Infrastructure;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Services;
using TestRESTAPI.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AcceptLanguageHeaderOperationFilter>();
});
builder.Services.AddSwaggerGenJwtAuth();
builder.Services.AddCustomJwtAuth(builder.Configuration);
builder.Services.AddScoped<SoftDeleteInterceptor>();

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var interceptor = serviceProvider.GetRequiredService<SoftDeleteInterceptor>();

    options.UseSqlServer(builder.Configuration.GetConnectionString("mycon"))
           .AddInterceptors(interceptor);
});


// Configure Identity
// note=> add identity register usermanger + role manager + sign in manager but if you use identity core it will not include sign in manager 
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();



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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My School API v1");
        c.RoutePrefix = string.Empty;
    });
}

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
