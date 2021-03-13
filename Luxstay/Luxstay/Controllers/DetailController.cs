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
            string home_id = Request.QueryString["home_id"];

            // Display model home by home_id
            Home home = homeDao.findById(home_id);

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