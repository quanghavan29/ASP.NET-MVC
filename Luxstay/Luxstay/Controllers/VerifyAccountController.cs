using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class VerifyAccountController : Controller
    {
        // POST: VerifyAccount
        [HttpPost]
        public ActionResult Index()
        {
            UserDao userDao = new UserDao();
            User user = new User();
            user.email = Session["emailRegister"].ToString();
            user.password = Session["passwordRegister"].ToString();
            user.name = Session["nameRegister"].ToString();
            user.phone = Session["phoneRegister"].ToString();
 
            string code = Request["code"];
            string code_verify = Session["code_verify"].ToString();
            if (code.Equals(code_verify))
            {
                // Condition to create account is valid
                userDao.insert(user); // => insert account to database
                // Return view login and set email, password in view login = email, password registed
                Session["register"] = "register";
                // Display notification "Register successfully!" for client
                Session["registerSuccess"] = "Đăng ký tài khoản thành công! Đăng nhập ngay.";
                Session.Remove("loginFail");
                return RedirectToAction("Index", "Login");
            } else
            {
                ViewBag.verify_fail = "Mã xác nhận không chính xác!";
                return View();
            }
        }

        public ActionResult ResendMail()
        {
            // Send mail again to verify
            SendMailDao sendMailDao = new SendMailDao();
            string code_verify = sendMailDao.randomCode(4);
            Session["code_verify"] = code_verify;
            string subject = "Xác thực địa chỉ email!";
            string content = "Cảm ơn bạn đã đăng ký sử dụng dịch vụ của Luxstay! Mã xác thực của bạn là: " + code_verify;
            string email = (string)Session["emailRegister"];
            sendMailDao.SendMail(email, subject, content);
            ViewBag.resend_mail = "Mã xác nhận đã được gửi lại!";
            return View();
        }
    }
}