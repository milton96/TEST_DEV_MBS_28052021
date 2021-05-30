using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEST_DEV.Handlers;
using TEST_DEV.Helpers;
using TEST_DEV.Helpers.Tabla;
using TEST_DEV.Models;

namespace TEST_DEV.Controllers
{
    [SesionIniciada]
    public class PersonasFisicasController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Personas físicas";
            return View();
        }

        [HttpPost]
        public JsonResult Personas()
        {
            try
            {
                List<PersonaFisica> personas = PersonaFisica.ObtenerRegistros();
                Tabla tabla = new Tabla();
                tabla.Columnas = new List<Columna>()
                {
                    new Columna() { Posicion = 1, Nombre = "Fecha de registro" },
                    new Columna() { Posicion = 2, Nombre = "Fecha de actualización" },
                    new Columna() { Posicion = 3, Nombre = "Nombre" },
                    new Columna() { Posicion = 4, Nombre = "Apellido paterno" },
                    new Columna() { Posicion = 5, Nombre = "Apellido materno" },
                    new Columna() { Posicion = 6, Nombre = "RFC" },
                    new Columna() { Posicion = 7, Nombre = "Fecha de nacimiento" },
                    new Columna() { Posicion = 8, Nombre = "Estatus" }
                };

                List<Fila> filas = new List<Fila>();
                foreach(PersonaFisica persona in personas)
                {
                    Fila fila = new Fila();
                    fila.Propiedades = new
                    {
                        id = persona.IdPersonaFisica
                    };
                    fila.Celdas = new List<Celda>()
                    {
                        new Celda() { Posicion = 1, Valor = persona.FechaRegistro.ToDateFormat() },
                        new Celda() { Posicion = 2, Valor = persona.FechaActualizacion.ToDateFormat() },
                        new Celda() { Posicion = 3, Valor = persona.Nombre },
                        new Celda() { Posicion = 4, Valor = persona.ApellidoPaterno },
                        new Celda() { Posicion = 5, Valor = persona.ApellidoMaterno },
                        new Celda() { Posicion = 6, Valor = persona.RFC },
                        new Celda() { Posicion = 7, Valor = persona.FechaNacimiento.ToDateFormat() },
                        new Celda() { Posicion = 8, Valor = persona.Activo ? "Activo" : "Inactivo" }
                    };
                    filas.Add(fila);
                }
                tabla.Filas = filas;
                return Json(new { tabla }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}