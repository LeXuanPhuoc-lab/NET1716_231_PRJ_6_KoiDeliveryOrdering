using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
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
            // Payment Types
            if (!dbContext.Payments.Any()) await SeedPaymentTypeAsync();
            // Shipping fees
            if (!dbContext.ShippingFees.Any()) await SeedShippingFeeAsync();
            // Delivery Orders
            if (!dbContext.DeliveryOrders.Any()) await SeedDeliveryOrderAsync();
            
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
        List<User> users = new()
        {
            new User()
            {
                Username = "username",
                Password = "password",
                FullName = "Admin",
                Phone = "0123456789",
                Email = "admin@admin.com",
                SenderInformations = new List<SenderInformation>()
                {
                    new ()
                    {
                        District = "ABC",
                        Latitude = 12,
                        Longitude = 12,
                        Street = "123 Main Street",
                        SenderName = "Random Name",
                        CityProvince = "Random City",
                        Ward = "Random Ward",
                        SenderPhone = "0123456789"
                    },
                    new ()
                    {
                        District = "ABC",
                        Latitude = 12,
                        Longitude = 12,
                        Street = "123 Main Street",
                        SenderName = "Random Name",
                        CityProvince = "Random City",
                        Ward = "Random Ward",
                        SenderPhone = "0123456789"
                    }
                }
            }
        };

        await dbContext.Users.AddRangeAsync(users);
        await dbContext.SaveChangesAsync();
    }


    //  Summary:
    //      Seeding Payment type
    private async Task SeedPaymentTypeAsync()
    {
        List<Payment> payments = new()
        {
            new()
            {
                PaymentMethod = "Cash"
            },
            new()
            {
                PaymentMethod = "Visa"
            },
            new()
            {
                PaymentMethod = "Debit"
            },
            new()
            {
                PaymentMethod = "Momo"
            },
            new()
            {
                PaymentMethod = "VNPay"
            }
        };

        await dbContext.Payments.AddRangeAsync(payments);
        await dbContext.SaveChangesAsync();
    }

    //  Summary:
    //      Seeding Shipping fee
    private async Task SeedShippingFeeAsync()
    {
        var shippingFees = new List<ShippingFee>
        {
            new ShippingFee
            {
                DistanceRangeFrom = 0m,
                DistanceRangeTo = 5m,
                ServiceCode = "STD",
                WeightClass = 1,
                BaseFee = 5.00m,
                EstimatedTime = "1-2 days"
            },
            new ShippingFee
            {
                DistanceRangeFrom = 5.01m,
                DistanceRangeTo = 10m,
                ServiceCode = "EXP",
                WeightClass = 2,
                BaseFee = 10.00m,
                EstimatedTime = "Same day"
            },
            new ShippingFee
            {
                DistanceRangeFrom = 10.01m,
                DistanceRangeTo = 20m,
                ServiceCode = "STD",
                WeightClass = 3,
                BaseFee = 15.00m,
                EstimatedTime = "2-3 days"
            },
            new ShippingFee
            {
                DistanceRangeFrom = 20.01m,
                DistanceRangeTo = 50m,
                ServiceCode = "PRM",
                WeightClass = 1,
                BaseFee = 20.00m,
                EstimatedTime = "Next day"
            },
            new ShippingFee
            {
                DistanceRangeFrom = 50.01m,
                DistanceRangeTo = 100m,
                ServiceCode = "EXP",
                WeightClass = 2,
                BaseFee = 30.00m,
                EstimatedTime = "Same day"
            }
        };

        await dbContext.ShippingFees.AddRangeAsync(shippingFees);
        await dbContext.SaveChangesAsync();
    }

    //  Summary:
    //      Seeding Delivery Orders
    private async Task SeedDeliveryOrderAsync()
    {
        var rnd = new Random();
        var paymentTypes = await dbContext.Payments.ToListAsync();
        var shippingFees = await dbContext.ShippingFees.ToListAsync();
        var users = await dbContext.Users.ToListAsync();

        var deliveryOrders = new List<DeliveryOrder>
        {
            new DeliveryOrder
            {
                RecipientAddress = "123 Elm St, New York, NY",
                RecipientLongitude = -73.935242,
                RecipientLatitude = 40.730610,
                RecipientAppointmentTime = "2024-09-28 10:00 AM",
                SenderAddress = "456 Maple Ave, Boston, MA",
                SenderLongitude = -71.0589,
                SenderLatitude = 42.3601,
                CreateDate = DateTime.Now.AddDays(-10),
                DeliveryDate = DateTime.Now.AddDays(-1),
                OrderStatus = "Delivered",
                TotalAmount = 100.50m,
                TaxFee = 8.50m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                CustomerId = users[rnd.Next(users.Count)].Id
            },
            new DeliveryOrder
            {
                RecipientAddress = "789 Oak St, San Francisco, CA",
                RecipientLongitude = -122.4194,
                RecipientLatitude = 37.7749,
                RecipientAppointmentTime = "2024-09-30 2:00 PM",
                SenderAddress = "101 Pine St, Chicago, IL",
                SenderLongitude = -87.6298,
                SenderLatitude = 41.8781,
                CreateDate = DateTime.Now.AddDays(-3),
                DeliveryDate = null,
                OrderStatus = "Out for Delivery",
                TotalAmount = 95.50m,
                TaxFee = 5.50m,
                IsPurchased = true,
                IsSenderPurchase = true,
                IsInternational = true,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                CustomerId = users[rnd.Next(users.Count)].Id
            },
            new DeliveryOrder
            {
                RecipientAddress = "202 Elm St, Miami, FL",
                RecipientLongitude = -80.1918,
                RecipientLatitude = 25.7617,
                RecipientAppointmentTime = null,
                SenderAddress = "333 Birch St, Seattle, WA",
                SenderLongitude = -122.3321,
                SenderLatitude = 47.6062,
                CreateDate = DateTime.Now.AddDays(-5),
                DeliveryDate = DateTime.Now.AddDays(2),
                OrderStatus = "Pending",
                TotalAmount = 120.00m,
                TaxFee = null,
                IsPurchased = false,
                IsSenderPurchase = true,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                CustomerId = users[rnd.Next(users.Count)].Id
            },
            new DeliveryOrder
            {
                RecipientAddress = "456 Willow Rd, Austin, TX",
                RecipientLongitude = -97.7431,
                RecipientLatitude = 30.2672,
                RecipientAppointmentTime = "2024-10-05 1:30 PM",
                SenderAddress = "789 Cedar St, Los Angeles, CA",
                SenderLongitude = -118.2437,
                SenderLatitude = 34.0522,
                CreateDate = DateTime.Now.AddDays(-7),
                DeliveryDate = null,
                OrderStatus = "Dispatched",
                TotalAmount = 150.25m,
                TaxFee = 10.25m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                CustomerId = users[rnd.Next(users.Count)].Id
            },
            new DeliveryOrder
            {
                RecipientAddress = "101 Maple Dr, Denver, CO",
                RecipientLongitude = -104.9903,
                RecipientLatitude = 39.7392,
                RecipientAppointmentTime = null,
                SenderAddress = "202 Spruce Ln, San Diego, CA",
                SenderLongitude = -117.1611,
                SenderLatitude = 32.7157,
                CreateDate = DateTime.Now.AddDays(-2),
                DeliveryDate = DateTime.Now.AddDays(1),
                OrderStatus = "Scheduled",
                TotalAmount = 110.00m,
                TaxFee = 7.50m,
                IsPurchased = false,
                IsSenderPurchase = true,
                IsInternational = true,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                CustomerId = users[rnd.Next(users.Count)].Id
            },
            new DeliveryOrder
            {
                RecipientAddress = "789 Birch St, Las Vegas, NV",
                RecipientLongitude = -115.1398,
                RecipientLatitude = 36.1699,
                RecipientAppointmentTime = "2024-09-27 4:00 PM",
                SenderAddress = "456 Oak Dr, Portland, OR",
                SenderLongitude = -122.6765,
                SenderLatitude = 45.5231,
                CreateDate = DateTime.Now.AddDays(-4),
                DeliveryDate = DateTime.Now,
                OrderStatus = "Delivered",
                TotalAmount = 140.75m,
                TaxFee = 9.75m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                CustomerId = users[rnd.Next(users.Count)].Id
            },
            new DeliveryOrder
            {
                RecipientAddress = "333 Cedar Ln, Dallas, TX",
                RecipientLongitude = -96.7970,
                RecipientLatitude = 32.7767,
                RecipientAppointmentTime = null,
                SenderAddress = "202 Pine Ave, Phoenix, AZ",
                SenderLongitude = -112.0740,
                SenderLatitude = 33.4484,
                CreateDate = DateTime.Now.AddDays(-1),
                DeliveryDate = null,
                OrderStatus = "Processing",
                TotalAmount = 175.00m,
                TaxFee = 12.50m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = true,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                CustomerId = users[rnd.Next(users.Count)].Id
            },
            new DeliveryOrder
            {
                RecipientAddress = "101 Elm Dr, Salt Lake City, UT",
                RecipientLongitude = -111.8910,
                RecipientLatitude = 40.7608,
                RecipientAppointmentTime = "2024-09-26 10:00 AM",
                SenderAddress = "456 Spruce Rd, Columbus, OH",
                SenderLongitude = -82.9988,
                SenderLatitude = 39.9612,
                CreateDate = DateTime.Now.AddDays(-6),
                DeliveryDate = DateTime.Now.AddDays(1),
                OrderStatus = "Pending",
                TotalAmount = 90.00m,
                TaxFee = 6.00m,
                IsPurchased = false,
                IsSenderPurchase = true,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                CustomerId = users[rnd.Next(users.Count)].Id
            },
            new DeliveryOrder
            {
                RecipientAddress = "789 Pine St, Charlotte, NC",
                RecipientLongitude = -80.8431,
                RecipientLatitude = 35.2271,
                RecipientAppointmentTime = "2024-09-29 11:00 AM",
                SenderAddress = "333 Maple Ave, Orlando, FL",
                SenderLongitude = -81.3792,
                SenderLatitude = 28.5384,
                CreateDate = DateTime.Now.AddDays(-8),
                DeliveryDate = null,
                OrderStatus = "Out for Delivery",
                TotalAmount = 130.50m,
                TaxFee = 8.50m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = true,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                CustomerId = users[rnd.Next(users.Count)].Id
            },
            new DeliveryOrder
            {
                RecipientAddress = "101 Birch Ln, Nashville, TN",
                RecipientLongitude = -86.7816,
                RecipientLatitude = 36.1627,
                RecipientAppointmentTime = null,
                SenderAddress = "789 Oak Dr, Houston, TX",
                SenderLongitude = -95.3698,
                SenderLatitude = 29.7604,
                CreateDate = DateTime.Now.AddDays(-5),
                DeliveryDate = DateTime.Now.AddDays(3),
                OrderStatus = "Scheduled",
                TotalAmount = 160.75m,
                TaxFee = 11.75m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = false,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                CustomerId = users[rnd.Next(users.Count)].Id
            }
        };

        await dbContext.DeliveryOrders.AddRangeAsync(deliveryOrders);
        await dbContext.SaveChangesAsync();
    }
}