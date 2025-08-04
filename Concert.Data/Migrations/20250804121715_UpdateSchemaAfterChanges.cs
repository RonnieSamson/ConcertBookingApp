using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Concert.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchemaAfterChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Performances",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "Id",
                keyValue: "1",
                column: "Location",
                value: "Göteborg Arena");

            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "Id",
                keyValue: "2",
                column: "Location",
                value: "Stockholm Globen");

            migrationBuilder.UpdateData(
                table: "Performances",
                keyColumn: "Id",
                keyValue: "3",
                column: "Location",
                value: "Malmö Live");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Performances");
        }
    }
}
