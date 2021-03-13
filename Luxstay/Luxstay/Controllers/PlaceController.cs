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
            HomeDao homeDao = new HomeDao();
            // Get value of place_id clicked
            string place_id = Request.QueryString["place_id"];
            // Get value of total_home 
            int total_home = Int32.Parse(Request.QueryString["total_home"]);
            // Get name of place to display view
            string place_name = Request.QueryString["place_name"];

            int pageIndex = 1;
            // If user clicked on other page index
            if (Request.QueryString["pageIndex"] != null)
            {
                // Then pageIndex = value of that page index user clicked
                pageIndex = Int32.Parse(Request.QueryString["pageIndex"]);
            }
            int pageSize = 8;

            int totalPage = 0;

            // Count all homes in database (Table Home) by place
            int count = homeDao.countByPlace(place_id);
            // IF count % pageSize == 0 => totalPage = count / pageSize
            if (count % pageSize == 0)
            {
                totalPage = count / pageSize;
            }
            else // totalPage add more 1 page, contains the of the residual homes. (residual = sót lại)
            {
                totalPage = count / pageSize + 1;
            }

            // Display toltal_home and place_name to Place.html
            ViewData["place_id"] = place_id;
            ViewData["total_home"] = total_home;
            ViewData["place_name"] = place_name;

            // Display total of page to Home page for pagging
            ViewData["totalPage"] = totalPage;
            // Display pageIndex to active page current
            ViewData["pageIndex"] = pageIndex;

            // Display a list homes by place_id
            List<Home> homes = homeDao.findAllByPlaceId(place_id, pageIndex, pageSize);
            return View(homes);
        }
    }
}