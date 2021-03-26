using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Areas.Admin.Controllers
{
    public class ManagerBookingController : Controller
    {
        // GET: Admin/ManagerBooking
        public ActionResult Index()
        {
            BookingDao bookingDao = new BookingDao();
            List<Booking> bookings = bookingDao.findAllBooking();
            ViewBag.bookings = bookings;
            return View();
        }

        public ActionResult Cancel()
        {
            int booking_id = Int32.Parse(Request.QueryString["booking_id"]);
            BookingDao bookingDao = new BookingDao();
            bookingDao.deleteById(booking_id);
            return RedirectToAction("Index", "Admin/ManagerBooking");
        }
    }
}