using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Areas.Admin.Controllers
{
    public class ManagerAccountController : Controller
    {
        // GET: Admin/ManagerAccount
        public ActionResult Index()
        {
            return View();
        }

        // POST: Admin/ManagerAccount/InsertAccount

        [HttpPost]
        public ActionResult InsertAccount()
        {
            UserDao userDao = new UserDao();
            string email = Request["email"];
            string phone = Request["phone"];
            string name = Request["name"];
            string address = Request["address"];
            string role = Request["role"];
            string password = Request["password"];
            User user = new User();
            user.email = email;
            user.phone = phone;
            user.name = name;
            user.address = address;
            user.role = role;
            user.password = password;
            userDao.insert(user);
            return RedirectToAction("Index", "Admin/Account");
        }

        [HttpPost]
        public ActionResult UpdateAccount()
        {
            UserDao userDao = new UserDao();
            int user_id = Int32.Parse(Request["user_id"]);
            string email = Request["email"];
            string phone = Request["phone"];
            string name = Request["name"];
            string address = Request["address"];
            string role = Request["role"];
            string password = Request["password"];
            User user = new User();
            user.user_id = user_id;
            user.email = email;
            user.phone = phone;
            user.name = name;
            user.address = address;
            user.role = role;
            user.password = password;
            userDao.update(user);
            return RedirectToAction("Index", "Admin/Account");
        }

        public ActionResult DeleteAccount()
        {
            UserDao userDao = new UserDao();
            int user_id = Int32.Parse(Request["user_id"]);
            userDao.delete(user_id);
            return RedirectToAction("Index", "Admin/Account");
        }
    }
}