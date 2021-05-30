using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST_DEV.Models;
using TEST_DEV.Requests;

namespace TEST_DEV.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginRequest form)
        {
            ViewBag.Title = "Index";
            return View();
        }
    }
}