using Booking.Data.Contexts;
using Booking.Domain.Interfaces.Queries;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data.Queries
{
    public class RoomQueries : BaseQuery<Room>, IRoomQueries
    {
        public RoomQueries(BookingContext db) : base(db) { }

        public async Task<IList<RoomAvailability>> GetAvailabilityByRange(DateTime from, DateTime to)
        {
            var result = await DbSet
                .Include(room => room.Reservations)
                .Select(room => new RoomAvailability (room, !room.Reservations.Any(x => (x.From >= from && x.From <= to) || (x.To >= from && x.To <= to))))
                .ToListAsync();

            return result;
        }
    }
}
