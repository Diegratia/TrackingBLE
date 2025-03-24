using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._16MstMember.Data;
using TrackingBle.src._16MstMember.Services;
using TrackingBle.src._16MstMember.MappingProfiles;
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

builder.Services.AddDbContext<MstMemberDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection") ??
                         "Server=192.168.1.116,1433;Database=TrackingBleDevV3;User Id=sa;Password=Password_123#;TrustServerCertificate=True"));

builder.Services.AddScoped<IMstMemberService, MstMemberService>();
builder.Services.AddAutoMapper(typeof(MstMemberProfile));

builder.Services.AddHttpClient("MstApplicationService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstApplicationService"] ?? "http://localhost:5007");
});
builder.Services.AddHttpClient("MstOrganizationService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstOrganizationService"] ?? "http://localhost:5017");
});
builder.Services.AddHttpClient("MstDepartmentService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstDepartmentService"] ?? "http://localhost:5011");
});
builder.Services.AddHttpClient("MstDistrictService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstDistrictService"] ?? "http://localhost:5012");
});

var port = Environment.GetEnvironmentVariable("MST_MEMBER_PORT") ??
           builder.Configuration["Ports:MstMemberService"] ?? "5016";
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var host = env == "Production" ? "0.0.0.0" : "localhost";
builder.WebHost.UseUrls($"http://{host}:{port}");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MstMember API V1");
        c.RoutePrefix = "";
    });
}


app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Hello from MstMember!");

Console.WriteLine("Environment Variables Check");
Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {env}");
Console.WriteLine($"MST_MEMBER_PORT: {Environment.GetEnvironmentVariable("MST_MEMBER_PORT")}");
Console.WriteLine($"Configured Port: {port}");
Console.WriteLine($"Host: {host}");
Console.WriteLine($"Application URL: http://{host}:{port}");
Console.WriteLine($"Current Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");
Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("TrackingBleDbConnection")}");
Console.WriteLine($"MstApplicationService URL: {builder.Configuration["ServiceUrls:MstApplicationService"]}");
Console.WriteLine($"MstOrganizationService URL: {builder.Configuration["ServiceUrls:MstOrganizationService"]}");
Console.WriteLine($"MstDepartmentService URL: {builder.Configuration["ServiceUrls:MstDepartmentService"]}");
Console.WriteLine($"MstDistrictService URL: {builder.Configuration["ServiceUrls:MstDistrictService"]}");
Console.WriteLine("==================================");
Console.WriteLine($"Starting on http://{host}:{port} in {env} environment...");

app.Run();