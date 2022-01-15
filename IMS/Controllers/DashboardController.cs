using IMSRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
  public class DashboardController : Controller
  {
    IMSEntities db = new IMSEntities();
    // GET: Dashboard
    public ActionResult Index()
    {
      Summary summary = new Summary();
      summary.TotalCustomers = db.Customers.Count();
      summary.TotalEmployees = db.Employees.Count();
      summary.TotalProducts = db.Products.Count();
      summary.TotalSO = db.SODs.Count();
      summary.TotalSuppliers = db.Suppliers.Count();


      return View(summary);
    }
  }
}
