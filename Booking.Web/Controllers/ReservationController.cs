﻿using Microsoft.AspNetCore.Mvc;

namespace Booking.Web.Controllers
{
    public class ReservationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
