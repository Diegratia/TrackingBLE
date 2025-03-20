using Microsoft.AspNetCore.Authentication.JwtBearer; // tambah jwtbearer untuk token dari auth api
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TrackingBle.Data; 
using TrackingBle.MappingProfiles;
using TrackingBle.Services;
using TrackingBle.Seeding;
using Microsoft.Extensions.FileProviders;
using TrackingBle.Services.Interfaces;
using BCrypt.Net;
using TrackingBle.Models.Domain;



var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"Jwt:Issuer = {builder.Configuration["Jwt:Issuer"]}");
Console.WriteLine($"Jwt:Audience = {builder.Configuration["Jwt:Audience"]}");
Console.WriteLine($"Jwt:Key = {builder.Configuration["Jwt:Key"]}");


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
builder.Services.AddAutoMapper(typeof(FloorplanMaskedAreaProfile));
builder.Services.AddAutoMapper(typeof(MstBleReaderProfile));
builder.Services.AddAutoMapper(typeof(MstBrandProfile));
builder.Services.AddAutoMapper(typeof(MstDepartmentProfile));
builder.Services.AddAutoMapper(typeof(MstDistrictProfile));
builder.Services.AddAutoMapper(typeof(MstFloorProfile));
builder.Services.AddAutoMapper(typeof(MstMemberProfile));
builder.Services.AddAutoMapper(typeof(MstOrganizationProfile));
builder.Services.AddAutoMapper(typeof(TrackingTransactionProfile));
builder.Services.AddAutoMapper(typeof(VisitorBlacklistAreaProfile));
builder.Services.AddAutoMapper(typeof(AlarmRecordTrackingProfile));
builder.Services.AddAutoMapper(typeof(FloorplanDeviceProfile));
builder.Services.AddAutoMapper(typeof(MstBuildingProfile));
builder.Services.AddAutoMapper(typeof(MstFloorplanProfile));

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
  options.Events = new JwtBearerEvents
{
    OnMessageReceived = context =>
    {
        var accessToken = context.Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrEmpty(accessToken) && accessToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            context.Token = accessToken.Substring("Bearer ".Length).Trim();
        }
        return Task.CompletedTask;
    },
    OnAuthenticationFailed = context =>
    {
        Console.WriteLine("Authentication failed: " + context.Exception.Message);
        Console.WriteLine("Token: " + context.Request.Headers["Authorization"]);
        return Task.CompletedTask;
    },
    OnTokenValidated = context =>
    {
        Console.WriteLine("Token validated successfully");
        return Task.CompletedTask;
    }
};
    });



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TrackingBle API", Version = "v1" });

    // detail token
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

builder.Services.AddDbContext<TrackingBleDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleConnectionString")));

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true,
//             ValidIssuer = builder.Configuration["Jwt:Issuer"],
//             ValidAudience = builder.Configuration["Jwt:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//         };
//     });

// Tambahkan IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
 
builder.Services.AddScoped<IFloorplanMaskedAreaService, FloorplanMaskedAreaService>();
builder.Services.AddScoped<IMstApplicationService, MstApplicationService>();
builder.Services.AddScoped<IMstIntegrationService, MstIntegrationService>();
builder.Services.AddScoped<IMstAccessCctvService, MstAccessCctvService>();
builder.Services.AddScoped<IMstAccessControlService, MstAccessControlService>();
builder.Services.AddScoped<IFloorplanMaskedAreaService, FloorplanMaskedAreaService>();
builder.Services.AddScoped<IMstBleReaderService, MstBleReaderService>();
builder.Services.AddScoped<IMstBrandService, MstBrandService>();
builder.Services.AddScoped<IMstDepartmentService, MstDepartmentService>();
builder.Services.AddScoped<IMstDistrictService, MstDistrictService>();
builder.Services.AddScoped<IMstFloorService, MstFloorService>();
builder.Services.AddScoped<IMstMemberService, MstMemberService>();
builder.Services.AddScoped<IMstOrganizationService, MstOrganizationService>();
builder.Services.AddScoped<ITrackingTransactionService, TrackingTransactionService>();
builder.Services.AddScoped<IVisitorBlacklistAreaService, VisitorBlacklistAreaService>();
builder.Services.AddScoped<IVisitorService, VisitorService>();
builder.Services.AddScoped<IAlarmRecordTrackingService, AlarmRecordTrackingService>();
builder.Services.AddScoped<IFloorplanDeviceService, FloorplanDeviceService>();
builder.Services.AddScoped<IAlarmRecordTrackingService, AlarmRecordTrackingService>();
builder.Services.AddScoped<IMstBuildingService, MstBuildingService>();
builder.Services.AddScoped<IAuthService, AuthService>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TrackingBleDbContext>();
    context.Database.Migrate();
    // DatabaseSeeder.Seed(context);
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrackingBle API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
    RequestPath = "/Uploads"
});
app.UseCors("AllowAll");


app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();




