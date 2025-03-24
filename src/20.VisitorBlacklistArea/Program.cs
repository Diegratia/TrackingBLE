using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._20VisitorBlacklistArea.Data;
using TrackingBle.src._20VisitorBlacklistArea.Services;
using TrackingBle.src._20VisitorBlacklistArea.MappingProfiles;
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

builder.Services.AddDbContext<VisitorBlacklistAreaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection") ??
                         "Server=192.168.1.116,1433;Database=TrackingBleDevV3;User Id=sa;Password=Password_123#;TrustServerCertificate=True"));

builder.Services.AddScoped<IVisitorBlacklistAreaService, VisitorBlacklistAreaService>();
builder.Services.AddAutoMapper(typeof(VisitorBlacklistAreaProfile));


builder.Services.AddHttpClient("FloorplanMaskedAreaService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:FloorplanMaskedAreaService"] ?? "http://localhost:5004");
});

builder.Services.AddHttpClient("VisitorService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:VisitorService"] ?? "http://localhost:5019");
});

var port = Environment.GetEnvironmentVariable("VISITOR_BLACKLIST_AREA_PORT") ??
           builder.Configuration["Ports:VisitorBlacklistAreaService"] ?? "5020";
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var host = env == "Production" ? "0.0.0.0" : "localhost";
builder.WebHost.UseUrls($"http://{host}:{port}");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VisitorBlacklistArea API V1");
        c.RoutePrefix = "";
    });
}

app.UseAuthorization();
app.MapControllers();


app.MapGet("/api/VisitorBlacklistArea/health", () => "Health Check");
app.MapGet("/", () => "Health Check");

Console.WriteLine("Environment Variables Check");
Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {env}");
Console.WriteLine($"VISITOR_BLACKLIST_AREA_PORT: {Environment.GetEnvironmentVariable("VISITOR_BLACKLIST_AREA_PORT")}");
Console.WriteLine($"Configured Port: {port}");
Console.WriteLine($"Host: {host}");
Console.WriteLine($"Application URL: http://{host}:{port}");
Console.WriteLine($"Current Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");
Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("TrackingBleDbConnection")}");
Console.WriteLine($"FloorplanMaskedAreaService URL: {builder.Configuration["ServiceUrls:FloorplanMaskedAreaService"]}");
Console.WriteLine($"VisitorService URL: {builder.Configuration["ServiceUrls:VisitorService"]}");
Console.WriteLine("==================================");
Console.WriteLine($"Starting on http://{host}:{port} in {env} environment...");

app.Run();