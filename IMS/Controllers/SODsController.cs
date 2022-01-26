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
  public class SODsController : Controller
  {
    private IMSEntities db = new IMSEntities();

    // GET: SODs
    public ActionResult Index()
    {
      var SODs = db.SODs.Include(s => s.Product);
      return View(SODs.ToList());
    }

    // GET: SODs/Details/5
    public ActionResult Details(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      SOD sO = db.SODs.Find(id);
      if (sO == null)
      {
        return HttpNotFound();
      }
      return View(sO);
    }

    // GET: SODs/Create

    public ActionResult Create()
    {
      ViewBag.MyMessage = "Good Morning";
      TempData["myData"] = "Hello world";
      ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
      ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
      return View();
    }

    // POST: SODs/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    //public ActionResult Create([Bind(Include = "Id,CustomerId,ProductId,ItemTotal,SODate")] SO sO)
    public ActionResult Create(SOD sod)
    {

      //DateTime PKDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time"));
      DateTime CorectedDate = TimeZoneInfo.ConvertTimeFromUtc(sod.SODate.Value, TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time"));

      sod.SODate = CorectedDate;
      var retrieveMSG = TempData["myData"];
      sod.Id = Guid.NewGuid().ToString();
      if (ModelState.IsValid)
      {
        db.SODs.Add(sod);
        db.SaveChanges();
        return RedirectToAction("Index");
      }

      //ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", sod.CustomerId);
      ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sod.ProductId);
      return View(sod);
    }

    // GET: SODs/Edit/5
    public ActionResult Edit(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }

      SOD sod = db.SODs.Find(id);

      //SOD so = db.SODs.Where(x => x.Id == id).FirstOrDefault();


      if (sod == null)
      {
        return HttpNotFound();
      }
      //ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", sO.CustomerId);
      ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sod.ProductId);
      return View(sod);
    }

    // POST: SODs/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,CustomerId,ProductId,ItemTotal,SODate")] SOD sod)
    {
      
        db.Entry(sod).State = EntityState.Modified;
        db.SaveChanges();
        return RedirectToAction("Index");
      
      //ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", sod.CustomerId);
      ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", sod.ProductId);
      return View(sod);
    }

    // GET: SODs/Delete/5
    public ActionResult Delete(string id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      SOD sod = db.SODs.Find(id);
      if (sod == null)
      {
        return HttpNotFound();
      }
      return View(sod);
    }

    // POST: SODs/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(string id)
    {
      SOD sod = db.SODs.Find(id);
      db.SODs.Remove(sod);
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
