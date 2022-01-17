using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IMSRepo.Models;

namespace IMS.Controllers
{
    public class MySOesController : Controller
    {
        private IMSEntities db = new IMSEntities();

        // GET: MySOes
        public ActionResult Index()
        {
            var mySOes = db.MySOes.Include(m => m.Customer);
            return View(mySOes.ToList());
        }

        // GET: MySOes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySO mySO = db.MySOes.Find(id);
            if (mySO == null)
            {
                return HttpNotFound();
            }
            return View(mySO);
        }

        // GET: MySOes/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            return View();
        }

        // POST: MySOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerId,TotalAmount,PaidAmout,Balance,SODate,DateCreated,DateModified")] MySO mySO)
        {
            mySO.SODate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.MySOes.Add(mySO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", mySO.CustomerId);
            return View(mySO);
        }

        // GET: MySOes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySO mySO = db.MySOes.Find(id);
            if (mySO == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", mySO.CustomerId);
            return View(mySO);
        }

        // POST: MySOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerId,TotalAmount,PaidAmout,Balance,SODate,DateCreated,DateModified")] MySO mySO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mySO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", mySO.CustomerId);
            return View(mySO);
        }

        // GET: MySOes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MySO mySO = db.MySOes.Find(id);
            if (mySO == null)
            {
                return HttpNotFound();
            }
            return View(mySO);
        }

        // POST: MySOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MySO mySO = db.MySOes.Find(id);
            db.MySOes.Remove(mySO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
