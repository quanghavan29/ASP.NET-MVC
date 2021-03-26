using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Index()
        {
            User user = (User)Session["user"];
            return View(user);
        }

        public ActionResult Update()
        {
            UserDao userDao = new UserDao();
            // Get value of that user update
            int user_id = Int32.Parse(Request["user_id"]);
            // Get values of user (input by admin edit) 
            string email = Request["email"];
            string phone = Request["phone"];
            string name = Request["name"];
            string address = Request["address"];
            string role = "ROLE_USER";
            string password = Request["password"];
            // Set values for user
            User user = new User();
            user.user_id = user_id;
            user.email = email;
            user.phone = phone;
            user.name = name;
            user.address = address;
            user.role = role;
            user.password = password;
            // Update user in database
            userDao.update(user);
            // Go to Manager Account page
            ViewData["update_profile_success"] = "Cập nhật thông tin tài khoản thành công!";
            Session["user"] = user;
            return View(user);
        }
    }
}