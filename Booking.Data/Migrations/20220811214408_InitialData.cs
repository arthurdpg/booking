using Booking.Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Data.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Hotel data
            var hotelId = Guid.NewGuid();
            migrationBuilder.Sql($"INSERT INTO Hotels (Id, Name) VALUES ('{hotelId}', 'Hotel')");

            // Room data
            var roomId = Guid.NewGuid();
            migrationBuilder.Sql($"INSERT INTO Rooms (Id, HotelId, Size, Type) VALUES ('{roomId}', '{hotelId}', 50, {(int)RoomType.StandardDoubleBed})");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Hotels");
            migrationBuilder.Sql("DELETE FROM Rooms");
        }
    }
}
