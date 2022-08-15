using Booking.Application.Interfaces;
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
        public async Task<IActionResult> Get(DateTime from, DateTime to)
        {
            if (from.Date <= DateTime.Now.Date || from.Date > to.Date)
                return BadRequest();

            return Ok(await _roomAppService.GetAvailabilityByRange(from, to));
        }
    }
}
