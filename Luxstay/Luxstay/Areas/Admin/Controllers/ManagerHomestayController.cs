using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        // Insert homestay to database
        [HttpPost]
        public ActionResult InsertHomestay()
        {
            string image_intro = "";
            // Get value from input when admin input and submit
            HttpPostedFileBase file = Request.Files["image_intro"];
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Assets/images/product"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    image_intro = Path.GetFileName(file.FileName);
                }
                catch
                {
                    Console.WriteLine("loi upload image!");
                }
            string home_name = Request["home_name"];
            string short_description = Request["short_description"];
            int price = 0;
            int room_number = 0;
            try
            {
                // Price is integer
                price = Int32.Parse(Request["price"]);
            }
            catch (Exception) // If price is not int => Error
            {
                Session["Error"] = "Giá Phòng Phải Là Số!";
                return RedirectToAction("Index", "Error"); // Display error to error page
            }
            try
            {   // room_number is int
                room_number = Int32.Parse(Request["room_number"]);
            }
            catch (Exception) // If room_number is not int => Error
            {
                Session["Error"] = "Số Phòng Ngủ Phải Là Số!";
                return RedirectToAction("Index", "Error"); // Display error to error page
            }
            // input do not match with <br/> so I replace by 'break'
            string detail_description = Request["detail_description"];
            // and then I replace 'break' by <br/>
            detail_description = detail_description.Replace("break", "<br /><br />");
            string address = Request["address"];
            string place_id = Request["place"];
            string home_tpye = Request["home_type"];
            // Check value of home type
            if (home_tpye.Equals("canho"))
            {
                home_tpye = "Căn hộ dịch vụ";
            }
            else if (home_tpye.Equals("chungcu"))
            {
                home_tpye = "Chung cư";
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
            // Set values for Home
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

            // Insert Home to database
            homeDao.insert(home);

            // images detail of Homestay (3 image)
            string image_1 = Request["image_1"];
            string image_2 = Request["image_2"];
            string image_3 = Request["image_3"];

            // get home_id of home (vừa insert ở bên trên)
            int home_id_insert = homeDao.findHomeIdInsert();
            // insert 3 image and home_id of that 3 images to database
            ImagesDetailDao imagesDetailDao = new ImagesDetailDao();
            imagesDetailDao.insert(home_id_insert, image_1);
            imagesDetailDao.insert(home_id_insert, image_2);
            imagesDetailDao.insert(home_id_insert, image_3);

            // total homestay by that place inserted to up more 1
            PlaceDao placeDao = new PlaceDao();
            // get old total homestay by place
            int total_homestay = placeDao.totalHomestayByPlace(place_id);
            // then update that total to up + 1
            placeDao.updateTotalHomestay(total_homestay + 1, place_id);

            // Goto Manager Homestay page
            return RedirectToAction("Index", "Admin/Homestay");
        }

        // POST: Admin/ManagerHomestay/UpdateHomestay
        [HttpPost]
        public ActionResult UpdateHomestay()
        {
            int home_id = Int32.Parse(Request["home_id"]);
            // Get value from input when admin input and submit
            string image_intro = Request["image_intro"];
            string home_name = Request["home_name"];
            string short_description = Request["short_description"];
            int price = 0;
            int room_number = 0;
            try
            {
                // Price is integer
                price = Int32.Parse(Request["price"]);
            }
            catch (Exception) // If price is not int => Error
            {
                Session["Error"] = "Giá Phòng Phải Là Số!";
                return RedirectToAction("Index", "Error"); // Display error to error page
            }
            try
            {   // room_number is int
                room_number = Int32.Parse(Request["room_number"]);
            }
            catch (Exception) // If room_number is not int => Error
            {
                Session["Error"] = "Số Phòng Ngủ Phải Là Số!";
                return RedirectToAction("Index", "Error"); // Display error to error page
            }
            // input do not match with <br/> so I replace by 'break'
            string detail_description = Request["detail_description"];
            // and then I replace 'break' by <br/>
            detail_description = detail_description.Replace("break", "<br/><br/>");
            string address = Request["address"];
            string place_id = Request["place"];
            string home_tpye = Request["home_type"];
            // Check value of home type
            if (home_tpye.Equals("canho"))
            {
                home_tpye = "Căn hộ dịch vụ";
            }
            else if (home_tpye.Equals("chungcu"))
            {
                home_tpye = "Chung cư";
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
            // Set values for Home
            HomeDao homeDao = new HomeDao();
            Home home = new Home();
            home.home_id = home_id;
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

            // Insert Home to database
            homeDao.update(home);
            ImagesDetailDao imagesDetailDao = new ImagesDetailDao();
            imagesDetailDao.clear(home.home_id);
            // images detail of Homestay (3 image)
            string image_1 = Request["image_1"];
            string image_2 = Request["image_2"];
            string image_3 = Request["image_3"];

            // insert 3 image and home_id of that 3 images to database
            imagesDetailDao.insert(home.home_id, image_1);
            imagesDetailDao.insert(home.home_id, image_2);
            imagesDetailDao.insert(home.home_id, image_3);

            return RedirectToAction("Index", "Admin/Homestay");

        }

        // GET: Admin/ManagerHomestay/DeleteHomestay
        // Delete Homestay by id (update status to restore = false)
        // That can restore homestay deleted
        public ActionResult DeleteHomestay()
        {
            // Get value of home_id when admin clicked delete
            int home_id = Int32.Parse(Request["home_id"]);
            HomeDao homeDao = new HomeDao();
            // Delete that homestay by id (update restore to false)
            homeDao.delete(home_id);
            return RedirectToAction("Index", "Admin/Homestay");
        }

    }
}