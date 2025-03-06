using Microsoft.EntityFrameworkCore;
using TrackingBle.Data; 
using TrackingBle.MappingProfiles;
using TrackingBle.Services;


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




builder.Services.AddAutoMapper(typeof(MstApplicationProfile));
builder.Services.AddAutoMapper(typeof(MstIntegrationProfile));
builder.Services.AddAutoMapper(typeof(MstAccessCctvProfile));
builder.Services.AddAutoMapper(typeof(MstAccessControlProfile));
builder.Services.AddAutoMapper(typeof(MstAreaProfile));
builder.Services.AddAutoMapper(typeof(MstBleReaderProfile));
builder.Services.AddAutoMapper(typeof(MstBrandProfile));
builder.Services.AddAutoMapper(typeof(MstDepartmentProfile));
builder.Services.AddAutoMapper(typeof(MstDistrictProfile));
builder.Services.AddAutoMapper(typeof(MstFloorProfile));
builder.Services.AddAutoMapper(typeof(MstMemberProfile));
builder.Services.AddAutoMapper(typeof(MstOrganizationProfile));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddDbContext<TrackingBleDbContext>(options =>
// options.UseSqlServer)

builder.Services.AddDbContext<TrackingBleDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleConnectionString")));


builder.Services.AddScoped<IMstAreaService, MstAreaService>();
builder.Services.AddScoped<IMstApplicationService, MstApplicationService>();
builder.Services.AddScoped<IMstIntegrationService, MstIntegrationService>();
builder.Services.AddScoped<IMstAccessCctvService, MstAccessCctvService>();
builder.Services.AddScoped<IMstAccessControlService, MstAccessControlService>();
builder.Services.AddScoped<IMstAreaService, MstAreaService>();
builder.Services.AddScoped<IMstBleReaderService, MstBleReaderService>();
builder.Services.AddScoped<IMstBrandService, MstBrandService>();
builder.Services.AddScoped<IMstDepartmentService, MstDepartmentService>();
builder.Services.AddScoped<IMstDistrictService, MstDistrictService>();
builder.Services.AddScoped<IMstFloorService, MstFloorService>();
builder.Services.AddScoped<IMstMemberService, MstMemberService>();
builder.Services.AddScoped<IMstOrganizationService, MstOrganizationService>();
var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
