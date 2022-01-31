using IMSRepo.Models;
using IMSRepo.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
  public class SOVMController : Controller
  {
    private IMSEntities db = new IMSEntities();

    // GET: MySOes
    public ActionResult Index()
    {
      List<MySO> mySOes = db.MySOes.ToList();
      return View(mySOes.ToList());
    }
    public ActionResult Create()
    {

      decimal maxId = 0;
      if (db.MySOes.Count() != 0) maxId = db.MySOes.Max(x => x == null ? 0 : x.InvoiceNo);
      maxId = maxId + 1;
      //SOVM sovm = new SOVM();
      ViewBag.InvoiceNo1 = maxId;
      ViewBag.Customers = new SelectList(db.Customers, "Id", "Name");
      //ViewBag.Products = new SelectList(db.Products, "Id", "Name");
      ViewBag.Products = db.Products;

      return View();
    }
    [HttpPost]
    public ActionResult Create(SOVM saleOrderVM)
    {

      MySO mySO = saleOrderVM.MySO;
      mySO.Id = Guid.NewGuid().ToString();
      mySO.DateCreated = DateTime.Now;

      db.MySOes.Add(mySO);
      List<SOD> LstSODs = saleOrderVM.SODs;
      foreach (SOD item in LstSODs)
      {
        Product rowProd = db.Products.FirstOrDefault(x => x.Name == item.Product.Name);
        item.Id = Guid.NewGuid().ToString();
        item.ProductId = rowProd.Id;
        item.MySoId = mySO.Id;
        item.Product = null;
        db.SODs.Add(item);
      }

      db.SaveChanges();
      return RedirectToAction("Index");
    }
    public MySO getSODataFromDB(string id)
    {
      MySO so1 = new MySO();
      foreach (MySO so in db.MySOes)
      {
        SOVM sovm = new SOVM();
        if (so.Id == id)
        {
          //sovm.MySO = so;
          so1 = so;
          break;
        }
      }
      return so1;
    }
    public ActionResult Edit(string id)
    {

      decimal maxId = 0;
      //if (db.MySOes.Count() != 0) maxId = db.MySOes.Max(x => x == null ? 0 : x.InvoiceNo);
      //maxId = maxId + 1;
      SOVM sovm = new SOVM();//create object

      //sovm.MySO = getSODataFromDB(id);
      sovm.MySO = db.MySOes.FirstOrDefault(x => x.Id == id);//get the sale order based on saleorder id




      sovm.SODs = db.SODs.Where(x => x.MySoId == id).ToList();// get the sale order details based on saleorder id
      sovm.Products = db.Products.ToList();//get all products

      ViewBag.InvoiceNo1 = sovm.MySO.InvoiceNo;//get invoice number and set it in viewbag
      ViewBag.Customers = new SelectList(db.Customers, "Id", "Name");
      //ViewBag.Products = new SelectList(db.Products, "Id", "Name");
      ViewBag.Products = db.Products;

      return View(sovm);
    }
    [HttpPost]
    public ActionResult Edit(SOVM sovm)
    {

      MySO mySO = sovm.MySO;
      mySO.DateModified = DateTime.Now;
      db.Entry(mySO).State = EntityState.Modified;

      List<SOD> LstOldSods = db.SODs.Where(x=>x.MySoId== mySO.Id).ToList();
      foreach(SOD oldSOD in LstOldSods)
      {
        db.SODs.Remove(oldSOD);
      }

      List<SOD> LstSODs = sovm.SODs;
      foreach (SOD item in LstSODs)
      {
        Product rowProd = db.Products.FirstOrDefault(x => x.Name == item.Product.Name);
        item.Id = Guid.NewGuid().ToString();
        item.ProductId = rowProd.Id;
        item.MySoId = mySO.Id;
        item.Product = null;
        db.SODs.Add(item);
      }

      db.SaveChanges();




      return RedirectToAction("Index");

    }
  }
}
