using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Admin/Account
        public ActionResult Index()
        {
            UserDao userDao = new UserDao();
            ViewBag.users = userDao.findAll();
            return View();
        }

        public ActionResult InsertAccount()
        {
            return View();
        }

        public ActionResult UpdateAccount()
        {
            int user_id = Int32.Parse(Request.QueryString["user_id"]);
            UserDao userDao = new UserDao();
            User user = userDao.findById(user_id);
            return View(user);
        }
    }
}