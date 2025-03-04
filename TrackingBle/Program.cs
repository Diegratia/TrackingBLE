using Microsoft.EntityFrameworkCore;
using TrackingBle.Data; 
using TrackingBle.MappingProfiles;
using TrackingBle.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Tambahkan AutoMapper ke DI
builder.Services.AddAutoMapper(typeof(MstApplicationProfile));
builder.Services.AddAutoMapper(typeof(MstIntegrationProfile));
builder.Services.AddAutoMapper(typeof(MstAccessCctvProfile));
builder.Services.AddAutoMapper(typeof(MstAccessControlProfile));
builder.Services.AddAutoMapper(typeof(MstAreaProfile));

builder.Services.AddControllers();
// Tambahkan konfigurasi lain seperti DbContext
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddDbContext<TrackingBleDbContext>(options =>
// options.UseSqlServer)

builder.Services.AddDbContext<TrackingBleDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleConnectionString")));

// Register services with their interfaces
builder.Services.AddScoped<IMstAreaService, MstAreaService>();
builder.Services.AddScoped<IMstApplicationService, MstApplicationService>();
builder.Services.AddScoped<IMstIntegrationService, MstIntegrationService>();
builder.Services.AddScoped<IMstAccessCctvService, MstAccessCctvService>();
builder.Services.AddScoped<IMstAccessControlService, MstAccessControlService>();
builder.Services.AddScoped<IMstAreaService, MstAreaService>();
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
