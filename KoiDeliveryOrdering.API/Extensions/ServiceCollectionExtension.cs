using KoiDeliveryOrdering.Business;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Business.Models;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Context;
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace KoiDeliveryOrdering.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Configure/Add services
        services.AddScoped<UnitOfWork>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDeliveryOrderService, DeliveryOrderService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IShippingFeeService, ShippingFeeService>();
        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        services.AddScoped<IDailyCareScheduleService, DailyCareScheduleService>();
        services.AddScoped<IDeliveryOrderDetailService, DeliveryOrderDetailService>();
        services.AddScoped<ICareTaskService, CareTaskService>();

        return services;
    }

    public static IServiceCollection AddApplicationConfiguration(this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        // Configure App settings
        services.Configure<AppSettings>(
            configuration.GetSection("AppSettings"));

        return services;
    }

    public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSqlServer<KoiDeliveryOrderingDbContext>(
            configuration.GetConnectionString("DefaultConnectionString"));
    }

    public static IServiceCollection ConfigureMapster(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Default
            .MapToConstructor(true)
            .PreserveReference(true);
        // Get Mapster GlobalSettings
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        // Scans the assembly and gets the IRegister, adding the registration to the TypeAdapterConfig
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());

        // Register the mapper as Singleton service for my application
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);

        return services;
    }

    public static IServiceCollection ConfigureCloudinary(this IServiceCollection services)
    {
        // Configure this later...

        return services;
    }
}