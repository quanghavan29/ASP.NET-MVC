using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            Session.RemoveAll();
            Session["logout"] = "logout";
            return RedirectToAction("Index", "Login");
        }
    }
}