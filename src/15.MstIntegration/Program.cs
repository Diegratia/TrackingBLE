using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._15MstIntegration.Data;
using TrackingBle.src._15MstIntegration.Services;
using TrackingBle.src._15MstIntegration.MappingProfiles;
using DotNetEnv;
try
{
    DotNetEnv.Env.Load("../../.env");
    Console.WriteLine("Successfully loaded .env file from ../../.env");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to load .env file: {ex.Message}");
}

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MstIntegrationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection") ??
                         "Server=192.168.1.116,1433;Database=TrackingBleDevV3;User Id=sa;Password=Password_123#;TrustServerCertificate=True"));

builder.Services.AddScoped<IMstIntegrationService, MstIntegrationService>();
builder.Services.AddAutoMapper(typeof(MstIntegrationProfile));

builder.Services.AddHttpClient("MstBrandService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstBrandService"]);
});

builder.Services.AddHttpClient("MstApplicationService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstApplicationService"]);
});

var port = Environment.GetEnvironmentVariable("MST_INTEGRATION_PORT") ??
           builder.Configuration["Ports:MstIntegrationService"] ?? "5015";
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var host = env == "Production" ? "0.0.0.0" : "localhost";
builder.WebHost.UseUrls($"http://{host}:{port}");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MstIntegration API V1");
        c.RoutePrefix = "";
    });
}


app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello from MstIntegration!");
app.MapGet("/api/MstIntegration/health", () => "Hello from MstIntegration!");

Console.WriteLine("Environment Variables Check");
Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {env}");
Console.WriteLine($"MST_INTEGRATION_PORT: {Environment.GetEnvironmentVariable("MST_INTEGRATION_PORT")}");
Console.WriteLine($"Configured Port: {port}");
Console.WriteLine($"Host: {host}");
Console.WriteLine($"Application URL: http://{host}:{port}");
Console.WriteLine($"Current Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");
Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("TrackingBleDbConnection")}");
Console.WriteLine($"MstBrandService URL: {builder.Configuration["ServiceUrls:MstBrandService"]}");
Console.WriteLine($"MstApplicationService URL: {builder.Configuration["ServiceUrls:MstApplicationService"]}");
Console.WriteLine("==================================");
Console.WriteLine($"Starting on http://{host}:{port} in {env} environment...");

app.Run();