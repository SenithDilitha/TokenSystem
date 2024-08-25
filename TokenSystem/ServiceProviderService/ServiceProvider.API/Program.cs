using Microsoft.EntityFrameworkCore;
using ServiceProvider.Application.Interfaces;
using ServiceProvider.Application.Services;
using ServiceProvider.Infrastructure.Data;
using ServiceProvider.Infrastructure.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        corsPolicyBuilder =>
        {
            corsPolicyBuilder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddTransient<IServiceProviderService, ServiceProviderService>();

builder.Services.AddHttpClient<ITokenIssuanceClient, TokenIssuanceClient>(client =>
{
    var tokenIssuanceUrl = builder.Configuration["TokenIssuanceServiceUrl"];

    if (string.IsNullOrEmpty(tokenIssuanceUrl))
        throw new InvalidOperationException("The Token Issuance URL is not configured properly.");

    client.BaseAddress = new Uri(tokenIssuanceUrl);
});

var connectionString = builder.Configuration["TokenDbConnectionString"];
builder.Services.AddDbContext<ServiceDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

ApplyMigrations();

app.Run();

void ApplyMigrations()
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ServiceDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while applying the migrations.");
    }
}