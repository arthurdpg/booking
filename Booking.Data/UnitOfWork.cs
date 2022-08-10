using Booking.Data.Contexts;
using Booking.Domain.Interfaces;

namespace Booking.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookingContext _context;

        public UnitOfWork(BookingContext context)
        {
            _context = context;
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
