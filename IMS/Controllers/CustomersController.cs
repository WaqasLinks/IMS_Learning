using IMSRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        private IMSEntities db = new IMSEntities();
        public ActionResult Index()
        {
            List<Customer> customers = db.Customers.ToList();

            return View(customers);
        }
        public ActionResult Update(string id)
        {
            Customer customer = db.Customers.Where(x => x.Id == id).FirstOrDefault();


                //List<Customer> customer = db.Customers.Where(x => x.Id == id).ToList();
                return View(customer);
            }
        }
    }
