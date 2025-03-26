using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._5MstAccessCctv.Data;
using TrackingBle.src._5MstAccessCctv.Services;
using TrackingBle.src._5MstAccessCctv.MappingProfiles;
using DotNetEnv;

DotNetEnv.Env.Load("../../.env");

var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod() 
              .AllowAnyHeader(); 
    });
});

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MstAccessCctvDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection") ??
                         "Server=192.168.68.175,1433;Database=TrackingBleDevV3;User Id=sa;Password=Password_123#;TrustServerCertificate=True"));

builder.Services.AddScoped<IMstAccessCctvService, MstAccessCctvService>();
builder.Services.AddAutoMapper(typeof(MstAccessCctvProfile));

builder.Services.AddHttpClient("MstIntegrationService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstIntegrationService"] ?? "http://localhost:5015");
});

builder.Services.AddHttpClient("MstApplicationService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstApplicationService"] ?? "http://localhost:5007");
});

var port = Environment.GetEnvironmentVariable("MST_ACCESS_CCTV_PORT") ??
           builder.Configuration["Ports:MstAccessCctvService"] ?? "5005";
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var host = env == "Production" ? "0.0.0.0" : "localhost";
builder.WebHost.UseUrls($"http://{host}:{port}");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MstAccessCctv API V1");
        c.RoutePrefix = "";
    });
}

app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello from MstAccessCctv!"); 
app.MapGet("/api/MstAccessCctv/health", () => "Hello from MstAccessCctv!");

Console.WriteLine("Environment Variables Check");
Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {env}");
Console.WriteLine($"MST_ACCESS_CCTV_PORT: {Environment.GetEnvironmentVariable("MST_ACCESS_CCTV_PORT")}");
Console.WriteLine($"Configured Port: {port}");
Console.WriteLine($"Host: {host}");
Console.WriteLine($"Application URL: http://{host}:{port}");
Console.WriteLine($"Current Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");
Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("TrackingBleDbConnection")}");
Console.WriteLine($"MstIntegrationService URL: {builder.Configuration["ServiceUrls:MstIntegrationService"]}");
Console.WriteLine($"MstApplicationService URL: {builder.Configuration["ServiceUrls:MstApplicationService"]}");
Console.WriteLine("==================================");
Console.WriteLine($"Starting on http://{host}:{port} in {env} environment...");

app.Run();