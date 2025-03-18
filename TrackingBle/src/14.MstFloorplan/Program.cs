using Microsoft.EntityFrameworkCore;
using TrackingBle.src._14MstFloorplan.Data;
using TrackingBle.src._14MstFloorplan.Services;
using AutoMapper;
using TrackingBle.src._14MstFloorplan.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MstFloorplanDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection")));

builder.Services.AddScoped<IMstFloorplanService, MstFloorplanService>();

builder.Services.AddAutoMapper(typeof(MstFloorplanProfile));

builder.Services.AddControllers();

var app = builder.Build();
app.UseRouting();
app.MapControllers();
app.Run();