using KoiDeliveryOrdering.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace KoiDeliveryOrdering.Data.Context;

public partial class KoiDeliveryOrderingDbContext : DbContext
{
    public KoiDeliveryOrderingDbContext()
    {
    }

    public KoiDeliveryOrderingDbContext(DbContextOptions<KoiDeliveryOrderingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<AnimalType> AnimalTypes { get; set; }

    public virtual DbSet<CareLog> CareLogs { get; set; }

    public virtual DbSet<CareTask> CareTasks { get; set; }

    public virtual DbSet<DailyCareSchedule> DailyCareSchedules { get; set; }

    public virtual DbSet<DeliveryOrder> DeliveryOrders { get; set; }

    public virtual DbSet<DeliveryOrderDetail> DeliveryOrderDetails { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentDetail> DocumentDetails { get; set; }

    public virtual DbSet<Garage> Garages { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<OrderAssignment> OrderAssignments { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<SenderInformation> SenderInformations { get; set; }

    public virtual DbSet<ShippingFee> ShippingFees { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Truck> Trucks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VoucherPromotion> VoucherPromotions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString(), o 
            => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
    
    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        
        return configuration.GetConnectionString("DefaultConnectionString")!;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.ToTable("Animal");

            entity.HasIndex(e => e.AnimalId, "UQ_Animal_AnimalId").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AnimalId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("animal_id");
            entity.Property(e => e.AnimalTypeId).HasColumnName("animal_type_id");
            entity.Property(e => e.Breed)
                .HasMaxLength(50)
                .HasColumnName("breed");
            entity.Property(e => e.ColorPattern)
                .HasMaxLength(20)
                .HasColumnName("color_pattern");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EstimatedPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("estimated_price");
            entity.Property(e => e.HealthStatus)
                .HasMaxLength(50)
                .HasColumnName("health_status");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.IsAvailable).HasColumnName("is_available");
            entity.Property(e => e.OriginCountry)
                .HasMaxLength(50)
                .HasColumnName("origin_country");
            entity.Property(e => e.Size)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("size");

            entity.HasOne(d => d.AnimalType).WithMany(p => p.Animals)
                .HasForeignKey(d => d.AnimalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_Animal_AnimalType");
        });

        modelBuilder.Entity<AnimalType>(entity =>
        {
            entity.HasKey(e => e.AnimalTypeId).HasName("PK_AnimalType");

            entity.ToTable("Animal_Type");

            entity.Property(e => e.AnimalTypeId).HasColumnName("animal_type_id");
            entity.Property(e => e.AnimalTypeDesc)
                .HasMaxLength(155)
                .HasColumnName("animal_type_desc");
        });

        modelBuilder.Entity<CareLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CareLog");

            entity.ToTable("Care_Log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("actual_value");
            entity.Property(e => e.DailyCareScheduleId).HasColumnName("daily_care_schedule_id");
            entity.Property(e => e.LogDate)
                .HasColumnType("datetime")
                .HasColumnName("log_date");
            entity.Property(e => e.StaffComments)
                .HasMaxLength(255)
                .HasColumnName("staff_comments");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.DailyCareSchedule).WithMany(p => p.CareLogs)
                .HasForeignKey(d => d.DailyCareScheduleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CareLog_DailyCareSchedule");

            entity.HasOne(d => d.Staff).WithMany(p => p.CareLogs)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CareLog_Staff");
        });

        modelBuilder.Entity<CareTask>(entity =>
        {
            entity.HasKey(e => e.CareTaskId).HasName("PK_CareTask");

            entity.ToTable("Care_Task");

            entity.Property(e => e.CareTaskId).HasColumnName("care_task_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.TaskName)
                .HasMaxLength(100)
                .HasColumnName("task_name");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasColumnName("unit");
        });

        modelBuilder.Entity<DailyCareSchedule>(entity =>
        {
            entity.HasKey(e => e.DailyCareScheduleId).HasName("PK_DailyCareSchedule");

            entity.ToTable("Daily_Care_Schedule");

            entity.Property(e => e.DailyCareScheduleId).HasColumnName("daily_care_schedule_id");
            entity.Property(e => e.CareTaskId).HasColumnName("care_task_id");
            entity.Property(e => e.DeliverOrderDetailId).HasColumnName("deliver_order_detail_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.RecommendedValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("recommended_value");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.TaskFrequency)
                .HasMaxLength(50)
                .HasColumnName("task_frequency");

            entity.HasOne(d => d.CareTask).WithMany(p => p.DailyCareSchedules)
                .HasForeignKey(d => d.CareTaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyCareSchedule_CareTask");

            entity.HasOne(d => d.DeliverOrderDetail).WithMany(p => p.DailyCareSchedules)
                .HasForeignKey(d => d.DeliverOrderDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DailyCareSchedule_DeliveryOrderDetail");
        });

        modelBuilder.Entity<DeliveryOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DeliveryOrder");

            entity.ToTable("Delivery_Order");

            entity.HasIndex(e => e.DeliveryOrderId, "UQ_DeliveryOrder_OrderId").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            //entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.SenderInformationId).HasColumnName("sender_information_id");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("datetime")
                .HasColumnName("delivery_date");
            entity.Property(e => e.DeliveryOrderId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("delivery_order_id");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.IsInternational).HasColumnName("is_international");
            entity.Property(e => e.IsPurchased).HasColumnName("is_purchased");
            entity.Property(e => e.IsSenderPurchase).HasColumnName("is_sender_purchase");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .HasColumnName("order_status");
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.RecipientName)
                .HasMaxLength(255)
                .HasColumnName("recipient_name");
            entity.Property(e => e.RecipientPhone)
                .HasMaxLength(15)
                .HasColumnName("recipient_phone");
            entity.Property(e => e.RecipientAddress)
                .HasMaxLength(255)
                .HasColumnName("recipient_address");
            entity.Property(e => e.RecipientAppointmentTime)
                .HasMaxLength(155)
                .HasColumnName("recipient_appointment_time");
            entity.Property(e => e.RecipientLatitude).HasColumnName("recipient_latitude");
            entity.Property(e => e.RecipientLongitude).HasColumnName("recipient_longitude");
            //entity.Property(e => e.SenderAddress)
            //    .HasMaxLength(255)
            //    .HasColumnName("sender_address");
            //entity.Property(e => e.SenderLatitude).HasColumnName("sender_latitude");
            //entity.Property(e => e.SenderLongitude).HasColumnName("sender_longitude");
            entity.Property(e => e.ShippingFeeId).HasColumnName("shipping_fee_id");
            entity.Property(e => e.TaxFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("tax_fee");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.VoucherPromotionId).HasColumnName("voucher_promotion_id");

            entity.HasOne(d => d.SenderInformation).WithMany(p => p.DeliveryOrders)
                .HasForeignKey(d => d.SenderInformationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeliveryOrder_SenderInformation");
            //entity.HasOne(d => d.Customer).WithMany(p => p.DeliveryOrders)
            //    .HasForeignKey(d => d.CustomerId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_DeliveryOrder_User");

            entity.HasOne(d => d.Document).WithMany(p => p.DeliveryOrders)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("FK_DeliveryOrder_Document");

            entity.HasOne(d => d.Payment).WithMany(p => p.DeliveryOrders)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeliveryOrder_Payment");

            entity.HasOne(d => d.ShippingFee).WithMany(p => p.DeliveryOrders)
                .HasForeignKey(d => d.ShippingFeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeliveryOrder_ShippingFee");

            entity.HasOne(d => d.VoucherPromotion).WithMany(p => p.DeliveryOrders)
                .HasForeignKey(d => d.VoucherPromotionId)
                .HasConstraintName("FK_DeliveryOrder_VoucherPromotion");
        });

        modelBuilder.Entity<DeliveryOrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OrderDetail");

            entity.ToTable("Delivery_Order_Detail");

            entity.HasIndex(e => e.DeliveryOrderDetailId, "UQ_OrderDetail_OrderDetailId").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnimalId).HasColumnName("animal_id");
            entity.Property(e => e.DeliveryOrderDetailId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("delivery_order_detail_id");
            entity.Property(e => e.DeliveryOrderId).HasColumnName("delivery_order_id");
            entity.Property(e => e.PostDeliveryHealthStatus)
                .HasMaxLength(50)
                .HasColumnName("post_delivery_health_status");
            entity.Property(e => e.PreDeliveryHealthStatus)
                .HasMaxLength(50)
                .HasColumnName("pre_delivery_health_status");

            entity.HasOne(d => d.Animal).WithMany(p => p.DeliveryOrderDetails)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Animal");

            entity.HasOne(d => d.DeliveryOrder).WithMany(p => p.DeliveryOrderDetails)
                .HasForeignKey(d => d.DeliveryOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.ToTable("Document");

            entity.HasIndex(e => e.DocumentId, "UQ_Document").IsUnique();

            entity.HasIndex(e => e.DocumentNumber, "UQ__Document__C8FE0D8C5D2DDE9F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssurranceFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("assurrance_fee");
            entity.Property(e => e.ConsigneeAddress)
                .HasMaxLength(155)
                .HasColumnName("consignee_address");
            entity.Property(e => e.ConsigneeName)
                .HasMaxLength(155)
                .HasColumnName("consignee_name");
            entity.Property(e => e.ConsigneePhone)
                .HasMaxLength(15)
                .HasColumnName("consignee_phone");
            entity.Property(e => e.DispatchMethod)
                .HasMaxLength(50)
                .HasColumnName("dispatch_method");
            entity.Property(e => e.DocumentId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("document_id");
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(50)
                .HasColumnName("document_number");
            entity.Property(e => e.DocumentType)
                .HasMaxLength(100)
                .HasColumnName("document_type");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("datetime")
                .HasColumnName("expiration_date");
            entity.Property(e => e.ExporterAddress)
                .HasMaxLength(155)
                .HasColumnName("exporter_address");
            entity.Property(e => e.ExporterName)
                .HasMaxLength(155)
                .HasColumnName("exporter_name");
            entity.Property(e => e.ExporterPhone)
                .HasMaxLength(15)
                .HasColumnName("exporter_phone");
            entity.Property(e => e.FinalDestination)
                .HasMaxLength(155)
                .HasColumnName("final_destination");
            entity.Property(e => e.IssueDate)
                .HasColumnType("datetime")
                .HasColumnName("issue_date");
            entity.Property(e => e.PortOfDischarge)
                .HasMaxLength(100)
                .HasColumnName("port_of_discharge");
            entity.Property(e => e.PortOfLoading)
                .HasMaxLength(100)
                .HasColumnName("port_of_loading");
            entity.Property(e => e.ShippingFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("shipping_fee");
            entity.Property(e => e.TaxFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("tax_fee");
            entity.Property(e => e.TransportationNo)
                .HasMaxLength(50)
                .HasColumnName("transportation_no");
            entity.Property(e => e.TransportationType)
                .HasMaxLength(100)
                .HasColumnName("transportation_type");
        });

        modelBuilder.Entity<DocumentDetail>(entity =>
        {
            entity.HasKey(e => e.DocumentDetailId).HasName("PK_DocumentDetail");

            entity.ToTable("Document_Detail");

            entity.Property(e => e.DocumentDetailId).HasColumnName("document_detail_id");
            entity.Property(e => e.DocumentDetailDescription)
                .HasMaxLength(500)
                .HasColumnName("document_detail_description");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.ItemCategory)
                .HasMaxLength(50)
                .HasColumnName("item_category");
            entity.Property(e => e.ItemEstimatePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("item_estimate_price");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemQuantity).HasColumnName("item_quantity");
            entity.Property(e => e.ItemWeight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("item_weight");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentDetails)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentDetail_Document");
        });

        modelBuilder.Entity<Garage>(entity =>
        {
            entity.ToTable("Garage");

            entity.Property(e => e.GarageId).HasColumnName("garage_id");
            entity.Property(e => e.CityProvince)
                .HasMaxLength(100)
                .HasColumnName("city_province");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .HasColumnName("district");
            entity.Property(e => e.GarageName)
                .HasMaxLength(100)
                .HasColumnName("garage_name");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.ManagerName)
                .HasMaxLength(100)
                .HasColumnName("manager_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.Street)
                .HasMaxLength(155)
                .HasColumnName("street");
            entity.Property(e => e.Ward)
                .HasMaxLength(100)
                .HasColumnName("ward");
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.JobTitleId).HasName("PK_JobTitle");

            entity.ToTable("Job_Title");

            entity.Property(e => e.JobTitleId).HasColumnName("job_title_id");
            entity.Property(e => e.JobTitleDesc)
                .HasMaxLength(100)
                .HasColumnName("job_title_desc");
        });

        modelBuilder.Entity<OrderAssignment>(entity =>
        {
            entity.HasKey(e => e.OrderAssignmentId).HasName("PK_OrderAssignment");

            entity.ToTable("Order_Assignment");

            entity.Property(e => e.OrderAssignmentId).HasColumnName("order_assignment_id");
            entity.Property(e => e.AssignedDate)
                .HasColumnType("datetime")
                .HasColumnName("assigned_date");
            entity.Property(e => e.AssignedTruckId).HasColumnName("assigned_truck_id");
            entity.Property(e => e.DeliveryOrderId).HasColumnName("delivery_order_id");
            entity.Property(e => e.DeliveryStatus)
                .HasMaxLength(50)
                .HasColumnName("delivery_status");
            entity.Property(e => e.DriverId).HasColumnName("driver_id");
            entity.Property(e => e.FishCarerId).HasColumnName("fish_carer_id");

            entity.HasOne(d => d.AssignedTruck).WithMany(p => p.OrderAssignments)
                .HasForeignKey(d => d.AssignedTruckId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderAssignment_Truck");

            entity.HasOne(d => d.DeliveryOrder).WithMany(p => p.OrderAssignments)
                .HasForeignKey(d => d.DeliveryOrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderAssignment_Order");

            entity.HasOne(d => d.Driver).WithMany(p => p.OrderAssignmentDrivers)
                .HasForeignKey(d => d.DriverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderAssignment_Staff");

            entity.HasOne(d => d.FishCarer).WithMany(p => p.OrderAssignmentFishCarers)
                .HasForeignKey(d => d.FishCarerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderAssignment_User");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .HasColumnName("payment_method");
        });

        modelBuilder.Entity<SenderInformation>(entity =>
        {
            entity.HasKey(e => e.SenderInformationId).HasName("PK_SenderInformation");

            entity.ToTable("Sender_Information");

            entity.Property(e => e.SenderInformationId).HasColumnName("sender_information_id");
            entity.Property(e => e.CityProvince)
                .HasMaxLength(100)
                .HasColumnName("city_province");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .HasColumnName("district");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.SenderAppointmentTime)
                .HasMaxLength(155)
                .HasColumnName("sender_appointment_time");
            entity.Property(e => e.SenderName)
                .HasMaxLength(155)
                .HasColumnName("sender_name");
            entity.Property(e => e.SenderPhone)
                .HasMaxLength(15)
                .HasColumnName("sender_phone");
            entity.Property(e => e.Street)
                .HasMaxLength(155)
                .HasColumnName("street");
            entity.Property(e => e.UserId)
                //.HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("user_id");
            entity.Property(e => e.Ward)
                .HasMaxLength(100)
                .HasColumnName("ward");

            entity.HasOne(d => d.User).WithMany(p => p.SenderInformations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_SenderInformation_User");
        });

        modelBuilder.Entity<ShippingFee>(entity =>
        {
            entity.HasKey(e => e.ShippingFeeId).HasName("PK_ShippingFee");

            entity.ToTable("Shipping_Fee");

            entity.Property(e => e.ShippingFeeId).HasColumnName("shipping_fee_id");
            entity.Property(e => e.BaseFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("base_fee");
            entity.Property(e => e.DistanceRangeFrom)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("distance_range_from");
            entity.Property(e => e.DistanceRangeTo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("distance_range_to");
            entity.Property(e => e.EstimatedTime)
                .HasMaxLength(50)
                .HasColumnName("estimated_time");
            entity.Property(e => e.ServiceCode)
                .HasMaxLength(10)
                .HasColumnName("service_code");
            entity.Property(e => e.WeightClass).HasColumnName("weight_class");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasIndex(e => e.StaffId, "UQ_Staff_StaffId").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__Staff__F3DBC572B8970878").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.AvatarImage)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("avatar_image");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(155)
                .HasColumnName("full_name");
            entity.Property(e => e.IdentityCard)
                .HasMaxLength(15)
                .HasColumnName("identity_card");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.JobTitleId).HasColumnName("job_title_id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Password)
                .HasMaxLength(155)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.StaffId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("staff_id");
            entity.Property(e => e.Username)
                .HasMaxLength(155)
                .HasColumnName("username");

            entity.HasOne(d => d.JobTitle).WithMany(p => p.Staff)
                .HasForeignKey(d => d.JobTitleId)
                .HasConstraintName("FK_Staff_JobTitle");
        });

        modelBuilder.Entity<Truck>(entity =>
        {
            entity.ToTable("Truck");

            entity.Property(e => e.TruckId).HasColumnName("truck_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.GarageId).HasColumnName("garage_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.LastMaintenanceDate)
                .HasColumnType("datetime")
                .HasColumnName("last_maintenance_date");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("model");
            entity.Property(e => e.TruckLicensePlate)
                .HasMaxLength(50)
                .HasColumnName("truck_license_plate");

            entity.HasOne(d => d.Garage).WithMany(p => p.Trucks)
                .HasForeignKey(d => d.GarageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Truck_Garage");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.UserId, "UQ_User_UserId").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__User__F3DBC572766FAF1D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.AvatarImage)
                .HasMaxLength(2048)
                .IsUnicode(false)
                .HasColumnName("avatar_image");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(155)
                .HasColumnName("full_name");
            entity.Property(e => e.IdentityCard)
                .HasMaxLength(15)
                .HasColumnName("identity_card");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.Password)
                .HasMaxLength(155)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.UserId)
                .HasDefaultValueSql("(newsequentialid())")
                .HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(155)
                .HasColumnName("username");

            entity.HasMany(d => d.VoucherPromotions).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserVoucherPromotion",
                    r => r.HasOne<VoucherPromotion>().WithMany()
                        .HasForeignKey("VoucherPromotionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserVoucherPromotion_VoucherPromotion"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_UserVoucherPromotion_User"),
                    j =>
                    {
                        j.HasKey("VoucherPromotionId", "Id").HasName("PK_UserVoucherPromotion");
                        j.ToTable("User_VoucherPromotion");
                        j.IndexerProperty<int>("VoucherPromotionId").HasColumnName("voucher_promotion_id");
                        j.IndexerProperty<int>("Id").HasColumnName("user_id");
                    });
        });

        modelBuilder.Entity<VoucherPromotion>(entity =>
        {
            entity.HasKey(e => e.VoucherPromotionId).HasName("PK_VoucherPromotion");

            entity.ToTable("Voucher_Promotion");

            entity.Property(e => e.VoucherPromotionId).HasColumnName("voucher_promotion_id");
            entity.Property(e => e.PromotionRate)
                .HasColumnType("decimal")
                .HasColumnName("promotion_rate");
            entity.Property(e => e.VoucherPromotionCode)
                .HasMaxLength(100)
                .HasColumnName("voucher_promotion_code");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
