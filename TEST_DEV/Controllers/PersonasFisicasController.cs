using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEST_DEV.Controllers
{
    public class PersonasFisicasController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Personas físicas";
            return View();
        }
    }
}