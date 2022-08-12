using Booking.Data.Contexts;
using Booking.Domain.Interfaces;
using Booking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IDomainModel
    {
        protected readonly BookingContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(BookingContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }
    }
}
