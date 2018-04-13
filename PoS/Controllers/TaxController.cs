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
    public class TaxController : Controller
    {
        private PoSContext db = new PoSContext();

        //get list of all taxes with names
        public List<Tax> GetTaxes()
        {
            return db.Taxes.ToList();
        }

        //get the total tax percent
        //Taxes are added to one another and not chosen (all are applied)
        public decimal GetTaxTotal()
        {
            decimal total = 0;
            foreach (var tax in db.Taxes.ToList()) {
                total += tax.Rate;
            }
            return total;
        }

        //
        // GET: /Tax/Create
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Tax/Create

        [HttpPost]
        public ActionResult Create(Tax tax)
        {
            if (ModelState.IsValid)
            {
                db.Taxes.Add(tax);
                db.SaveChanges();
                return RedirectToAction("Index", "Register");  
            }

            return View(tax);
        }
        
        //
        // GET: /Tax/Edit/5
        public ActionResult Edit(int id)
        {
            Tax tax = db.Taxes.Find(id);
            return View(tax);
        }

        //
        // POST: /Tax/Edit/5
        [HttpPost]
        public ActionResult Edit(Tax tax)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tax).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Register");
            }
            return View(tax);
        }

        //
        // GET: /Tax/Delete/5
        public ActionResult Delete(int id)
        {
            Tax tax = db.Taxes.Find(id);
            return View(tax);
        }

        //
        // POST: /Tax/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Tax tax = db.Taxes.Find(id);
            db.Taxes.Remove(tax);
            db.SaveChanges();
            return RedirectToAction("Index", "Register");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult IsNameExist(string Name, int ? Id)
        {
            var validateName = Id == null ?
                              db.Taxes.FirstOrDefault(x => x.Name == Name) :
                              db.Taxes.FirstOrDefault(x => x.Name == Name && x.Id != Id);
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