using Luxstay.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class CancelBookingController : Controller
    {
        // GET: CancelBooking
        public ActionResult Index()
        {
            int booking_id = Int32.Parse(Request.QueryString["booking_id"]);
            BookingDao bookingDao = new BookingDao();
            bookingDao.deleteById(booking_id);
            return RedirectToAction("Index", "HistoryBooking");
        }
    }
}