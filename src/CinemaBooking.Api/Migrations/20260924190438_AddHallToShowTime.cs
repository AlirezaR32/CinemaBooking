using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaBooking.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddHallToShowTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HallId",
                table: "ShowTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShowTimes_HallId",
                table: "ShowTimes",
                column: "HallId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShowTimes_Halls_HallId",
                table: "ShowTimes",
                column: "HallId",
                principalTable: "Halls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShowTimes_Halls_HallId",
                table: "ShowTimes");

            migrationBuilder.DropIndex(
                name: "IX_ShowTimes_HallId",
                table: "ShowTimes");

            migrationBuilder.DropColumn(
                name: "HallId",
                table: "ShowTimes");
        }
    }
}
