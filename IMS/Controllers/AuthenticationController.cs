using IMSRepo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IMS.Controllers
{
  public class AuthenticationController : Controller
  {
    IMSEntities db = new IMSEntities();
    // GET: Authentication
    public ActionResult Login()
    {
      return View();
    }
    [HttpPost]
    public ActionResult Login(User user)
    {
      User authenticatedUser = db.Users.FirstOrDefault(x => x.Login == user.Login && x.Password == user.Password);
      if (authenticatedUser!=null)
      {
        Session.Add("CurrentUser", user);
        return RedirectToAction("Index", "Dashboard");
      }
      else
      {
        TempData["message"] = "Password is not valid";
        return RedirectToAction("Login", "Authentication");
      }


    }

  }
}
