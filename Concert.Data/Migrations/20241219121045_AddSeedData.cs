using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcertBookingApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "This is the first concert", "First Concert" },
                    { 2, "This is the second concert", "Second Concert" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Concerts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
