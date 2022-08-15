using Booking.Domain.Models;

namespace Booking.Domain.Interfaces.Queries
{
    public interface IReservationQueries : IQueries<Reservation>
    {
        Task<IList<Reservation>> FindByUserId(Guid userId);
        Task<Reservation> FindByUserReservationId(Guid userId, Guid reservationId);
        Task<IList<Reservation>> FindByRoomAndRange(Guid roomId, DateTime from, DateTime to);
    }
}
