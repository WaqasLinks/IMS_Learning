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
      decimal maxId= db.MySOes.Max(x => x == null ? 0 : x.InvoiceNo);
      maxId = maxId + 1;

      //SOVM sovm = new SOVM();
      ViewBag.InvoiceNo1 = maxId;
      ViewBag.Customers = new SelectList(db.Customers, "Id", "Name");
      //ViewBag.Products = new SelectList(db.Products, "Id", "Name");
      ViewBag.Products = db.Products;

      return View();
    }
  }
}
