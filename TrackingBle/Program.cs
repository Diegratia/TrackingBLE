using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using TrackingBle.src._4FloorplanMaskedArea;
using TrackingBle.src._13MstFloor;
using TrackingBle.src._14MstFloorplan;

var builder = WebApplication.CreateBuilder(args);

// Load konfigurasi
builder.Configuration
    .AddJsonFile("appsettings.Development.json", optional: false)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// Ambil port dari konfigurasi
var floorplanMaskedAreaPort = builder.Configuration["Ports:FloorplanMaskedAreaService"] ?? "5000";
var mstFloorPort = builder.Configuration["Ports:MstFloorService"] ?? "5013";
var mstFloorplanPort = builder.Configuration["Ports:MstFloorplanService"] ?? "5014";

// Jalankan setiap layanan dalam task terpisah
var tasks = new List<Task>
{
    Task.Run(() => FloorplanMaskedAreaProgram.Run(builder.Configuration, floorplanMaskedAreaPort)),
    Task.Run(() => MstFloorProgram.Run(builder.Configuration, mstFloorPort)),
    Task.Run(() => MstFloorplanProgram.Run(builder.Configuration, mstFloorplanPort))
};

// Tunggu semua layanan selesai (akan berjalan sampai dihentikan)
await Task.WhenAll(tasks);