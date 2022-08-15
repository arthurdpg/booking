using Booking.Domain.Enums;

namespace Booking.Application.ViewModels
{
    public class RoomViewModel
    {
        public Guid Id { get; private set; }
        public RoomType Type { get; private set; }
        public int Size { get; private set; }
        public Guid HotelId { get; private set; }
    }
}
