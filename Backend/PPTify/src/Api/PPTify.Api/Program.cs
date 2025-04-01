using PPTify.Infrastructure.Authentication.Jwt;
using PPTify.Infrastructure;
using PPTify.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Db");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var serverVersion = MySqlServerVersion.AutoDetect(connectionString);

    options.UseMySql(connectionString, serverVersion);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<PPTify_Token_Generator>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", builder => {
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
