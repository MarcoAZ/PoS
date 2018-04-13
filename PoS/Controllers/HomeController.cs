using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoS.Models;
using System.Web.Security;

namespace PoS.Controllers
{
    public class HomeController : Controller
    {
        private PoSContext db = new PoSContext();

        //Login Page
        // GET: /Home/
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        //Register login page
        // GET: /Home/Create/
        public ActionResult Create() {
            FormsAuthentication.SignOut();
            return View();
        }

        //POST on Login page
        [HttpPost]
        public ActionResult Index(User user) {
            //validate user exists and set cookies for the Server name
            var validate = db.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (validate != null)
            {
                FormsAuthentication.SetAuthCookie(validate.UserName.ToString(), true);
                HttpCookie UserCookie = new HttpCookie("UserCookie");
                UserCookie.Values.Add("LastName", validate.LastName);
                UserCookie.Values.Add("FirstName", validate.FirstName);
                Response.Cookies.Add(UserCookie);
                return RedirectToAction("Index", "Register");
            }
            else
                ModelState.AddModelError("", "Username or Password is incorrect");
            return View(user);
        }

        //Submitting new User for creation
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                //validate username does not already exist
                var validate = db.Users.FirstOrDefault(x => x.UserName == user.UserName);
                if (validate == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(user.UserName.ToString(), true);
                    HttpCookie UserCookie = new HttpCookie("UserCookie");
                    UserCookie.Values.Add("LastName", user.LastName);
                    UserCookie.Values.Add("FirstName", user.FirstName);
                    Response.Cookies.Add(UserCookie);
                    return RedirectToAction("Index", "Register");
                }
                else //kick back to register page
                    ModelState.AddModelError("", "Username is taken");
                return View(user);
            }

            return View(user);
        }

    }
}
