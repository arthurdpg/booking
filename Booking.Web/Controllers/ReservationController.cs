using Booking.Application.ViewModels;
using Booking.Domain.Configuration;
using Booking.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly ApiConfig _config;
        public ReservationController(ApiConfig config)
        {
            _config = config;
        }

        [Authorize]
        public async Task<IActionResult> MyReservations()
        {
            using (var httpClient = GetHttpClient())
            {
                var response = httpClient.GetAsync(string.Format(_config.Reservation + "user/{0}", GetUserId())).Result;

                if (response.IsSuccessStatusCode)
                {
                    var reservations = await response.Content.ReadAsAsync<IList<ReservationViewModel>>();
                    return View(new MyReservationsViewModel { Reservations = reservations });
                }
            }
            return View(new MyReservationsViewModel());
        }
    }
}
