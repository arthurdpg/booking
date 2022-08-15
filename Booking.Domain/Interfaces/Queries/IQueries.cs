using Booking.Domain.Models;

namespace Booking.Domain.Interfaces.Queries
{
    public interface IQueries<TEntity> where TEntity : IDomainModel
    {
        Task<TEntity> FindById(object id);
    }
}
