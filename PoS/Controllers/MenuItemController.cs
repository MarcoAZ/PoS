using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PoS.Models;
using System.Web.Helpers;

namespace PoS.Controllers
{ 
    public class MenuItemController : Controller
    {
        private PoSContext db = new PoSContext();

        public List<MenuItem> GetItems() 
        {
            return db.MenuItems.ToList();
        }

        // get the cost of the item as json
        // GET: /MenuItem/Details/5
        public ActionResult Details(int id)
        {
            MenuItem menuitem = db.MenuItems.Find(id);
            return Json(new { name = menuitem.Name, cost = menuitem.Cost, id = menuitem.Id }, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /MenuItem/Create
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /MenuItem/Create
        [HttpPost]
        public ActionResult Create(MenuItem menuitem)
        {
            if (ModelState.IsValid)
            {
                db.MenuItems.Add(menuitem);
                db.SaveChanges();
                return RedirectToAction("Index", "Register");  
            }

            return View(menuitem);
        }
        
        //
        // GET: /MenuItem/Edit/5
        public ActionResult Edit(int id)
        {
            MenuItem menuitem = db.MenuItems.Find(id);
            return View(menuitem);
        }

        //
        // POST: /MenuItem/Edit/5
        [HttpPost]
        public ActionResult Edit(MenuItem menuitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menuitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Register");
            }
            return View(menuitem);
        }

        //
        // GET: /MenuItem/Delete/5
        public ActionResult Delete(int id)
        {
            MenuItem menuitem = db.MenuItems.Find(id);
            return View(menuitem);
        }

        //
        // POST: /MenuItem/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            MenuItem menuitem = db.MenuItems.Find(id);
            db.MenuItems.Remove(menuitem);
            db.SaveChanges();
            return RedirectToAction("Index", "Register");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public JsonResult IsItemNameExist(string Name, int ? Id) {
            var validateName = Id == null ? 
                              db.MenuItems.FirstOrDefault(x => x.Name == Name) :
                              db.MenuItems.FirstOrDefault(x => x.Name == Name && x.Id != Id);

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