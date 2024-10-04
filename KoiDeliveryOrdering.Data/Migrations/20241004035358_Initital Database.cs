using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiDeliveryOrdering.Data.Migrations
{
    /// <inheritdoc />
    public partial class InititalDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animal_Type",
                columns: table => new
                {
                    animal_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    animal_type_desc = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalType", x => x.animal_type_id);
                });

            migrationBuilder.CreateTable(
                name: "Care_Task",
                columns: table => new
                {
                    care_task_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    task_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareTask", x => x.care_task_id);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    document_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    document_number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    document_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    issue_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    consignee_name = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    consignee_phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    consignee_address = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    exporter_name = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    exporter_phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    exporter_address = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    dispatch_method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    final_destination = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    transportation_no = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    transportation_type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    port_of_loading = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    port_of_discharge = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    tax_fee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    shipping_fee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    assurrance_fee = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Garage",
                columns: table => new
                {
                    garage_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    garage_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    manager_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    city_province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    street = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    district = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ward = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: true),
                    latitude = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garage", x => x.garage_id);
                });

            migrationBuilder.CreateTable(
                name: "Job_Title",
                columns: table => new
                {
                    job_title_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_title_desc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitle", x => x.job_title_id);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    payment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_method = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.payment_id);
                });

            migrationBuilder.CreateTable(
                name: "Shipping_Fee",
                columns: table => new
                {
                    shipping_fee_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    distance_range_from = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    distance_range_to = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    service_code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    weight_class = table.Column<int>(type: "int", nullable: false),
                    base_fee = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    estimated_time = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingFee", x => x.shipping_fee_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    full_name = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    avatar_image = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: true),
                    identity_card = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    longitude = table.Column<double>(type: "float", nullable: true),
                    latitude = table.Column<double>(type: "float", nullable: true),
                    username = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: true),
                    password = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Voucher_Promotion",
                columns: table => new
                {
                    voucher_promotion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voucher_promotion_code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    promotion_rate = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherPromotion", x => x.voucher_promotion_id);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    animal_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    breed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    color_pattern = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    size = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    age = table.Column<int>(type: "int", nullable: true),
                    estimated_price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    health_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    is_available = table.Column<bool>(type: "bit", nullable: true),
                    origin_country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    image_url = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: true),
                    animal_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.id);
                    table.ForeignKey(
                        name: "PK_Animal_AnimalType",
                        column: x => x.animal_type_id,
                        principalTable: "Animal_Type",
                        principalColumn: "animal_type_id");
                });

            migrationBuilder.CreateTable(
                name: "Document_Detail",
                columns: table => new
                {
                    document_detail_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    document_detail_description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    item_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    item_weight = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    item_category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    item_estimate_price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    item_quantity = table.Column<int>(type: "int", nullable: true),
                    document_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentDetail", x => x.document_detail_id);
                    table.ForeignKey(
                        name: "FK_DocumentDetail_Document",
                        column: x => x.document_id,
                        principalTable: "Document",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Truck",
                columns: table => new
                {
                    truck_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    truck_license_plate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    capacity = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false),
                    garage_id = table.Column<int>(type: "int", nullable: false),
                    last_maintenance_date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Truck", x => x.truck_id);
                    table.ForeignKey(
                        name: "FK_Truck_Garage",
                        column: x => x.garage_id,
                        principalTable: "Garage",
                        principalColumn: "garage_id");
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    staff_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    full_name = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    date_of_birth = table.Column<DateTime>(type: "datetime", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    avatar_image = table.Column<string>(type: "varchar(2048)", unicode: false, maxLength: 2048, nullable: true),
                    identity_card = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    job_title_id = table.Column<int>(type: "int", nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    longitude = table.Column<double>(type: "float", nullable: true),
                    latitude = table.Column<double>(type: "float", nullable: true),
                    username = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: true),
                    password = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.id);
                    table.ForeignKey(
                        name: "FK_Staff_JobTitle",
                        column: x => x.job_title_id,
                        principalTable: "Job_Title",
                        principalColumn: "job_title_id");
                });

            migrationBuilder.CreateTable(
                name: "Sender_Information",
                columns: table => new
                {
                    sender_information_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    sender_name = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    sender_phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    city_province = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    street = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: false),
                    district = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ward = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    longitude = table.Column<double>(type: "float", nullable: true),
                    latitude = table.Column<double>(type: "float", nullable: true),
                    sender_appointment_time = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenderInformation", x => x.sender_information_id);
                    table.ForeignKey(
                        name: "FK_SenderInformation_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User_VoucherPromotion",
                columns: table => new
                {
                    voucher_promotion_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVoucherPromotion", x => new { x.voucher_promotion_id, x.user_id });
                    table.ForeignKey(
                        name: "FK_UserVoucherPromotion_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_UserVoucherPromotion_VoucherPromotion",
                        column: x => x.voucher_promotion_id,
                        principalTable: "Voucher_Promotion",
                        principalColumn: "voucher_promotion_id");
                });

            migrationBuilder.CreateTable(
                name: "Delivery_Order",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    delivery_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    recipient_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    recipient_phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    recipient_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    recipient_longitude = table.Column<double>(type: "float", nullable: true),
                    recipient_latitude = table.Column<double>(type: "float", nullable: true),
                    recipient_appointment_time = table.Column<string>(type: "nvarchar(155)", maxLength: 155, nullable: true),
                    create_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    delivery_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    order_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    tax_fee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    payment_id = table.Column<int>(type: "int", nullable: false),
                    is_purchased = table.Column<bool>(type: "bit", nullable: true),
                    is_sender_purchase = table.Column<bool>(type: "bit", nullable: false),
                    is_international = table.Column<bool>(type: "bit", nullable: false),
                    voucher_promotion_id = table.Column<int>(type: "int", nullable: true),
                    shipping_fee_id = table.Column<int>(type: "int", nullable: false),
                    sender_information_id = table.Column<int>(type: "int", nullable: false),
                    document_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryOrder", x => x.id);
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_Document",
                        column: x => x.document_id,
                        principalTable: "Document",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_Payment",
                        column: x => x.payment_id,
                        principalTable: "Payment",
                        principalColumn: "payment_id");
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_SenderInformation",
                        column: x => x.sender_information_id,
                        principalTable: "Sender_Information",
                        principalColumn: "sender_information_id");
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_ShippingFee",
                        column: x => x.shipping_fee_id,
                        principalTable: "Shipping_Fee",
                        principalColumn: "shipping_fee_id");
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_VoucherPromotion",
                        column: x => x.voucher_promotion_id,
                        principalTable: "Voucher_Promotion",
                        principalColumn: "voucher_promotion_id");
                });

            migrationBuilder.CreateTable(
                name: "Delivery_Order_Detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    delivery_order_detail_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    animal_id = table.Column<int>(type: "int", nullable: false),
                    delivery_order_id = table.Column<int>(type: "int", nullable: false),
                    pre_delivery_health_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    post_delivery_health_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Animal",
                        column: x => x.animal_id,
                        principalTable: "Animal",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order",
                        column: x => x.delivery_order_id,
                        principalTable: "Delivery_Order",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Order_Assignment",
                columns: table => new
                {
                    order_assignment_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    delivery_order_id = table.Column<int>(type: "int", nullable: false),
                    driver_id = table.Column<int>(type: "int", nullable: false),
                    fish_carer_id = table.Column<int>(type: "int", nullable: false),
                    assigned_truck_id = table.Column<int>(type: "int", nullable: false),
                    assigned_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    delivery_status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAssignment", x => x.order_assignment_id);
                    table.ForeignKey(
                        name: "FK_OrderAssignment_Order",
                        column: x => x.delivery_order_id,
                        principalTable: "Delivery_Order",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_OrderAssignment_Staff",
                        column: x => x.driver_id,
                        principalTable: "Staff",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_OrderAssignment_Truck",
                        column: x => x.assigned_truck_id,
                        principalTable: "Truck",
                        principalColumn: "truck_id");
                    table.ForeignKey(
                        name: "FK_OrderAssignment_User",
                        column: x => x.fish_carer_id,
                        principalTable: "Staff",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Daily_Care_Schedule",
                columns: table => new
                {
                    daily_care_schedule_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    care_task_id = table.Column<int>(type: "int", nullable: false),
                    task_frequency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    recommended_value = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    deliver_order_detail_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyCareSchedule", x => x.daily_care_schedule_id);
                    table.ForeignKey(
                        name: "FK_DailyCareSchedule_CareTask",
                        column: x => x.care_task_id,
                        principalTable: "Care_Task",
                        principalColumn: "care_task_id");
                    table.ForeignKey(
                        name: "FK_DailyCareSchedule_DeliveryOrderDetail",
                        column: x => x.deliver_order_detail_id,
                        principalTable: "Delivery_Order_Detail",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Care_Log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    daily_care_schedule_id = table.Column<int>(type: "int", nullable: false),
                    log_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    actual_value = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    staff_comments = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    staff_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareLog", x => x.id);
                    table.ForeignKey(
                        name: "FK_CareLog_DailyCareSchedule",
                        column: x => x.daily_care_schedule_id,
                        principalTable: "Daily_Care_Schedule",
                        principalColumn: "daily_care_schedule_id");
                    table.ForeignKey(
                        name: "FK_CareLog_Staff",
                        column: x => x.staff_id,
                        principalTable: "Staff",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animal_animal_type_id",
                table: "Animal",
                column: "animal_type_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Animal_AnimalId",
                table: "Animal",
                column: "animal_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Care_Log_daily_care_schedule_id",
                table: "Care_Log",
                column: "daily_care_schedule_id");

            migrationBuilder.CreateIndex(
                name: "IX_Care_Log_staff_id",
                table: "Care_Log",
                column: "staff_id");

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Care_Schedule_care_task_id",
                table: "Daily_Care_Schedule",
                column: "care_task_id");

            migrationBuilder.CreateIndex(
                name: "IX_Daily_Care_Schedule_deliver_order_detail_id",
                table: "Daily_Care_Schedule",
                column: "deliver_order_detail_id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Order_document_id",
                table: "Delivery_Order",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Order_payment_id",
                table: "Delivery_Order",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Order_sender_information_id",
                table: "Delivery_Order",
                column: "sender_information_id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Order_shipping_fee_id",
                table: "Delivery_Order",
                column: "shipping_fee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Order_voucher_promotion_id",
                table: "Delivery_Order",
                column: "voucher_promotion_id");

            migrationBuilder.CreateIndex(
                name: "UQ_DeliveryOrder_OrderId",
                table: "Delivery_Order",
                column: "delivery_order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Order_Detail_animal_id",
                table: "Delivery_Order_Detail",
                column: "animal_id");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Order_Detail_delivery_order_id",
                table: "Delivery_Order_Detail",
                column: "delivery_order_id");

            migrationBuilder.CreateIndex(
                name: "UQ_OrderDetail_OrderDetailId",
                table: "Delivery_Order_Detail",
                column: "delivery_order_detail_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Document__C8FE0D8C5D2DDE9F",
                table: "Document",
                column: "document_number",
                unique: true,
                filter: "[document_number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_Document",
                table: "Document",
                column: "document_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_Detail_document_id",
                table: "Document_Detail",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Assignment_assigned_truck_id",
                table: "Order_Assignment",
                column: "assigned_truck_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Assignment_delivery_order_id",
                table: "Order_Assignment",
                column: "delivery_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Assignment_driver_id",
                table: "Order_Assignment",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Assignment_fish_carer_id",
                table: "Order_Assignment",
                column: "fish_carer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sender_Information_user_id",
                table: "Sender_Information",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_job_title_id",
                table: "Staff",
                column: "job_title_id");

            migrationBuilder.CreateIndex(
                name: "UQ__Staff__F3DBC572B8970878",
                table: "Staff",
                column: "username",
                unique: true,
                filter: "[username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_Staff_StaffId",
                table: "Staff",
                column: "staff_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Truck_garage_id",
                table: "Truck",
                column: "garage_id");

            migrationBuilder.CreateIndex(
                name: "UQ__User__F3DBC572766FAF1D",
                table: "User",
                column: "username",
                unique: true,
                filter: "[username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ_User_UserId",
                table: "User",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_VoucherPromotion_user_id",
                table: "User_VoucherPromotion",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Care_Log");

            migrationBuilder.DropTable(
                name: "Document_Detail");

            migrationBuilder.DropTable(
                name: "Order_Assignment");

            migrationBuilder.DropTable(
                name: "User_VoucherPromotion");

            migrationBuilder.DropTable(
                name: "Daily_Care_Schedule");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Truck");

            migrationBuilder.DropTable(
                name: "Care_Task");

            migrationBuilder.DropTable(
                name: "Delivery_Order_Detail");

            migrationBuilder.DropTable(
                name: "Job_Title");

            migrationBuilder.DropTable(
                name: "Garage");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "Delivery_Order");

            migrationBuilder.DropTable(
                name: "Animal_Type");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "Sender_Information");

            migrationBuilder.DropTable(
                name: "Shipping_Fee");

            migrationBuilder.DropTable(
                name: "Voucher_Promotion");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
