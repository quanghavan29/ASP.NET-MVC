using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class HistoryBookingController : Controller
    {
        // GET: HistoryBooking
        public ActionResult Index()
        {
            User user = (User)Session["user"];
            int user_id = user.user_id;
            BookingDao bookingDao = new BookingDao();
            List<Booking> bookings = bookingDao.findAllBookingByUserId(user_id);
            ViewBag.bookings = bookings;
            return View();
        }
    }
}