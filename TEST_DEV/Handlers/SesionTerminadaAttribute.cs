using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST_DEV.Models;

namespace TEST_DEV.Handlers
{
    public class SesionTerminadaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Usuario u = (Usuario)filterContext.HttpContext.Session[Usuario.USUARIO];
            if (u != null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        { "Controller", "PersonasFisicas" },
                        { "Action", "Index" }
                    }
                );
            }
            base.OnActionExecuting(filterContext);
        }
    }
}