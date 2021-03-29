using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class ForgetPasswordController : Controller
    {
        // GET: ForgetPassword
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AcceptEmail()
        {
            return View();
        }
    }
}