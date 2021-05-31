using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEST_DEV.Controllers
{
    public class ReportesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Reportes";
            return View();
        }

        [HttpPost]
        public JsonResult ObtenerReportes()
        {
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
            return null;
        }
    }
}