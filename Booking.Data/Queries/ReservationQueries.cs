using Booking.Data.Contexts;
using Booking.Domain.Interfaces.Queries;
using Booking.Domain.Models;

namespace Booking.Data.Queries
{
    public class ReservationQueries : BaseQuery<Reservation>, IReservationQueries
    {
        public ReservationQueries(BookingContext db) : base(db) { }
    }
}
