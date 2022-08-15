using Booking.Domain.Enums;

namespace Booking.Application.ViewModels
{
    public class RoomViewModel
    {
        public Guid Id { get; set; }
        public RoomType Type { get; set; }
        public int Size { get; set; }
        public Guid HotelId { get; set; }
        public HotelViewModel Hotel { get; set;}
}
}
