using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Luxstay.Dao;
using Luxstay.Models;
using System.Dynamic;

namespace Luxstay.Controllers
{

    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            SendMailDao sendMailDao = new SendMailDao();
            HomeDao homeDao = new HomeDao();
            PlaceDao placeDao = new PlaceDao();

            int pageIndex = 1;
            // If user clicked on other page index
            if (Request.QueryString["pageIndex"] != null)
            {
                // Then pageIndex = value of that page index user clicked
                pageIndex = Int32.Parse(Request.QueryString["pageIndex"]);
            }
            int pageSize = 8;

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

            // Display list of homes and list of places to Home page
            dynamic dy = new ExpandoObject(); // dynamic - multiple model
            dy.homes = homeDao.findAll(pageIndex, pageSize);
            dy.places = placeDao.findAll();

            return View(dy);
        }
    }
}