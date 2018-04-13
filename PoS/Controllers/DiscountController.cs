using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoS.Models;

namespace PoS.Controllers
{ 
    public class DiscountController : Controller
    {
        private PoSContext db = new PoSContext();

        //get list of all discounts
        public List<Discount> GetDiscounts()
        {
            return db.Discounts.ToList();
        }

        // Get details about the discount from the server rather than client-side
        // GET: /Discount/Details/5
        // returns Json rather than a page
        public ActionResult Details(int id)
        {
            Discount discount = db.Discounts.Find(id);
            return Json(new { name = discount.Name, 
                            type = discount.Type, 
                            fixedAmount = discount.FixedAmount, 
                            percentAmount = discount.PercentAmount,
                            id = discount.Id }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Discount/Create
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Discount/Create
        [HttpPost]
        public ActionResult Create(Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Discounts.Add(discount);
                db.SaveChanges();
                return RedirectToAction("Index", "Register");  
            }

            return View(discount);
        }
        
        //
        // GET: /Discount/Edit/5
        public ActionResult Edit(int id)
        {
            Discount discount = db.Discounts.Find(id);
            return View(discount);
        }

        //
        // POST: /Discount/Edit/5
        [HttpPost]
        public ActionResult Edit(Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Register");
            }
            return View(discount);
        }

        //
        // GET: /Discount/Delete/5
        public ActionResult Delete(int id)
        {
            Discount discount = db.Discounts.Find(id);
            return View(discount);
        }

        //
        // POST: /Discount/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Discount discount = db.Discounts.Find(id);
            db.Discounts.Remove(discount);
            db.SaveChanges();
            return RedirectToAction("Index", "Register");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //check if discount name already exists
        public JsonResult IsNameExist(string Name, int ? Id)
        {
            //have to do a different check depending on whether we are editing or creating
            //if id null, we are creating
            //else we are editing
            var validateName = Id == null ?
                              db.Discounts.FirstOrDefault(x => x.Name == Name) : 
                              db.Discounts.FirstOrDefault(x => x.Name == Name && x.Id != Id); 
            if (validateName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }
    }
}