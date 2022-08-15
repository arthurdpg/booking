using Booking.Application.ViewModels;

namespace Booking.Web.Models
{
    public class MyReservationsViewModel
    {
        public IList<ReservationViewModel> Reservations { get; set; }
        public IList<string> Messages { get; set; }
    }
}