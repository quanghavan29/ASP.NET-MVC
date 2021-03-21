using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Areas.Admin.Controllers
{
    public class ManagerHomestayController : Controller
    {
        // GET: Admin/ManagerHomestay
        public ActionResult Index()
        {
            return View();
        }

        // POST: Admin/ManagerHomstay/InsertHomestay

        [HttpPost]
        public ActionResult InsertHomestay()
        {
            string image_intro = Request["image_intro"];
            string home_name = Request["home_name"];
            string short_description = Request["short_description"];
            int price = 0;
            int room_number = 0;
            try
            {
                price = Int32.Parse(Request["price"]);
            }
            catch (Exception)
            {
                Session["Error"] = "Giá Phòng Phải Là Số!";
                return RedirectToAction("Index", "Error");
            }
            try
            {
                room_number = Int32.Parse(Request["room_number"]);
            }
            catch (Exception)
            {
                Session["Error"] = "Số Phòng Ngủ Phải Là Số!";
                return RedirectToAction("Index", "Error");
            }
            string detail_description = Request["detail_description"];
            detail_description = detail_description.Replace("break", "<br /><br />");
            string address = Request["address"];
            string place_id = Request["place"];
            string home_tpye = Request["home_type"];
            if (home_tpye.Equals("canho"))
            {
                home_tpye = "Căn hộ dịch vụ";
            }
            else if (home_tpye.Equals("chungcu"))
            {
                home_tpye = "Chu cư";
            }
            else if (home_tpye.Equals("homestay"))
            {
                home_tpye = "Homestay";
            }
            else if (home_tpye.Equals("studio"))
            {
                home_tpye = "Studio";
            }
            else if (home_tpye.Equals("bietthu"))
            {
                home_tpye = "Biệt thự";
            }

            HomeDao homeDao = new HomeDao();
            Home home = new Home();
            home.home_name = home_name;
            home.home_type = home_tpye;
            home.image_intro = image_intro;
            Place place = new Place();
            place.place_id = place_id;
            home.place = place;
            home.price = price;
            home.room_number = room_number;
            home.short_description = short_description;
            home.detail_description = detail_description;
            home.address = address;



            homeDao.insert(home);

            string image_1 = Request["image_1"];
            string image_2 = Request["image_2"];
            string image_3 = Request["image_3"];

            int home_id_insert = homeDao.findHomeIdInsert();
            ImagesDetailDao imagesDetailDao = new ImagesDetailDao();
            imagesDetailDao.insert(home_id_insert, image_1);
            imagesDetailDao.insert(home_id_insert, image_2);
            imagesDetailDao.insert(home_id_insert, image_3);

            PlaceDao placeDao = new PlaceDao();
            int total_homestay = placeDao.totalHomestayByPlace(place_id);
            placeDao.updateTotalHomestay(total_homestay + 1, place_id);

            return RedirectToAction("Index", "Admin/Homestay");
        }

        // POST: Admin/ManagerHomestay/UpdateHomestay
        public ActionResult UpdateHomestay()
        {
            return View();
        }

        // GET: Admin/ManagerHomestay/DeleteHomestay

        public ActionResult DeleteHomestay()
        {
            int home_id = Int32.Parse(Request["home_id"]);
            HomeDao homeDao = new HomeDao();
            homeDao.delete(home_id);
            return RedirectToAction("Index", "Admin/Homestay");
        }

    }
}