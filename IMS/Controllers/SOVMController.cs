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
      //SOVM sovm = new SOVM();
      ViewBag.Customers = new SelectList(db.Customers, "Id", "Name");
      ViewBag.Products = new SelectList(db.Products, "Id", "Name");
      
      return View();
    }
  }
}
