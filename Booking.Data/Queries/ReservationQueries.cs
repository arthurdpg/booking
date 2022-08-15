using Booking.Data.Contexts;
using Booking.Domain.Interfaces.Queries;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data.Queries
{
    public class ReservationQueries : BaseQuery<Reservation>, IReservationQueries
    {
        public ReservationQueries(BookingContext db) : base(db) { }

        public async Task<IList<Reservation>> FindByUserId(Guid userId)
        {
            return await DbSet
                .Include(r => r.Room)
                .Include(r => r.Room.Hotel)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<Reservation> FindByUserReservationId(Guid userId, Guid reservationId)
        {
            return await DbSet
                .FirstOrDefaultAsync(r => r.UserId == userId && r.Id == reservationId);
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
