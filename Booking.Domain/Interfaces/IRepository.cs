using Booking.Domain.Models;

namespace Booking.Domain.Interfaces
{
    public interface IRepository<in TEntity> where TEntity : IDomainModel
    {
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
