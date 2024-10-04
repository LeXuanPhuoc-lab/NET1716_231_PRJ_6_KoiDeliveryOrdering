using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiDeliveryOrdering.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedeliveryorderdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaregiverName",
                table: "Daily_Care_Schedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCritical",
                table: "Daily_Care_Schedule",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastPerformedDate",
                table: "Daily_Care_Schedule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Daily_Care_Schedule",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskDuration",
                table: "Daily_Care_Schedule",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaregiverName",
                table: "Daily_Care_Schedule");

            migrationBuilder.DropColumn(
                name: "IsCritical",
                table: "Daily_Care_Schedule");

            migrationBuilder.DropColumn(
                name: "LastPerformedDate",
                table: "Daily_Care_Schedule");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Daily_Care_Schedule");

            migrationBuilder.DropColumn(
                name: "TaskDuration",
                table: "Daily_Care_Schedule");
        }
    }
}
