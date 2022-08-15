using Booking.Application.ViewModels;
using Booking.Domain.Configuration;
using Booking.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;

namespace Booking.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ApiConfig _config;

        public HomeController(ApiConfig config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            return View(new RoomsViewModel());
        }

        [HttpPost("room-availability")]
        public async Task<IActionResult> GetRoomAvailability([FromForm] RoomsViewModel model)
        {
            using (var httpClient = GetHttpClient())
            {
                var response = httpClient.GetAsync(string.Format(_config.Room + "room-availability?from={0}&to={1}"
                    , HttpUtility.UrlEncode(model.From.Value.ToShortDateString())
                    , HttpUtility.UrlEncode(model.To.Value.ToShortDateString()))).Result;

                if (response.IsSuccessStatusCode)
                {
                    var rooms = await response.Content.ReadAsAsync<IList<RoomAvailabilityViewModel>>();
                    return View("Index", new RoomsViewModel { From = model.From, To = model.To, Rooms = rooms });
                }
            }

            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}