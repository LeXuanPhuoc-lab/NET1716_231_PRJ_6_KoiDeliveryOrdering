using KoiDeliveryOrdering.Data;

namespace KoiDeliveryOrdering.API.Extensions;

public static class DatabaseInitializerExtension
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    
                await initializer.InitializeAsync();
                await initializer.TrySeedAsync();
            }
        }
}