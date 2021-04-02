using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class VerifyAccountLinkController : Controller
    {
        // GET: VerifyAccountLink
        public ActionResult Index()
        {
            UserDao userDao = new UserDao();
            User user = new User();
            user.email = Session["emailRegister"].ToString();
            user.password = Session["passwordRegister"].ToString();
            user.name = Session["nameRegister"].ToString();
            user.phone = Session["phoneRegister"].ToString();
            // Condition to create account is valid
            userDao.insert(user); // => insert account to database
                                  // Return view login and set email, password in view login = email, password registed
            Session["register"] = "register";
            // Display notification "Register successfully!" for client
            Session["registerSuccess"] = "Đăng ký tài khoản thành công! Đăng nhập ngay.";
            Session.Remove("loginFail");
            return RedirectToAction("Index", "Login");
        }
    }
}