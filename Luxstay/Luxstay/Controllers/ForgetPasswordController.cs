using Luxstay.Dao;
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

        [HttpPost]
        public ActionResult AcceptEmail()
        {
            string email = Request["email"];
            SendMailDao sendMailDao = new SendMailDao();
            string password = sendMailDao.randomCode(4);
            string subject = "Quên Mật Khẩu!";
            string content = "Mật khẩu mới của bạn để đăng nhập trên Luxstay là: " + password;
            sendMailDao.SendMail(email, subject, content);
            UserDao userDao = new UserDao();
            userDao.updatePasswordByEmail(email, password);
            ViewData["email"] = email;
            return View();
        }
    }
}