using Booking.Application.ViewModels;

namespace Booking.Application.Interfaces
{
    public interface IRoomAppService
    {
        Task<IList<RoomAvailabilityViewModel>> GetAvailabilityByRange(DateTime from, DateTime to);
    }
}
