using Booking.Data.Contexts;
using Booking.Domain.Interfaces.Queries;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data.Queries
{
    public class RoomQueries : BaseQuery<Room>, IRoomQueries
    {
        public RoomQueries(BookingContext db) : base(db) { }

        public async Task<IList<Room>> GetAvailabilityByRange(DateTime from, DateTime to)
        {
            return await DbSet
                .Include(r => r.Facilities)
                .ToListAsync();
        }
    }
}
