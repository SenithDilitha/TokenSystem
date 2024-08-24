using Microsoft.EntityFrameworkCore;
using TokenIssuanceService.Application.Interfaces;
using TokenIssuanceService.Infrastructure.Data;
using TokenIssuanceService.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITokenIssuanceService, TokenIssuanceService.Application.Services.TokenIssuanceService>();
builder.Services.AddTransient<ITokenRepository, TokenRepository>();

var connectionString = builder.Configuration.GetConnectionString("TokenDbConnectionString");
builder.Services.AddDbContext<TokenDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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