using Microsoft.EntityFrameworkCore.Migrations;

namespace TestAPT.Migrations
{
    public partial class IncreaseRangeAmountInDetailsEvenMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "FileDetails",
                type: "decimal(13,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "FileDetails",
                type: "decimal(9,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(13,2)");
        }
    }
}
