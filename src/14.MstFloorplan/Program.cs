using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._14MstFloorplan.Data;
using TrackingBle.src._14MstFloorplan.Services;
using TrackingBle.src._14MstFloorplan.MappingProfiles;
using DotNetEnv;

// Load file .env jika ada (opsional, sesuaikan path-nya)
if (File.Exists("../../.env"))
{
    DotNetEnv.Env.Load("../../.env");
    Console.WriteLine(".env file loaded successfully");
}
else
{
    Console.WriteLine("Warning: .env file not found at ../.env");
}

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MstFloorplanDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection")));

builder.Services.AddScoped<IMstFloorplanService, MstFloorplanService>();
builder.Services.AddAutoMapper(typeof(MstFloorplanProfile));

builder.Services.AddHttpClient("MstFloorService", client =>
{
    string serviceUrl = builder.Configuration["ServiceUrls:MstFloorService"] ?? "http://localhost:5013";
    client.BaseAddress = new Uri(serviceUrl);
}).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
});

var port = Environment.GetEnvironmentVariable("MST_FLOORPLAN_PORT") ?? builder.Configuration["Ports:MstFloorplanService"] ?? "5014";
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var host = env == "Production" ? "0.0.0.0" : "localhost";
builder.WebHost.UseUrls($"http://{host}:{port}");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MstFloorplan API V1");
        c.RoutePrefix = "";
    });
}


app.UseAuthorization();
app.MapControllers();

app.MapGet("/api/MstFloorplan/health", () => "Hello from MstFloorplan!");
app.MapGet("/", () => "Hello from MstFloorplan!");

Console.WriteLine("=== Environment Variables Check ===");
Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {env}");
Console.WriteLine($"MST_FLOORPLAN_PORT: {Environment.GetEnvironmentVariable("MST_FLOORPLAN_PORT")}");
Console.WriteLine($"Configured Port: {port}");
Console.WriteLine($"Host: {host}");
Console.WriteLine($"Application URL: http://{host}:{port}");
Console.WriteLine($"Current Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");
Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("TrackingBleDbConnection")}");
Console.WriteLine($"MstFloorService URL: {builder.Configuration["ServiceUrls:MstFloorService"] ?? "http://localhost:5013"}");
Console.WriteLine("==================================");

app.Run();