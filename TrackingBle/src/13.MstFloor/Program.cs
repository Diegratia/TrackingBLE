using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._13MstFloor.Data;
using TrackingBle.src._13MstFloor.Services;
using TrackingBle.src._13MstFloor.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MstFloorDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection")));

builder.Services.AddScoped<IMstFloorService, MstFloorService>();
builder.Services.AddAutoMapper(typeof(MstFloorProfile));

builder.Services.AddHttpClient("MstFloorService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstFloorService"]);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();