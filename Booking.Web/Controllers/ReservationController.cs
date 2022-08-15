using Booking.Application.ViewModels;
using Booking.Domain.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Booking.Web.Controllers
{
    public class ReservationController : BaseController
    {
        private readonly ApiConfig _config;
        public ReservationController(ApiConfig config)
        {
            _config = config;
        }

        public IActionResult Details()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> MyReservations()
        {
            using (var httpClient = GetHttpClient())
            {
                var response = httpClient.GetAsync(string.Format(_config.MyReservations, GetUserId())).Result;

                if (response.IsSuccessStatusCode)
                {
                    var reservations = await response.Content.ReadAsAsync<IList<ReservationViewModel>>();
                }
            }
            return View();
        }
    }
}
