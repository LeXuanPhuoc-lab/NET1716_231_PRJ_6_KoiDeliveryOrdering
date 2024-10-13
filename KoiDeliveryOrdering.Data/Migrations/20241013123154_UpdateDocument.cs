using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiDeliveryOrdering.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryOrder_Document",
                table: "Delivery_Order");

            migrationBuilder.DropIndex(
                name: "UQ__Document__C8FE0D8C5D2DDE9F",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_Order_document_id",
                table: "Delivery_Order");

            migrationBuilder.DropColumn(
                name: "assurrance_fee",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "tax_fee",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "document_id",
                table: "Delivery_Order");

            migrationBuilder.AlterColumn<string>(
                name: "transportation_type",
                table: "Document",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "port_of_loading",
                table: "Document",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "port_of_discharge",
                table: "Document",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "final_destination",
                table: "Document",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155);

            migrationBuilder.AlterColumn<string>(
                name: "exporter_phone",
                table: "Document",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "exporter_name",
                table: "Document",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155);

            migrationBuilder.AlterColumn<string>(
                name: "exporter_address",
                table: "Document",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155);

            migrationBuilder.AlterColumn<string>(
                name: "document_number",
                table: "Document",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dispatch_method",
                table: "Document",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "consignee_phone",
                table: "Document",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "consignee_name",
                table: "Document",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155);

            migrationBuilder.AlterColumn<string>(
                name: "consignee_address",
                table: "Document",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155);

            migrationBuilder.AddColumn<int>(
                name: "DeliveryOrderId",
                table: "Document",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "assigned_to",
                table: "Care_Task",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "completed_at",
                table: "Care_Task",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Care_Task",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "due_date",
                table: "Care_Task",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_recurring",
                table: "Care_Task",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "notes",
                table: "Care_Task",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "priority",
                table: "Care_Task",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "Care_Task",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_DeliveryOrderId",
                table: "Document",
                column: "DeliveryOrderId");

            migrationBuilder.CreateIndex(
                name: "UQ__Document__C8FE0D8C5D2DDE9F",
                table: "Document",
                column: "document_number",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DeliveryOrder",
                table: "Document",
                column: "DeliveryOrderId",
                principalTable: "Delivery_Order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_DeliveryOrder",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_DeliveryOrderId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "UQ__Document__C8FE0D8C5D2DDE9F",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "DeliveryOrderId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "assigned_to",
                table: "Care_Task");

            migrationBuilder.DropColumn(
                name: "completed_at",
                table: "Care_Task");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Care_Task");

            migrationBuilder.DropColumn(
                name: "due_date",
                table: "Care_Task");

            migrationBuilder.DropColumn(
                name: "is_recurring",
                table: "Care_Task");

            migrationBuilder.DropColumn(
                name: "notes",
                table: "Care_Task");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "Care_Task");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Care_Task");

            migrationBuilder.AlterColumn<string>(
                name: "transportation_type",
                table: "Document",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "port_of_loading",
                table: "Document",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "port_of_discharge",
                table: "Document",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "final_destination",
                table: "Document",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "exporter_phone",
                table: "Document",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "exporter_name",
                table: "Document",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "exporter_address",
                table: "Document",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "document_number",
                table: "Document",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "dispatch_method",
                table: "Document",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "consignee_phone",
                table: "Document",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "consignee_name",
                table: "Document",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "consignee_address",
                table: "Document",
                type: "nvarchar(155)",
                maxLength: 155,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(155)",
                oldMaxLength: 155,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "assurrance_fee",
                table: "Document",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "tax_fee",
                table: "Document",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "document_id",
                table: "Delivery_Order",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Document__C8FE0D8C5D2DDE9F",
                table: "Document",
                column: "document_number",
                unique: true,
                filter: "[document_number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_Order_document_id",
                table: "Delivery_Order",
                column: "document_id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryOrder_Document",
                table: "Delivery_Order",
                column: "document_id",
                principalTable: "Document",
                principalColumn: "id");
        }
    }
}
