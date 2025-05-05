using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrastructure.Abstract;
using SchoolProject.Infrastructure.Concrete;
using SchoolProject.Infrastructure.Data;
using SchoolProject.Infrastructure;
using SchoolProject.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));

#region Dependency Injection

builder.Services.AddInfrastructureDependencies()
                .AddServicesDependencies();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
