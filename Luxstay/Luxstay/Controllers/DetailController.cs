using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;

namespace Luxstay.Controllers
{
    public class DetailController : Controller
    {
        // GET: Detail
        public ActionResult Index()
        {
            HomeDao homeDao = new HomeDao();
            ImagesDetailDao imagesDetailDao = new ImagesDetailDao();
            int home_id = Int32.Parse(Request.QueryString["home_id"]);

            int status = 0;
            BookingDao bookingDao = new BookingDao();
            Booking booking = bookingDao.findByHomeId(home_id);
            if (booking != null)
            {
                DateTime date_check_in = booking.date_check_in;
                DateTime date_check_out = booking.date_check_out;
                DateTime now = DateTime.Now;
                if (now <= date_check_out)
                {
                    status = 1;
                }
                else if (now > date_check_out)
                {
                    status = 0;
                }
            }

            

            // Display model home by home_id
            Home home = homeDao.findById(home_id);
            home.status = status;

            // Display all images detail of home by home_id
            List<ImagesDetail> imagesDetails = new List<ImagesDetail>();
            imagesDetails = imagesDetailDao.findAllByHomeId(home_id);
            // using dynamic to response models to view (multiple models)
            dynamic dy = new ExpandoObject();
            dy.home = home;
            dy.imagesDetails = imagesDetails;
            return View(dy);
        }
    }
}