using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify()
        {
            UserDao userDao = new UserDao();
            // Declare a user and set in4 for user = Request by name
            User user = new User();
            user.email = Request["email"];
            user.phone = Request["phone"];
            user.name = Request["name"]; ;
            user.password = Request["password"];
            string repassword = Request["repassword"];
            // Save infor user register to session
            Session["emailRegister"] = user.email;
            Session["passwordRegister"] = user.password;
            Session["nameRegister"] = user.name;
            Session["phoneRegister"] = user.phone;
            
            // if email already exist in database
            if (userDao.findByEmail(user.email) != null)
            {
                // Display notification email exist to client
                Session["emailExist"] = "Địa chỉ email đã tồn tại!";
                Session.Remove("passwordNotMatch");
                return RedirectToAction("Index", "Register");
            }
            else // else if email does not exist
            {
                // Then check password do macth
                if (user.password.Equals(repassword)) // if password == repasssword
                {
                    // Condition to create account is valid
                    userDao.insert(user); // => insert account to database
                    // Return view login and set email, password in view login = email, password registed
                    Session["register"] = "register";
                    // Display notification "Register successfully!" for client
                    Session["registerSuccess"] = "Đăng ký tài khoản thành công! Đăng nhập ngay.";
                    Session.Remove("loginFail");
                    return RedirectToAction("Index", "Login");
                }
                else // else if password do not match (password != repassword)
                {
                    // Display notification "password do not match" for client
                    Session["passwordNotMatch"] = "Mật khẩu không trùng khớp!";
                    Session.Remove("emailExist");
                    return RedirectToAction("Index", "Register");
                }
            }
        }
    }
}