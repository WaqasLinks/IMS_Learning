using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IMS.CustomClasses
{
  public class LoginFilter : ActionFilterAttribute
  {
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
      string requestingController = HttpContext.Current.Request.RequestContext.RouteData.Values["Controller"].ToString();
      string requestingAction = HttpContext.Current.Request.RequestContext.RouteData.Values["Action"].ToString();

      if (HttpContext.Current.Session["CurrentUser"] != null)
      {
        return;
      }
      if (HttpContext.Current.Session["CurrentUser"] == null && requestingController == "Authentication" && requestingAction == "Login")
      {
        //Let things happend automatically.
        //return;
        //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "UserManagement" }, { "action", "Login" } });
        return;
      }
      if (HttpContext.Current.Session["CurrentUser"] == null) /*|| requestingController == "Authentication" && requestingAction == "Login")*/
      {
        //Let things happend automatically.
        //return;
        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Authentication" }, { "action", "Login" } });
        return;
      }
    }
  }
}
