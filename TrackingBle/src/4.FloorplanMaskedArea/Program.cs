using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TrackingBle.src._4FloorplanMaskedArea.Data;
using TrackingBle.src._4FloorplanMaskedArea.Services;
using TrackingBle.src._4FloorplanMaskedArea.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Tambahkan layanan ke container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Tambahkan DbContext
builder.Services.AddDbContext<FloorplanMaskedAreaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection")));

// Tambahkan dependensi
builder.Services.AddScoped<IFloorplanMaskedAreaService, FloorplanMaskedAreaService>();
builder.Services.AddAutoMapper(typeof(FloorplanMaskedAreaProfile));

// Tambahkan HttpClient untuk komunikasi dengan layanan lain
builder.Services.AddHttpClient("MstFloorService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstFloorService"]);
});
builder.Services.AddHttpClient("MstFloorplanService", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:MstFloorplanService"]);
});

var app = builder.Build();

// Konfigurasi pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();