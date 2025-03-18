using Microsoft.EntityFrameworkCore;
using TrackingBle.src._4FloorplanMaskedArea.Data;
using TrackingBle.src._4FloorplanMaskedArea.Services;
using AutoMapper;
using TrackingBle.src._4FloorplanMaskedArea.MappingProfiles;
using Microsoft.OpenApi.Models; // Untuk Swagger

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.Development.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Add DbContext
builder.Services.AddDbContext<FloorplanMaskedAreaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection")));

// Add HttpClient for inter-service communication
builder.Services.AddHttpClient<IFloorplanMaskedAreaService, FloorplanMaskedAreaService>();

// Add services
builder.Services.AddScoped<IFloorplanMaskedAreaService, FloorplanMaskedAreaService>();
builder.Services.AddAutoMapper(typeof(FloorplanMaskedAreaProfile));
builder.Services.AddControllers();

// Tambah Swagger
builder.Services.AddEndpointsApiExplorer(); // Untuk mengumpulkan endpoint
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "FloorplanMaskedArea API", 
        Version = "v1",
        Description = "API for managing Floorplan Masked Areas"
    });
});

var app = builder.Build();

// Apply migrations at startup (opsional, uncomment jika perlu)
// using (var scope = app.Services.CreateScope())
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<FloorplanMaskedAreaDbContext>();
//     dbContext.Database.Migrate();
// }

// Tambah middleware Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FloorplanMaskedArea API v1");
    c.RoutePrefix = string.Empty; // Akses Swagger di root (http://localhost:5004/)
});

app.UseRouting();
app.MapControllers();
app.Run();