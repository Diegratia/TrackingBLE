    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;
    using TrackingBle.src._9MstBrand.Data;
    using TrackingBle.src._9MstBrand.Services;
    using TrackingBle.src._9MstBrand.MappingProfiles;
    using DotNetEnv;

    DotNetEnv.Env.Load("../../.env");

    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<MstBrandDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TrackingBleDbConnection") ?? 
                            "Server=192.168.1.116,1433;Database=TrackingBleDevV3;User Id=sa;Password=Password_123#;TrustServerCertificate=True"));

    builder.Services.AddScoped<IMstBrandService, MstBrandService>();
    builder.Services.AddAutoMapper(typeof(MstBrandProfile));

    var port = Environment.GetEnvironmentVariable("MST_BRAND_PORT") ?? 
            builder.Configuration["Ports:MstBrandService"] ?? "5009";
    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
    var host = env == "Production" ? "0.0.0.0" : "localhost";
    builder.WebHost.UseUrls($"http://{host}:{port}");

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "MstBrand API V1");
            c.RoutePrefix = "";
        });
    }

    
    app.UseAuthorization();
    app.MapControllers();

    app.MapGet("/api/MstBrand/health", () => "Hello from MstBrand!");
    app.MapGet("/", () => "Hello from MstBrand!");

    Console.WriteLine("Environment Variables Check");
    Console.WriteLine($"ASPNETCORE_ENVIRONMENT: {env}");
    Console.WriteLine($"MST_BRAND_PORT: {Environment.GetEnvironmentVariable("MST_BRAND_PORT")}");
    Console.WriteLine($"Configured Port: {port}");
    Console.WriteLine($"Host: {host}");
    Console.WriteLine($"Application URL: http://{host}:{port}");
    Console.WriteLine($"Current Environment: {app.Environment.EnvironmentName}");
    Console.WriteLine($"Is Development: {app.Environment.IsDevelopment()}");
    Console.WriteLine($"Connection String: {builder.Configuration.GetConnectionString("TrackingBleDbConnection")}");
    Console.WriteLine("==================================");
    Console.WriteLine($"Starting on http://{host}:{port} in {env} environment...");

    app.Run();