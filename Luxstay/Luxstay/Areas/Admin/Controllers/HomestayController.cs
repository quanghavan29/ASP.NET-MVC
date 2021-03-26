using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Areas.Admin.Controllers
{
    public class HomestayController : Controller
    {
        // GET: Admin/Homestay
        public ActionResult Index()
        {
            HomeDao homeDao = new HomeDao();
            int pageIndex = 1;
            // If user clicked on other page index
            if (Request.QueryString["pageIndex"] != null)
            {
                // Then pageIndex = value of that page index user clicked
                pageIndex = Int32.Parse(Request.QueryString["pageIndex"]);
            }
            int pageSize = 10;

            int totalPage = 0;

            // Count all homes in database (Table Home)
            int count = homeDao.count();
            // IF count % pageSize == 0 => totalPage = count / pageSize
            if (count % pageSize == 0)
            {
                totalPage = count / pageSize;
            }
            else // totalPage add more 1 page, contains the of the residual homes. (residual = sót lại)
            {
                totalPage = count / pageSize + 1;
            }

            // Display total of page to Home page for pagging
            ViewData["totalPage"] = totalPage;
            // Display pageIndex to active page current
            ViewData["pageIndex"] = pageIndex;
            List<Home> homes = homeDao.findAll(pageIndex, pageSize);
            // Display list all homestay in database to index page of (Manager Homestay) and pagging
            ViewBag.homes = homes;
            return View();
        }

        public ActionResult InsertHome()
        {
            return View();
        }

        public ActionResult UpdateHome()
        {
            int home_id = Int32.Parse(Request.QueryString["home_id"]);
            HomeDao homeDao = new HomeDao();
            Home home = homeDao.findById(home_id);
            home.detail_description = home.detail_description.Replace("<br/><br/>", "break");
            home.detail_description = home.detail_description.Replace("<br /><br />", "break");
            home.detail_description = home.detail_description.Replace("<br/>", "break");
            home.detail_description = home.detail_description.Replace("<br />", "break");
            ImagesDetailDao imagesDetailDao = new ImagesDetailDao();

            // Display all images detail of home by home_id
            List<ImagesDetail> imagesDetails = new List<ImagesDetail>();
            imagesDetails = imagesDetailDao.findAllByHomeId(home_id);
            // using dynamic to response models to view (multiple models)
            dynamic dy = new ExpandoObject();
            dy.home = home;
            dy.imagesDetails = imagesDetails;

            // Check value of home type
            if (home.home_type.Equals("Căn hộ dịch vụ"))
            {
                ViewBag.home_type = "canho";
            }
            else if (home.home_type.Equals("Chung cư"))
            {
                ViewBag.home_type = "chungcu";
            }
            else if (home.home_type.Equals("Homestay"))
            {
                ViewBag.home_type = "homestay";
            }
            else if (home.home_type.Equals("Studio"))
            {
                ViewBag.home_type = "studio";
            }
            else if (home.home_type.Equals("Biệt thự"))
            {
                ViewBag.home_type = "bietthu";
            }

            return View(dy);
        }
    }
}