using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Areas.Admin.Controllers
{
    public class ManagerHomeController : Controller
    {
        // GET: Admin/ManagerHome
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/ManagerHome
        
        public ActionResult InsertHome()
        {
            return View();
        }
    }
}