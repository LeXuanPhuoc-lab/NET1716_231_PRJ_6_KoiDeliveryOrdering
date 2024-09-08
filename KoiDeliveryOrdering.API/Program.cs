using KoiDeliveryOrdering.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure DBContext
builder.Services.ConfigureDbContext(builder.Configuration)
    // Configure application services
    .AddApplicationServices();

// Configure App settings
builder.Services.AddApplicationConfiguration(
    builder.Configuration, builder.Environment);

// Configure Cloudinary
builder.Services.ConfigureCloudinary();
    
// Configure CORS
builder.Services.AddCors(p => p.AddPolicy("Cors", policy =>
{
    // allow all with any header, method
    policy.WithOrigins("*")
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

// Configure Mapster
builder.Services.ConfigureMapster();

builder.Services.AddControllers();

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Debug()
    .WriteTo.Console()
    .Enrich.WithProperty("Environment", builder.Environment)
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Register database initializer
app.Lifetime.ApplicationStarted.Register(() => Task.Run(async () => await app.InitializeDatabaseAsync()));
// Configure exception handler
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseCors("Cors");
// app.UseAuthorization();
app.MapControllers();
app.Run();

