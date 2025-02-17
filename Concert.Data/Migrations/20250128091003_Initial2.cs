using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Concert.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "Id", "Email", "Name", "UserId" },
                values: new object[,]
                {
                    { "1", "alice@example.com", "John", "1" },
                    { "12", "Chrilleeeee@example.com", "Chrilleeeee", "12" },
                    { "123", "RonniePonnie@example.com", "Ronnieee", "123" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Booking",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Booking",
                keyColumn: "Id",
                keyValue: "12");

            migrationBuilder.DeleteData(
                table: "Booking",
                keyColumn: "Id",
                keyValue: "123");
        }
    }
}
