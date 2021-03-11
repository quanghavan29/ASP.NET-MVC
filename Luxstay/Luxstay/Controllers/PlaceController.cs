using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class PlaceController : Controller
    {
        // GET: Place
        public ActionResult Index()
        {
            // Get value of place_id clicked
            string place_id = Request.QueryString["place_id"];
            // Get value of total_home 
            int total_home = Int32.Parse(Request.QueryString["total_home"]);
            // Get name of place
            string place_name = Request.QueryString["place_name"];

            // Display toltal_home and place_name to Place.html
            ViewData["total_home"] = total_home;
            ViewData["place_name"] = place_name;

            HomeDao homeDao = new HomeDao();
            // Display a list homes by place_id
            List<Home> homes = homeDao.findAllByPlaceId(place_id);
            return View(homes);
        }
    }
}