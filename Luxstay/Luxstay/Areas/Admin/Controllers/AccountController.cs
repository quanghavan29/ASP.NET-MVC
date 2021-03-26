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
            // Display list all user in database
            ViewBag.users = userDao.findAll();
            return View();
        }

        public ActionResult InsertAccount()
        {
            // goto insert account page
            return View();
        }

        public ActionResult UpdateAccount()
        {
            // get value of user_id when admin clicked update that user
            int user_id = Int32.Parse(Request.QueryString["user_id"]);
            UserDao userDao = new UserDao();
            // Display update account page in4 of user by id
            User user = userDao.findById(user_id);
            return View(user);
        }
    }
}