using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Concert.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithExtendedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Concerts",
                columns: table => new
                {
                    ConcertId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Concerts", x => x.ConcertId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ConcertId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performances_Concerts_ConcertId",
                        column: x => x.ConcertId,
                        principalTable: "Concerts",
                        principalColumn: "ConcertId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PerformanceId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Performances_PerformanceId",
                        column: x => x.PerformanceId,
                        principalTable: "Performances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "ConcertId", "Description", "Title" },
                values: new object[,]
                {
                    { "1", "A night of rock music with legendary bands", "Rock Night" },
                    { "2", "Smooth jazz evening with world-class musicians", "Jazz Night" },
                    { "3", "The biggest pop hits performed live", "Pop Night" },
                    { "4", "Beautiful classical music performed by symphony orchestra", "Classical Symphony" },
                    { "5", "High-energy electronic music and DJ performances", "Electronic Dance Festival" },
                    { "6", "Intimate acoustic performances by indie artists", "Acoustic Unplugged" },
                    { "7", "Heavy metal concert featuring brutal bands", "Metal Mayhem" },
                    { "8", "Country classics and modern hits", "Country Music Night" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { "1", "John123@example.com", "John", "John123" },
                    { "2", "bob@example.com", "Bob Bengtsson", "anotherpassword" },
                    { "3", "alice@example.com", "Alice", "securepassword" }
                });

            migrationBuilder.InsertData(
                table: "Performances",
                columns: new[] { "Id", "ConcertId", "EndTime", "Location", "StartTime" },
                values: new object[,]
                {
                    { "1", "1", new DateTime(2025, 3, 15, 22, 0, 0, 0, DateTimeKind.Unspecified), "Göteborg Arena", new DateTime(2025, 3, 15, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "10", "4", new DateTime(2025, 6, 3, 21, 30, 0, 0, DateTimeKind.Unspecified), "Konserthuset Stockholm", new DateTime(2025, 6, 3, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "11", "4", new DateTime(2025, 6, 10, 21, 0, 0, 0, DateTimeKind.Unspecified), "Göteborgs Konserthus", new DateTime(2025, 6, 10, 18, 30, 0, 0, DateTimeKind.Unspecified) },
                    { "12", "5", new DateTime(2025, 7, 13, 2, 0, 0, 0, DateTimeKind.Unspecified), "Ullevi Göteborg", new DateTime(2025, 7, 12, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "13", "5", new DateTime(2025, 7, 20, 1, 0, 0, 0, DateTimeKind.Unspecified), "Stockholm Stadion", new DateTime(2025, 7, 19, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "14", "5", new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Malmö Festivalen", new DateTime(2025, 7, 26, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "15", "6", new DateTime(2025, 8, 5, 21, 0, 0, 0, DateTimeKind.Unspecified), "Cirkus Stockholm", new DateTime(2025, 8, 5, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "16", "6", new DateTime(2025, 8, 12, 20, 30, 0, 0, DateTimeKind.Unspecified), "Liseberg Stora Scen", new DateTime(2025, 8, 12, 18, 30, 0, 0, DateTimeKind.Unspecified) },
                    { "17", "6", new DateTime(2025, 8, 19, 21, 30, 0, 0, DateTimeKind.Unspecified), "Malmö Live", new DateTime(2025, 8, 19, 19, 30, 0, 0, DateTimeKind.Unspecified) },
                    { "18", "7", new DateTime(2025, 9, 7, 23, 0, 0, 0, DateTimeKind.Unspecified), "Hovet Stockholm", new DateTime(2025, 9, 7, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "19", "7", new DateTime(2025, 9, 14, 22, 0, 0, 0, DateTimeKind.Unspecified), "Partille Arena", new DateTime(2025, 9, 14, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "2", "1", new DateTime(2025, 3, 20, 23, 0, 0, 0, DateTimeKind.Unspecified), "Stockholm Globen", new DateTime(2025, 3, 20, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "20", "8", new DateTime(2025, 10, 4, 22, 0, 0, 0, DateTimeKind.Unspecified), "Gröna Lund Stockholm", new DateTime(2025, 10, 4, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "21", "8", new DateTime(2025, 10, 11, 21, 30, 0, 0, DateTimeKind.Unspecified), "Liseberg Stora Scen", new DateTime(2025, 10, 11, 18, 30, 0, 0, DateTimeKind.Unspecified) },
                    { "22", "8", new DateTime(2025, 10, 18, 22, 30, 0, 0, DateTimeKind.Unspecified), "Malmö Live", new DateTime(2025, 10, 18, 19, 30, 0, 0, DateTimeKind.Unspecified) },
                    { "3", "1", new DateTime(2025, 3, 25, 21, 30, 0, 0, DateTimeKind.Unspecified), "Malmö Live", new DateTime(2025, 3, 25, 18, 30, 0, 0, DateTimeKind.Unspecified) },
                    { "4", "2", new DateTime(2025, 4, 5, 22, 0, 0, 0, DateTimeKind.Unspecified), "Konserthuset Stockholm", new DateTime(2025, 4, 5, 19, 30, 0, 0, DateTimeKind.Unspecified) },
                    { "5", "2", new DateTime(2025, 4, 10, 22, 30, 0, 0, DateTimeKind.Unspecified), "Göteborgs Konserthus", new DateTime(2025, 4, 10, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "6", "2", new DateTime(2025, 4, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), "Malmö Opera", new DateTime(2025, 4, 15, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "7", "3", new DateTime(2025, 5, 1, 22, 30, 0, 0, DateTimeKind.Unspecified), "Friends Arena", new DateTime(2025, 5, 1, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "8", "3", new DateTime(2025, 5, 8, 21, 0, 0, 0, DateTimeKind.Unspecified), "Scandinavium", new DateTime(2025, 5, 8, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "9", "3", new DateTime(2025, 5, 15, 22, 0, 0, 0, DateTimeKind.Unspecified), "Malmö Arena", new DateTime(2025, 5, 15, 19, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "CustomerEmail", "CustomerName", "PerformanceId" },
                values: new object[,]
                {
                    { "1", new DateTime(2025, 2, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John Doe", "1" },
                    { "2", new DateTime(2025, 2, 20, 18, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane Smith", "4" },
                    { "3", new DateTime(2025, 3, 1, 19, 0, 0, 0, DateTimeKind.Unspecified), "bob.johnson@example.com", "Bob Johnson", "7" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PerformanceId",
                table: "Bookings",
                column: "PerformanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_ConcertId",
                table: "Performances",
                column: "ConcertId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "Concerts");
        }
    }
}
