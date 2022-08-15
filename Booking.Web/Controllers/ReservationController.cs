using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Booking.Web.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }

        [Authorize]
        public IActionResult MyReservations()
        {
            using (var httpClient = GetHttpClient())
            {
                var response = httpClient.GetAsync("http://booking.api:50002/api/Reservation/user/arthurdpg@gmail.com/38393000-418d-4070-8a45-83a6b7401383").Result;

            }
            return View();
        }

        private HttpClient GetHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
