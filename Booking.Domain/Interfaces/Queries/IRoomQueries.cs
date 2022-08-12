using Booking.Domain.Models;

namespace Booking.Domain.Interfaces.Queries
{
    public interface IRoomQueries
    {
        Task<IList<Room>> GetAvailabilityByRange(DateTime from, DateTime to);
    }
}
