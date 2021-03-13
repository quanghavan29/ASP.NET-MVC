using Luxstay.Dao;
using Luxstay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Luxstay.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Session["logout"] != null || Session["loginFail"] != null)
            {
                Session.Remove("logout");
                return View();
            }

            if (Session["register"] != null)
            {
                Session.Remove("register");
                return View();
            }

            string email = "";
            string password = "";
            // get cookie of email and password from Cookies has name = "email" an "password" 
            HttpCookie c_email = HttpContext.Request.Cookies.Get("email");
            HttpCookie c_password = HttpContext.Request.Cookies.Get("password");
            // if cookies of email and password has value
            if (c_email != null && c_password != null)
            {
                // email and password get value from cookie
                email = c_email.Value;
                password = c_password.Value;
            } // else email and password still equals ""

            UserDao userDao = new UserDao();
            // Display information of User by email and password
            User user = userDao.findByEmailAndPassword(email, password);
            if (user != null) // if (email and password has value from cookie)
            {
                // lenght of username
                int lenght = user.name.Split().Length;
                // get last name of username
                string lastName = user.name.Split()[lenght - 1];
                // Save user and last name of use to session
                Session["user"] = user;
                Session["lastName"] = lastName;
                // if (user is admin => goto admin page)
                if (user.role.Equals("ROLE_ADMIN"))
                {
                    return RedirectToAction("Index", "Admin/HomeAdmin");
                } // else if (user is client => goto client page)
                else if (user.role.Equals("ROLE_USER"))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        // POST: Login/Authentic
        [HttpPost]
        public ActionResult Authentic(string remember) // string remember of checkbox
        {
            UserDao userDao = new UserDao();
            // string email and password by name from view when user input
            string email = Request["email"];
            string password = Request["password"];
            // Display information of user by email and password
            User user = userDao.findByEmailAndPassword(email, password);
            // if user != null => email and password correct
            if (user != null)
            {
                if (remember.Equals("true")) // if user click checkbox lưu tài khoản
                {
                    // Save account of user to cookies
                    HttpCookie c_email = new HttpCookie("email", email);
                    HttpCookie c_password = new HttpCookie("password", password);
                    // Set time out for cookie
                    c_email.Expires = DateTime.Now.AddDays(1);
                    c_password.Expires = DateTime.Now.AddDays(1);
                    // Add to cookie
                    Response.Cookies.Add(c_email);
                    Response.Cookies.Add(c_password);
                }
                // lenght of username
                int lenght = user.name.Split().Length;
                // last name of username
                string lastName = user.name.Split()[lenght - 1];
                // Save in4 and last name of user name to session
                Session["user"] = user;
                Session["lastName"] = lastName;
                // if user is admin
                if (user.role.Equals("ROLE_ADMIN"))
                {
                    // go to admin page
                    return RedirectToAction("Index", "Admin/HomeAdmin");
                } // else if user is not admin (client)
                // go to client page
                return RedirectToAction("Index", "Home");
            }
            else  // if email and password incorrect
            {   // Display a notification login fail to user
                Session["loginFail"] = "Invalid email or password!";
                Session.Remove("registerSuccess");
                return RedirectToAction("Index", "Login");
            }
        }

    }
}