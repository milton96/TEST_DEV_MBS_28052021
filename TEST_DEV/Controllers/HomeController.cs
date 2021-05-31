using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TEST_DEV.Handlers;
using TEST_DEV.Models;
using TEST_DEV.Requests;

namespace TEST_DEV.Controllers
{    
    public class HomeController : Controller
    {
        [HttpGet]
        [SesionTerminada]
        public ActionResult Index()
        {
            ViewBag.Title = "Index";
            return View();
        }

        [HttpPost]
        [SesionTerminada]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginRequest form)
        {
            ViewBag.Title = "Index";
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            Usuario user = Usuario.Login(form);
            if (user == null)
            {
                ModelState.AddModelError("Error", "Correo o contraseña incorrectos");
                return View(form);
            }

            HttpContext.Session.Add(Usuario.USUARIO, user);

            return RedirectToAction("Index", "PersonasFisicas");
        }
                
        [SesionIniciada]
        public ActionResult CerrarSesion()
        {
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}