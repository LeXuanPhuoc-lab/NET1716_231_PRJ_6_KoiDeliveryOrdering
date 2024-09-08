using KoiDeliveryOrdering.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrdering.Data;


public interface IDatabaseInitializer
{
    Task InitializeAsync();
    Task TrySeedAsync();
    Task SeedAsync();
}

public class DatabaseInitializer(KoiDeliveryOrderingDbContext dbContext) : IDatabaseInitializer
{
    //  Summary:
    //      Initialize Database
    public async Task InitializeAsync()
    {
        try
        {
            // Check whether the database exists and can be connected to
            if (!await dbContext.Database.CanConnectAsync())
            {
                // Check for applied migrations
                var appliedMigrations = await dbContext.Database.GetAppliedMigrationsAsync();
                if (appliedMigrations.Any())
                {
                    Console.WriteLine("Migrations have been applied.");
                    return;
                }

                // Perform migration if necessary
                await dbContext.Database.MigrateAsync();
                Console.WriteLine("Database initialized successfully");
            }
            else
            {
                Console.WriteLine("Database cannot be connected to.");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //  Summary:
    //      Try to perform seeding data
    public async Task TrySeedAsync()
    {
        try
        {
            await SeedAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //  Summary:
    //      Seeding data
    public async Task SeedAsync()
    {
        try
        {
            // Users
            if (!dbContext.Users.Any()) await SeedUserAsync();
            
            // More seeding here...
            // Each table need to create private method to seeding data
            
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    //  Summary:
    //      Seeding Users
    private async Task SeedUserAsync()
    {
        await Task.CompletedTask;  
    }
}