using Booking.Application.ViewModels;

namespace Booking.Web.Models
{
    public class RoomAvailabilityViewModel
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public IList<RoomViewModel> Rooms { get; set; }
    }
}