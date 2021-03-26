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
        // Insert account to database
        [HttpPost]
        public ActionResult InsertAccount()
        {
            UserDao userDao = new UserDao();
            // Get value of all input by name
            string email = Request["email"];
            string phone = Request["phone"];
            string name = Request["name"];
            string address = Request["address"];
            string role = Request["role"];
            string password = Request["password"];
            // And set to ob user
            User user = new User();
            user.email = email;
            user.phone = phone;
            user.name = name;
            user.address = address;
            user.role = role;
            user.password = password;
            // insert user to database
            userDao.insert(user);
            // Goto Index page of Manager Account
            return RedirectToAction("Index", "Admin/Account");
        }

        // POST: Admin/ManagerAccount/UpdateAccount
        // Update user by id

        [HttpPost]
        public ActionResult UpdateAccount()
        {
            UserDao userDao = new UserDao();
            // Get value of that user update
            int user_id = Int32.Parse(Request["user_id"]);
            // Get values of user (input by admin edit) 
            string email = Request["email"];
            string phone = Request["phone"];
            string name = Request["name"];
            string address = Request["address"];
            string role = Request["role"];
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
            return RedirectToAction("Index", "Admin/Account");
        }

        // Get: Admin/ManagerAccount/DeleteAccount
        // Delete user from database by id
        public ActionResult DeleteAccount()
        {
            UserDao userDao = new UserDao();
            // Get value of user_id when admin clicked
            int user_id = Int32.Parse(Request["user_id"]);
            // Delete user by id
            userDao.delete(user_id);
            // Go to Manager Account page
            return RedirectToAction("Index", "Admin/Account");
        }
    }
}