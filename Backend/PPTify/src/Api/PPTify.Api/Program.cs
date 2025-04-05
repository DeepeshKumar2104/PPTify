using PPTify.Infrastructure.Authentication.Jwt;
using PPTify.Infrastructure;
using PPTify.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using PPTify.Domain.Interfaces;
using PPTify.Infrastructure.Persistence.Repositories;
using PPTify.Application.Contracts.Interface;
using PPTify.Application.Services;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Db");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var serverVersion = MySqlServerVersion.AutoDetect(connectionString);

    options.UseMySql(connectionString, serverVersion);
});
builder.Services.AddScoped(typeof(IGenricRepository<>),typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitofWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<PPTify_Token_Generator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
