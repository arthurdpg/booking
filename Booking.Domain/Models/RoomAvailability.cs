namespace Booking.Domain.Models
{
    public class RoomAvailability
    {
        public RoomAvailability( Room room, bool available)
        {
            Room = room;
            Available = available;
        }

        public Room Room { get; private set; }
        public bool Available { get; private set; }
    }
}
