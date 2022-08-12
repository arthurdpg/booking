using AutoMapper;
using Booking.Application.Interfaces;
using Booking.Application.ViewModels;
using Booking.Domain.Interfaces.Queries;

namespace Booking.Application.Services
{
    public class RoomAppService : IRoomAppService
    {
        private readonly IMapper _mapper;
        private readonly IRoomQueries _roomQueries;

        public RoomAppService(IMapper mapper, IRoomQueries roomQueries)
        {
            _mapper = mapper;
            _roomQueries = roomQueries;
        }

        public async Task<IList<RoomViewModel>> GetAvailabilityByRange(DateTime from, DateTime to)
        {
            return _mapper.Map<IList<RoomViewModel>>(await _roomQueries.GetAvailabilityByRange(from, to));
        }
    }
}
