using Booking.Application.ViewModels;

namespace Booking.Application.Interfaces
{
    public interface IRoomAppService
    {
        Task<IList<RoomViewModel>> GetAvailabilityByRange(DateTime from, DateTime to);
    }
}
