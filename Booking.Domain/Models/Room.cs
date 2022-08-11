using Booking.Domain.Enums;

namespace Booking.Domain.Models
{
    public class Room
    {
        public Room(Guid id, Hotel hotel, int size, IList<Facility> facilities)
        {
            Id = id;
            Hotel = hotel;
            Size = size;
            Facilities = facilities;
        }

        public Guid Id { get; private set; }
        public RoomType Type { get; private set; }
        public int Size { get; private set; }
        public Guid HotelId { get; private set; }
        public Hotel Hotel { get; private set; }
        public IList<Facility> Facilities { get; private set; }
    }
}
