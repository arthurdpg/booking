using Booking.Data.Contexts;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data.Queries
{
    public class BaseQuery<TEntity> where TEntity : class, IDomainModel
    {
        protected readonly BookingContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public BaseQuery(BookingContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task<TEntity> FindById(object id)
        {
            return await DbSet.FindAsync(id);
        }
    }
}
