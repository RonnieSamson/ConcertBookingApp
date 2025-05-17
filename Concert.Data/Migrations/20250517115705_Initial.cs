using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Concert.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    UserId = table.Column<string>(type: "nvarchar(36)", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Concerts",
                columns: new[] { "ConcertId", "Description", "Title" },
                values: new object[,]
                {
                    { "1", "A night of rock music", "Rock Night" },
                    { "2", "A night of jazz music", "Jazz Night" },
                    { "3", "A night of pop music", "Pop Night" }
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
                columns: new[] { "Id", "ConcertId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { "1", "1", new DateTime(2024, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "2", "2", new DateTime(2024, 1, 2, 20, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 2, 18, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "3", "3", new DateTime(2024, 1, 3, 21, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 3, 19, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "PerformanceId", "UserId" },
                values: new object[,]
                {
                    { "1", new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "1", "1" },
                    { "2", new DateTime(2024, 1, 2, 18, 0, 0, 0, DateTimeKind.Unspecified), "2", "2" },
                    { "3", new DateTime(2024, 1, 3, 19, 0, 0, 0, DateTimeKind.Unspecified), "3", "3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PerformanceId",
                table: "Bookings",
                column: "PerformanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

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
                name: "Performances");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Concerts");
        }
    }
}
