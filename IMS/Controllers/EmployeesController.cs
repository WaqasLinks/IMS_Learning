
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMS.CustomClasses;
using IMSRepo.Models;
namespace IMS.Controllers
{
  [LoginFilter]
  public class EmployeesController : Controller
  {
    IMSEntities db = new IMSEntities();
    // GET: Employees
    public ActionResult Index()
    {
      List<Employee> employees= db.Employees.ToList<Employee>();
      return View(employees);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Employee employee)
    {
      employee.Id = Guid.NewGuid().ToString();
      db.Employees.Add(employee);
      db.SaveChanges();
      return RedirectToAction("Index","Employees");
    }


    public ActionResult Edit(string id)
    {
      Employee employee= db.Employees.FirstOrDefault(x => x.Id == id);
      return View(employee);
    }

    [HttpPost]
    public ActionResult Edit(Employee employee)
    {

      db.Entry(employee).State =EntityState.Modified;
      db.SaveChanges();

      //Employee employee = db.Employees.FirstOrDefault(x => x.Id == id);
      return RedirectToAction("Index", "Employees");
    }

    public ActionResult Delete(string id)
    {
      Employee employee= db.Employees.Find(id);
      return  View(employee);

    }
    [HttpPost,ActionName("Delete")]
    public ActionResult DeleteConfirmed(string id)
    {
      Employee employee= db.Employees.Find(id);
      db.Employees.Remove(employee);
      db.SaveChanges();
      return RedirectToAction("Index");
    }


  }
}
