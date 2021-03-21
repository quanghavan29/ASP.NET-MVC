using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Areas.Admin.Controllers
{
    public class RestoreController : Controller
    {
        // GET: Admin/Restore
        public ActionResult Index()
        {
            HomeDao homeDao = new HomeDao();
            // If user clicked on other page index

            List<Home> homes = homeDao.findAllDeleted();
            ViewBag.homes = homes;
            return View();
        }

        public ActionResult RestoreHomestay()
        {
            int home_id = Int32.Parse(Request.QueryString["home_id"]);
            HomeDao homeDao = new HomeDao();
            homeDao.restore(home_id);
            return RedirectToAction("Index", "Admin/Restore");
        }

        public ActionResult ClearHomestay()
        {
            int home_id = Int32.Parse(Request.QueryString["home_id"]);

            HomeDao homeDao = new HomeDao();
            Home home = homeDao.findById(home_id);

            PlaceDao placeDao = new PlaceDao();
            int total_homestay = placeDao.totalHomestayByPlace(home.place.place_id);

            ImagesDetailDao imagesDetailDao = new ImagesDetailDao();
            imagesDetailDao.clear(home_id);

            BookingDao bookingDao = new BookingDao();
            bookingDao.clear(home_id);

            homeDao.clear(home_id);

            placeDao.updateTotalHomestay(total_homestay - 1, home.place.place_id);

            return RedirectToAction("Index", "Admin/Restore");
        }
    }
}