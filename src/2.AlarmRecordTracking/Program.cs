using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using TrackingBle.src._2AlarmRecordTracking.Data;
using TrackingBle.src._2AlarmRecordTracking.Services;
using TrackingBle.src._2AlarmRecordTracking.MappingProfiles;
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

builder.Services.AddDbContext<AlarmRecordTrackingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection") ??
                         "Server=192.168.68.175,1433;Database=TrackingBleDevV3;User Id=sa;Password=Password_123#;TrustServerCertificate=True"));

builder.Services.AddScoped<IAlarmRecordTrackingService, AlarmRecordTrackingService>();
builder.Services.AddAutoMapper(typeof(AlarmRecordTrackingProfile));

builder.Services.AddHttpClient("VisitorService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:VisitorService"] ?? "http://localhost:5019");
}).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

builder.Services.AddHttpClient("MstBleReaderService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstBleReaderService"] ?? "http://localhost:5008");
}).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

builder.Services.AddHttpClient("FloorplanMaskedAreaService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:FloorplanMaskedAreaService"] ?? "http://localhost:5004");
}).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

builder.Services.AddHttpClient("MstApplicationService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstApplicationService"] ?? "http://localhost:5007");
}).AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

var port = Environment.GetEnvironmentVariable("ALARM_RECORD_TRACKING_PORT") ??
           builder.Configuration["Ports:AlarmRecordTrackingService"] ?? "5002";
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
var host = env == "Production" ? "0.0.0.0" : "localhost";
builder.WebHost.UseUrls($"http://{host}:{port}");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlarmRecordTracking API V1");
        c.RoutePrefix = "";
    });
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// app.MapGet("/", () => "Hello from AlarmRecordTrackings");
app.MapGet("/api/AlarmRecordTracking/health", () => "Hello from AlarmRecordTrackings");

Console.WriteLine("Environment Variables Check");
Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {env}");
Console.WriteLine($"ALARM_RECORD_TRACKING_PORT: {Environment.GetEnvironmentVariable("ALARM_RECORD_TRACKING_PORT")}");
Console.WriteLine($"Configured Port: {port}");
Console.WriteLine($"Host: {host}");
Console.WriteLine($"Application URL: http://{host}:{port}");
Console.WriteLine($"Current Environment: {app.Environment.EnvironmentName}");
Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");
Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("TrackingBleDbConnection")}");
Console.WriteLine($"VisitorService URL: {builder.Configuration["ServiceUrls:VisitorService"]}");
Console.WriteLine($"MstBleReaderService URL: {builder.Configuration["ServiceUrls:MstBleReaderService"]}");
Console.WriteLine($"FloorplanMaskedAreaService URL: {builder.Configuration["ServiceUrls:FloorplanMaskedAreaService"]}");
Console.WriteLine($"MstApplicationService URL: {builder.Configuration["ServiceUrls:MstApplicationService"]}");
Console.WriteLine("==================================");
Console.WriteLine($"Starting on http://{host}:{port} in {env} environment...");

app.Run();