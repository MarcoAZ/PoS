using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoS.Models;

namespace PoS.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        public ActionResult Index()
        {
            //Get all items
            ViewBag.MenuItems = new MenuItemController().GetItems();

            //Get Taxes
            ViewBag.Taxes = new TaxController().GetTaxes();
            ViewBag.TotalTaxPerc = new TaxController().GetTaxTotal();

            //get discounts
            ViewBag.Discounts = new DiscountController().GetDiscounts();

            //set cookies for current server
            HttpCookie UserCookies = Request.Cookies["UserCookie"];
            if (UserCookies != null) //incase we got here without logging in...
            {
                ViewBag.FirstName = UserCookies.Values["FirstName"];
                ViewBag.LastName = UserCookies.Values["LastName"];
            }

            //send to view
            return View();
        }

    }
}
