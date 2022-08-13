using Booking.Application.Interfaces;
using Booking.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ApiController
    {
        private readonly IReservationAppService _reservationAppService;

        public ReservationController(IReservationAppService reservationAppService)
        {
            _reservationAppService = reservationAppService;
        }

        [HttpGet("customer-management/{id:guid}")]
        public async Task<ReservationViewModel> Get(Guid id)
        {
            return await _reservationAppService.GetById(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ReservationViewModel model)
        {
            return!ModelState.IsValid? CustomResponse(ModelState) : CustomResponse(await _reservationAppService.Create(model));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ReservationViewModel model)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _reservationAppService.Update(model));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _reservationAppService.Delete(id));
        }
    }
}
