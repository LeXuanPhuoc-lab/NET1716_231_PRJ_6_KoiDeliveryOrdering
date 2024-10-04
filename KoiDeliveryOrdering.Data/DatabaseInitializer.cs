using KoiDeliveryOrdering.Data.Context;
using KoiDeliveryOrdering.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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
			// Delivery Orders
			if (!dbContext.Documents.Any()) await SeedDocumentAsync();
            // Animal Types
            if (!dbContext.AnimalTypes.Any()) await SeedAnimalTypeAsync();


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
                Username = "admin",
                Password = "@Admin123",
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
                },
                VoucherPromotions = new List<VoucherPromotion>()
                {
                    new()
                    {
                        VoucherPromotionCode = "ABC",
                        PromotionRate = 10
                    },
                    new()
                    {
                        VoucherPromotionCode = "ABC_2",
                        PromotionRate = 15
                    },
                    new()
                    {
                        VoucherPromotionCode = "ABC_3",
                        PromotionRate = 15
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
        var senders = await dbContext.SenderInformations.ToListAsync();

        var deliveryOrders = new List<DeliveryOrder>
        {
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "123 Elm St, New York, NY",
                RecipientLongitude = -73.935242,
                RecipientLatitude = 40.730610,
                RecipientAppointmentTime = "2024-09-28 10:00 AM",
                RecipientPhone = "0123456789",
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
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "789 Oak St, San Francisco, CA",
                RecipientLongitude = -122.4194,
                RecipientLatitude = 37.7749,
                RecipientAppointmentTime = "2024-09-30 2:00 PM",
                RecipientPhone = "0123456789",
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
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "202 Elm St, Miami, FL",
                RecipientLongitude = -80.1918,
                RecipientLatitude = 25.7617,
                RecipientAppointmentTime = null,
                RecipientPhone = "0123456789",
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
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "456 Willow Rd, Austin, TX",
                RecipientLongitude = -97.7431,
                RecipientLatitude = 30.2672,
                RecipientAppointmentTime = "2024-10-05 1:30 PM",
                RecipientPhone = "0123456789",
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
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "101 Maple Dr, Denver, CO",
                RecipientLongitude = -104.9903,
                RecipientLatitude = 39.7392,
                RecipientAppointmentTime = null,
                RecipientPhone = "0123456789",
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
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "789 Birch St, Las Vegas, NV",
                RecipientLongitude = -115.1398,
                RecipientLatitude = 36.1699,
                RecipientAppointmentTime = "2024-09-27 4:00 PM",
                RecipientPhone = "0123456789",
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
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "333 Cedar Ln, Dallas, TX",
                RecipientLongitude = -96.7970,
                RecipientLatitude = 32.7767,
                RecipientAppointmentTime = null,
                CreateDate = DateTime.Now.AddDays(-1),
                RecipientPhone = "0123456789",
                DeliveryDate = null,
                OrderStatus = "Processing",
                TotalAmount = 175.00m,
                TaxFee = 12.50m,
                IsPurchased = true,
                IsSenderPurchase = false,
                IsInternational = true,
                ShippingFeeId = shippingFees[rnd.Next(shippingFees.Count)].ShippingFeeId,
                PaymentId = paymentTypes[rnd.Next(paymentTypes.Count)].PaymentId,
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "101 Elm Dr, Salt Lake City, UT",
                RecipientLongitude = -111.8910,
                RecipientLatitude = 40.7608,
                RecipientAppointmentTime = "2024-09-26 10:00 AM",
                RecipientPhone = "0123456789",
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
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "789 Pine St, Charlotte, NC",
                RecipientLongitude = -80.8431,
                RecipientLatitude = 35.2271,
                RecipientAppointmentTime = "2024-09-29 11:00 AM",
                RecipientPhone = "0123456789",
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
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            },
            new DeliveryOrder
            {
                RecipientName = "Recipient ABCD",
                RecipientAddress = "101 Birch Ln, Nashville, TN",
                RecipientLongitude = -86.7816,
                RecipientLatitude = 36.1627,
                RecipientAppointmentTime = null,
                RecipientPhone = "0123456789",
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
                SenderInformationId = senders[rnd.Next(senders.Count)].SenderInformationId
            }
        };

        await dbContext.DeliveryOrders.AddRangeAsync(deliveryOrders);
        await dbContext.SaveChangesAsync();
    }

	//  Summary:
	//      Seeding Document
	private async Task SeedDocumentAsync()
	{
		List<Document> documents = new()
        {
	        new Document
	        {
		        DocumentNumber = "DOC001",
		        DocumentType = "Invoice",
		        IssueDate = new DateTime(2023, 1, 15),
		        ExpirationDate = new DateTime(2024, 1, 15),
		        ConsigneeName = "ABC Company",
		        ConsigneePhone = "+1234567890",
		        ConsigneeAddress = "123 Main Street, Springfield",
		        ExporterName = "Global Exports Ltd.",
		        ExporterPhone = "+1987654321",
		        ExporterAddress = "789 Business Rd, Shelbyville",
		        DispatchMethod = "Air",
		        FinalDestination = "New York, USA",
		        TransportationNo = "TRANS001",
		        TransportationType = "Airplane",
		        PortOfLoading = "London Heathrow Airport",
		        PortOfDischarge = "JFK International Airport",
		        TaxFee = 500.00m,
		        ShippingFee = 1500.00m,
		        AssurranceFee = 250.00m
	        },
	        new Document
	        {
		        DocumentNumber = "DOC002",
		        DocumentType = "Packing List",
		        IssueDate = new DateTime(2023, 2, 1),
		        ExpirationDate = null,
		        ConsigneeName = "XYZ Industries",
		        ConsigneePhone = "+9876543210",
		        ConsigneeAddress = "456 Industry Ave, Metropolis",
		        ExporterName = "FastShip Logistics",
		        ExporterPhone = "+1234098765",
		        ExporterAddress = "12 Logistic Ln, Gotham City",
		        DispatchMethod = "Sea",
		        FinalDestination = "San Francisco, USA",
		        TransportationNo = "TRANS002",
		        TransportationType = "Ship",
		        PortOfLoading = "Port of Rotterdam",
		        PortOfDischarge = "Port of Oakland",
		        TaxFee = 350.00m,
		        ShippingFee = 2000.00m,
		        AssurranceFee = 300.00m
	        },
	        new Document
	        {
		        DocumentNumber = "DOC003",
		        DocumentType = "Bill of Lading",
		        IssueDate = new DateTime(2023, 3, 10),
		        ExpirationDate = new DateTime(2023, 9, 10),
		        ConsigneeName = "PQR Traders",
		        ConsigneePhone = "+1122334455",
		        ConsigneeAddress = "789 Market St, Star City",
		        ExporterName = "TradeMasters Inc.",
		        ExporterPhone = "+9988776655",
		        ExporterAddress = "99 Export Blvd, Central City",
		        DispatchMethod = "Road",
		        FinalDestination = "Chicago, USA",
		        TransportationNo = "TRANS003",
		        TransportationType = "Truck",
		        PortOfLoading = "Houston Port",
		        PortOfDischarge = "Chicago Terminal",
		        TaxFee = 250.00m,
		        ShippingFee = 1200.00m,
		        AssurranceFee = 200.00m
	        },
	        new Document
	        {
		        DocumentNumber = "DOC004",
		        DocumentType = "Certificate of Origin",
		        IssueDate = new DateTime(2023, 4, 5),
		        ExpirationDate = null,
		        ConsigneeName = "LMN Enterprises",
		        ConsigneePhone = "+1029384756",
		        ConsigneeAddress = "101 Commerce Blvd, Coast City",
		        ExporterName = "Origin Exports",
		        ExporterPhone = "+5647382910",
		        ExporterAddress = "55 Heritage St, Emerald City",
		        DispatchMethod = "Rail",
		        FinalDestination = "Dallas, USA",
		        TransportationNo = "TRANS004",
		        TransportationType = "Train",
		        PortOfLoading = "Los Angeles Terminal",
		        PortOfDischarge = "Dallas Terminal",
		        TaxFee = 400.00m,
		        ShippingFee = 800.00m,
		        AssurranceFee = 150.00m
	        },
	        new Document
	        {
		        DocumentNumber = "DOC005",
		        DocumentType = "Commercial Invoice",
		        IssueDate = new DateTime(2023, 5, 18),
		        ExpirationDate = new DateTime(2024, 5, 18),
		        ConsigneeName = "RST Company",
		        ConsigneePhone = "+6758493021",
		        ConsigneeAddress = "45 Trade Dr, Gotham City",
		        ExporterName = "Global Trade Ltd.",
		        ExporterPhone = "+7849301023",
		        ExporterAddress = "600 Export Way, Metropolis",
		        DispatchMethod = "Air",
		        FinalDestination = "Seattle, USA",
		        TransportationNo = "TRANS005",
		        TransportationType = "Airplane",
		        PortOfLoading = "Paris Charles de Gaulle Airport",
		        PortOfDischarge = "Seattle-Tacoma International Airport",
		        TaxFee = 600.00m,
		        ShippingFee = 1700.00m,
		        AssurranceFee = 350.00m
	        },
	        new Document
	        {
		        DocumentNumber = "DOC006",
		        DocumentType = "Export Declaration",
		        IssueDate = new DateTime(2023, 6, 25),
		        ExpirationDate = null,
		        ConsigneeName = "UVW Group",
		        ConsigneePhone = "+1231231234",
		        ConsigneeAddress = "111 Business Park, Starling City",
		        ExporterName = "Export Partners Ltd.",
		        ExporterPhone = "+3213214321",
		        ExporterAddress = "99 Export Street, Central City",
		        DispatchMethod = "Sea",
		        FinalDestination = "Miami, USA",
		        TransportationNo = "TRANS006",
		        TransportationType = "Ship",
		        PortOfLoading = "Shanghai Port",
		        PortOfDischarge = "Port of Miami",
		        TaxFee = 500.00m,
		        ShippingFee = 2500.00m,
		        AssurranceFee = 400.00m
	        },
	        new Document
	        {
		        DocumentNumber = "DOC007",
		        DocumentType = "Insurance Certificate",
		        IssueDate = new DateTime(2023, 7, 12),
		        ExpirationDate = new DateTime(2024, 7, 12),
		        ConsigneeName = "DEF Logistics",
		        ConsigneePhone = "+4564564567",
		        ConsigneeAddress = "23 Shipping Lane, Keystone City",
		        ExporterName = "Assured Exports Inc.",
		        ExporterPhone = "+6546547654",
		        ExporterAddress = "32 Insurance Blvd, Star City",
		        DispatchMethod = "Road",
		        FinalDestination = "Los Angeles, USA",
		        TransportationNo = "TRANS007",
		        TransportationType = "Truck",
		        PortOfLoading = "Houston Port",
		        PortOfDischarge = "Los Angeles Terminal",
		        TaxFee = 300.00m,
		        ShippingFee = 1000.00m,
		        AssurranceFee = 250.00m
	        },
	        new Document
	        {
		        DocumentNumber = "DOC008",
		        DocumentType = "Shipping Order",
		        IssueDate = new DateTime(2023, 8, 20),
		        ExpirationDate = null,
		        ConsigneeName = "GHI Retailers",
		        ConsigneePhone = "+7897897890",
		        ConsigneeAddress = "678 Commerce Ave, Coast City",
		        ExporterName = "WorldWide Shippers",
		        ExporterPhone = "+8908908901",
		        ExporterAddress = "77 Global Rd, Emerald City",
		        DispatchMethod = "Air",
		        FinalDestination = "Denver, USA",
		        TransportationNo = "TRANS008",
		        TransportationType = "Airplane",
		        PortOfLoading = "Frankfurt Airport",
		        PortOfDischarge = "Denver International Airport",
		        TaxFee = 700.00m,
		        ShippingFee = 1800.00m,
		        AssurranceFee = 300.00m
	        },
	        new Document
	        {
		        DocumentNumber = "DOC009",
		        DocumentType = "Proforma Invoice",
		        IssueDate = new DateTime(2023, 9, 5),
		        ExpirationDate = new DateTime(2024, 9, 5),
		        ConsigneeName = "JKL Suppliers",
		        ConsigneePhone = "+0980980987",
		        ConsigneeAddress = "88 Supplier St, Metropolis",
		        ExporterName = "ExportKing Ltd.",
		        ExporterPhone = "+8768768765",
		        ExporterAddress = "123 Export Avenue, Gotham City",
		        DispatchMethod = "Rail",
		        FinalDestination = "Boston, USA",
		        TransportationNo = "TRANS009",
		        TransportationType = "Train",
		        PortOfLoading = "Vancouver Terminal",
		        PortOfDischarge = "Boston Terminal",
		        TaxFee = 450.00m,
		        ShippingFee = 900.00m,
		        AssurranceFee = 180.00m
	        },
	        new Document
	        {
		        DocumentNumber = "DOC010",
		        DocumentType = "Inspection Certificate",
		        IssueDate = new DateTime(2023, 10, 15),
		        ExpirationDate = null,
		        ConsigneeName = "MNO Warehousing",
		        ConsigneePhone = "+6543219870",
		        ConsigneeAddress = "99 Distribution Blvd, Star City",
		        ExporterName = "Inspection Ready Exports",
		        ExporterPhone = "+3459872345",
		        ExporterAddress = "456 Verification Ln, Central City",
		        DispatchMethod = "Sea",
		        FinalDestination = "Houston, USA",
		        TransportationNo = "TRANS010",
		        TransportationType = "Ship",
		        PortOfLoading = "Port of Singapore",
		        PortOfDischarge = "Port of Houston",
		        TaxFee = 380.00m,
		        ShippingFee = 2200.00m,
		        AssurranceFee = 320.00m
	        }
        };

        await dbContext.Documents.AddRangeAsync(documents);
        await dbContext.SaveChangesAsync();
    }

    //  Summary:
    //      Seeding Animal Type
    private async Task SeedAnimalTypeAsync()
    {
        List<AnimalType> animalTypes = new()
        {
            new() { AnimalTypeDesc = "Mammal" },
            new() { AnimalTypeDesc = "Bird" },
            new() { AnimalTypeDesc = "Reptile" },
            new() { AnimalTypeDesc = "Amphibian" },
            new() { AnimalTypeDesc = "Fish" },
            new() { AnimalTypeDesc = "Insect" },
        };

        await dbContext.AnimalTypes.AddRangeAsync(animalTypes);
        await dbContext.SaveChangesAsync();
    }
}