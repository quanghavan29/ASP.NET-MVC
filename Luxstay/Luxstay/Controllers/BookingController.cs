using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class BookingController : Controller
    {

        public ActionResult HistoryBooking()
        {
            User user = (User)Session["user"];
            int user_id = user.user_id;
            BookingDao bookingDao = new BookingDao();
            List<Booking> bookings = bookingDao.findAllBookingByUserId(user_id);
            ViewBag.bookings = bookings;
            return View();
        }

        // POST: Booking
        [HttpPost]
        public ActionResult Index()
        {
            HomeDao homeDao = new HomeDao();
            int home_id = Int32.Parse(Request["home_id"]);
            Home home = homeDao.findById(home_id);
            User user = (User)Session["user"];
            BookingDao bookingDao = new BookingDao();
            try
            {
                string str_date_check_in = Request["dateCheckIn"];
                string str_date_check_out = Request["dateCheckOut"];
                DateTime date_check_in = DateTime.Parse(str_date_check_in);
                DateTime date_check_out = DateTime.Parse(str_date_check_out);
                DateTime now = DateTime.Now;
                if (date_check_in < now)
                {
                    Session["Error"] = "Ngày Đặt Phòng Phải Lớn Hơn Ngày Hiện Tại!";
                    return RedirectToAction("Index", "Error");
                }
                else
                {
                    TimeSpan TimeCheckInt = date_check_in - now;
                    if (TimeCheckInt.Days > 7)
                    {
                        Session["Error"] = "Thời Gian Đặt Phòng Trước Không Quá 7 Ngày Tính Từ Ngày Hôm Nay!";
                        return RedirectToAction("Index", "Error");
                    }
                    else
                    {
                        if (date_check_out <= date_check_in)
                        {
                            Session["Error"] = "Thời Gian Đặt Phòng Phải Ít Nhất Là 1 Ngày!";
                            return RedirectToAction("Index", "Error");
                        }
                        else
                        {
                            TimeSpan TimeCheckOut = date_check_out - date_check_in;
                            int total_day_number = TimeCheckOut.Days;
                            int total_price = total_day_number * home.price;
                            bookingDao.insert(user.user_id, home.home_id, date_check_in, date_check_out, total_price);
                            return RedirectToAction("HistoryBooking", "Booking/HistoryBooking");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session["Error"] = "Hãy Đảm Bảo Rằng Bạn Đã Nhập Đúng Ngày/Tháng/Năm khi đặt phòng!";
                return RedirectToAction("Index", "Error");
            }
        }
    }
}