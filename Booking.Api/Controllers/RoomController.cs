using Booking.Application.Interfaces;
using Booking.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomAppService _roomAppService;

        public RoomController(IRoomAppService roomAppService)
        {
            _roomAppService = roomAppService;
        }

        [HttpGet("room-availability")]
        public async Task<IList<RoomAvailabilityViewModel>> Get(DateTime from, DateTime to)
        {
            return await _roomAppService.GetAvailabilityByRange(from, to);
        }
    }
}
