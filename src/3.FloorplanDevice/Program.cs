using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TrackingBle.src._3FloorplanDevice.Data;
using TrackingBle.src._3FloorplanDevice.Services;
using TrackingBle.src._3FloorplanDevice.MappingProfiles;
using DotNetEnv;

DotNetEnv.Env.Load("/app/.env");

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor(); 
builder.Services.AddTransient<HttpClientAuthorizationDelegatingHandler>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddDbContext<FloorplanDeviceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection") ??
                         "Server=192.168.1.116,1433;Database=TrackingBleDevV3;User Id=sa;Password=Password_123#;TrustServerCertificate=True"));

builder.Services.AddScoped<IFloorplanDeviceService, FloorplanDeviceService>();
builder.Services.AddAutoMapper(typeof(FloorplanDeviceProfile));


builder.Services.AddHttpClient("MstFloorplanService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstFloorplanService"] ?? "http://localhost:5014");
});

builder.Services.AddHttpClient("MstAccessCctvService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstAccessCctvService"] ?? "http://localhost:5005");
});

builder.Services.AddHttpClient("MstBleReaderService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstBleReaderService"] ?? "http://localhost:5008");
});

builder.Services.AddHttpClient("MstAccessControlService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstAccessControlService"] ?? "http://localhost:5006");
});

builder.Services.AddHttpClient("FloorplanMaskedAreaService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:FloorplanMaskedAreaService"] ?? "http://localhost:5004");
});

builder.Services.AddHttpClient("MstApplicationService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstApplicationService"] ?? "http://localhost:5007");
});

var port = Environment.GetEnvironmentVariable("FLOORPLAN_DEVICE_PORT") ??
           builder.Configuration["Ports:FloorplanDeviceService"] ?? "5003";
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var host = env == "Production" ? "0.0.0.0" : "localhost";
builder.WebHost.UseUrls($"http://{host}:{port}");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FloorplanDevice API V1");
        c.RoutePrefix = "";
    });
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.MapGet("/api/FloorplanDevice/health", () => "Hello from FloorplanDevice!");
// app.MapGet("/", () => "Hello from FloorplanDevice!");

Console.WriteLine("Environment Variables Check");
Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {env}");
Console.WriteLine($"FLOORPLAN_DEVICE_PORT: {Environment.GetEnvironmentVariable("FLOORPLAN_DEVICE_PORT")}");
Console.WriteLine($"Configured Port: {port}");
Console.WriteLine($"Host: {host}");
Console.WriteLine($"Application URL: http://{host}:{port}");
Console.WriteLine($"Current Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");
Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("TrackingBleDbConnection")}");
Console.WriteLine($"MstFloorplanService URL: {builder.Configuration["ServiceUrls:MstFloorplanService"]}");
Console.WriteLine($"MstAccessCctvService URL: {builder.Configuration["ServiceUrls:MstAccessCctvService"]}");
Console.WriteLine($"MstBleReaderService URL: {builder.Configuration["ServiceUrls:MstBleReaderService"]}");
Console.WriteLine($"MstAccessControlService URL: {builder.Configuration["ServiceUrls:MstAccessControlService"]}");
Console.WriteLine($"FloorplanMaskedAreaService URL: {builder.Configuration["ServiceUrls:FloorplanMaskedAreaService"]}");
Console.WriteLine($"MstApplicationService URL: {builder.Configuration["ServiceUrls:MstApplicationService"]}");
Console.WriteLine("==================================");
Console.WriteLine($"Starting on http://{host}:{port} in {env} environment...");

app.Run();