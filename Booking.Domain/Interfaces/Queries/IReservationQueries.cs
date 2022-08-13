using Booking.Domain.Models;

namespace Booking.Domain.Interfaces.Queries
{
    public interface IReservationQueries : IQueries<Reservation>
    {
        Task<IList<Reservation>> FindByUserId(string userId);
        Task<IList<Reservation>> FindByRoomAndRange(Guid roomId, DateTime from, DateTime to);
    }
}
