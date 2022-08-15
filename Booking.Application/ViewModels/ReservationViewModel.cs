namespace Booking.Application.ViewModels
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public RoomViewModel Room { get; set; }
        public string UserId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Observations { get; set; }
    }
}
