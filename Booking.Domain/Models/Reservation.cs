namespace Booking.Domain.Models
{
    public class Reservation : IDomainModel
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
        public string UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Observations { get; set; }
    }
}
