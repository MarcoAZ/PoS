using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoS.Models;

namespace PoS.Controllers
{
    public class OrderDetailsController : Controller
    {
        private OrderDetailsContext db = new OrderDetailsContext();

        //
        // GET: /OrderDetails/
        public ViewResult Index()
        {
            return View(db.OrderDetails.ToList());
        }

        // 
        // POST: /Register/Create
        [HttpPost]
        public ActionResult Create(OrderDetails order)
        {
            order.OrderDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(order);
                db.SaveChanges();

                //go to Order history page
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index", "Register");
        }

    }

}
