using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
    }
}
