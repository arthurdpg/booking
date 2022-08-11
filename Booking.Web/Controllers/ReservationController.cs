using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }

        public IActionResult MyReservations()
        {
            return View();
        }
    }
}
