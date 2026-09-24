using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaBooking.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookingSeats_ShowTimeId",
                table: "BookingSeats");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "BookingSeats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowTimeId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_ShowTimes_ShowTimeId",
                        column: x => x.ShowTimeId,
                        principalTable: "ShowTimes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeats_BookingId",
                table: "BookingSeats",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeats_ShowTimeId_SeatId",
                table: "BookingSeats",
                columns: new[] { "ShowTimeId", "SeatId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ShowTimeId",
                table: "Bookings",
                column: "ShowTimeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSeats_Bookings_BookingId",
                table: "BookingSeats",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingSeats_Bookings_BookingId",
                table: "BookingSeats");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_BookingSeats_BookingId",
                table: "BookingSeats");

            migrationBuilder.DropIndex(
                name: "IX_BookingSeats_ShowTimeId_SeatId",
                table: "BookingSeats");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "BookingSeats");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeats_ShowTimeId",
                table: "BookingSeats",
                column: "ShowTimeId");
        }
    }
}
