using Booking.Application.Interfaces;
using Booking.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomAppService _roomAppService;

        public RoomController(IRoomAppService roomAppService)
        {
            _roomAppService = roomAppService;
        }

        [HttpGet(Name = "room-Availability")]
        public async Task<IList<RoomViewModel>> Get(DateTime from, DateTime to)
        {
            return await _roomAppService.GetAvailabilityByRange(from, to);
        }
    }
}
