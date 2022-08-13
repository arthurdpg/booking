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

        [HttpGet("{id:guid}")]
        public async Task<ReservationViewModel> Get([FromRoute] Guid id)
        {
            return await _reservationAppService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ManageReservationViewModel model)
        {
            return!ModelState.IsValid? CustomResponse(ModelState) : CustomResponse(await _reservationAppService.Create(model));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] ManageReservationViewModel model)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _reservationAppService.Update(id, model));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return CustomResponse(await _reservationAppService.Delete(id));
        }

        [HttpGet("user/{id}")]
        public async Task<IList<ReservationViewModel>> GetByUser([FromRoute] string id)
        {
            return await _reservationAppService.GetByUserId(id);
        }
    }
}
