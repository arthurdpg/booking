namespace Booking.Domain.Models
{
    public class Reservation : IDomainModel
    {
        public Reservation(Guid id, Guid roomId, string userId, DateTime from, DateTime to, string observations)
        {
            Id = id;
            RoomId = roomId;
            UserId = userId;
            From = from;
            To = to;
            Observations = observations;
        }

        // Empty constructor for EF
        protected Reservation() { }

        public Guid Id { get; private set; }
        public Guid RoomId { get; private set; }
        public Room Room { get; private set; }
        public string UserId { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public string Observations { get; private set; }

        public void Update(DateTime from, DateTime to, string observations)
        {
            From = from;
            To = to;
            Observations = observations;
        }
    }
}
