using Booking.Application.ViewModels;

namespace Booking.Web.Models
{
    public class RoomsViewModel
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public Guid? SelectedRoom { get; set; }
        public IList<RoomAvailabilityViewModel> Rooms { get; set; }
    }
}