using Microsoft.EntityFrameworkCore.Migrations;

namespace TestAPT.Migrations
{
    public partial class RemovedUnusedFieldTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUploadId",
                table: "FileDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileUploadId",
                table: "FileDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
