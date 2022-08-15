using Booking.Domain.Enums;

namespace Booking.Domain.Models
{
    public class Room : IDomainModel
    {
        public Room(Guid id, Hotel hotel, int size)
        {
            Id = id;
            Hotel = hotel;
            Size = size;
        }

        // Empty constructor for EF
        protected Room() { }

        public Guid Id { get; private set; }
        public RoomType Type { get; private set; }
        public int Size { get; private set; }
        public Guid HotelId { get; private set; }
        public Hotel Hotel { get; private set; }
        public IList<Reservation> Reservations { get; private set; }
    }
}
