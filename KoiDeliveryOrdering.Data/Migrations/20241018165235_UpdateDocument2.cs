using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoiDeliveryOrdering.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDocument2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "item_weight",
                table: "Document_Detail",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");

            migrationBuilder.AlterColumn<double>(
                name: "item_estimate_price",
                table: "Document_Detail",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "item_weight",
                table: "Document_Detail",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "item_estimate_price",
                table: "Document_Detail",
                type: "decimal(18,0)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
