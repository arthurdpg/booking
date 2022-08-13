using Booking.Data.Contexts;
using Booking.Domain.Interfaces.Queries;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data.Queries
{
    public class ReservationQueries : BaseQuery<Reservation>, IReservationQueries
    {
        public ReservationQueries(BookingContext db) : base(db) { }

        public async Task<IList<Reservation>> FindByUserId(string userId)
        {
            return await DbSet
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
        public async Task<IList<Reservation>> FindByRoomAndRange(Guid roomId, DateTime from, DateTime to)
        {
            return await DbSet
                .Where(r => r.RoomId == roomId)
                .Where(r => (r.From >= from && r.From <= to) || (r.To >= from && r.To <= to))
                .ToListAsync();
        }
    }
}
