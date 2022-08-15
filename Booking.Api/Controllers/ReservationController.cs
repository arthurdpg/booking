using Booking.Application.Interfaces;
using Booking.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ApiController
    {
        private readonly IReservationAppService _reservationAppService;

        public ReservationController(IReservationAppService reservationAppService)
        {
            _reservationAppService = reservationAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ManageReservationViewModel model)
        {
            return!ModelState.IsValid? CustomResponse(ModelState) : CustomResponse(await _reservationAppService.Create(model));
        }

        [HttpPut("{reservationId:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid reservationId, [FromBody] ManageReservationViewModel model)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _reservationAppService.Update(reservationId, model));
        }

        [HttpGet("user/{userId}")]
        public async Task<IList<ReservationViewModel>> GetByUser([FromRoute] string userId)
        {
            return await _reservationAppService.GetByUserId(userId);
        }

        [HttpGet("user/{userId}/{reservationId:guid}")]
        public async Task<ReservationViewModel> Get([FromRoute] string userId, [FromRoute] Guid reservationId)
        {
            return await _reservationAppService.GetByUserReservationId(userId, reservationId);
        }

        [HttpDelete("user/{userId}/{reservationId:guid}")]
        public async Task<IActionResult> Delete([FromRoute] string userId, [FromRoute] Guid reservationId)
        {
            return CustomResponse(await _reservationAppService.Delete(userId, reservationId));
        }
    }
}
