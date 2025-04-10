using Microsoft.EntityFrameworkCore;
using TrackingBle.src._3FloorplanDevice.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FloorplanDeviceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleConnectionString")));

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();
