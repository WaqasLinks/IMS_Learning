using IMSRepo.Models;
using IMSRepo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
  public class SOVMController : Controller
  {
    private IMSEntities db = new IMSEntities();
    // GET: SOVM
    public ActionResult Create()
    {

      decimal maxId = 0;
      if (db.MySOes.Count()!=0) maxId = db.MySOes.Max(x => x == null ? 0 : x.InvoiceNo);
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
      mySO.Id= Guid.NewGuid().ToString();
      mySO.DateCreated = DateTime.Now;
      
      db.MySOes.Add(mySO);
      List<SOD> LstSODs = saleOrderVM.SODs;
      foreach (SOD item in LstSODs)
      {
        Product rowProd= db.Products.FirstOrDefault(x => x.Name == item.Product.Name);
        item.Id = Guid.NewGuid().ToString();
        item.ProductId = rowProd.Id;
        item.MySoId = mySO.Id;
        item.Product = null;
        db.SODs.Add(item);
      }
     
      db.SaveChanges();
      return RedirectToAction("Index","MySOes") ;
    }
  }
}
