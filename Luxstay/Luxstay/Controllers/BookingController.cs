using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class BookingController : Controller
    {
        // POST: Booking
        [HttpPost]
        public ActionResult Index()
        {
            return View();
        }
    }
}