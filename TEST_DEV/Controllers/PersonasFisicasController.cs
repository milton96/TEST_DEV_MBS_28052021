using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TEST_DEV.Handlers;
using TEST_DEV.Helpers;
using TEST_DEV.Helpers.Tabla;
using TEST_DEV.Models;
using TEST_DEV.Requests;

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

        [HttpPost]
        public JsonResult ObtenerPersona(int id)
        {
            try
            {
                PersonaFisica persona = PersonaFisica.ObtenerPorId(id);
                return Json(new { persona }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPut]
        public JsonResult Actualizar(UpdatePersonaRequest req)
        {
            try
            {
                if (req == null) throw new Exception("No se recibieron datos.");

                List<string> errors = new List<string>();
                DateTime fechaNacimiento = DateTime.Now;
                if (!ModelState.IsValid)
                {
                    foreach (KeyValuePair<string, ModelState> model in ModelState)
                    {
                        foreach (ModelError error in model.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                }

                if (req.Id <= 0)
                    errors.Add("El identificador es requerido");

                if (!String.IsNullOrEmpty(req.FechaNacimiento) && !DateTime.TryParse(req.FechaNacimiento, out fechaNacimiento))
                    errors.Add("La fecha de nacimiento no es válida");
                if (errors.Any())
                    return Json(new { msj = errors.ToHtmlList(), code = HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);

                PersonaFisica persona = PersonaFisica.ObtenerPorId(req.Id);
                persona.Nombre = String.IsNullOrEmpty(req.Nombre) ? persona.Nombre : req.Nombre;
                persona.ApellidoPaterno = String.IsNullOrEmpty(req.ApellidoPaterno) ? persona.ApellidoPaterno : req.ApellidoPaterno;
                persona.ApellidoMaterno = String.IsNullOrEmpty(req.ApellidoMaterno) ? persona.ApellidoMaterno : req.ApellidoMaterno;
                persona.RFC = String.IsNullOrEmpty(req.RFC) ? persona.RFC : req.RFC;
                persona.FechaNacimiento = String.IsNullOrEmpty(req.FechaNacimiento) ? persona.FechaNacimiento : fechaNacimiento;

                PersonaFisica res = persona.Actualizar();

                return Json(new { msj = "Registro actualizado", code = HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msj = ex.Message, code = HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Crear(PersonaRequest req)
        {
            try
            {
                if (req == null) throw new Exception("No se recibieron datos.");

                List<string> errors = new List<string>();
                DateTime fechaNacimiento = DateTime.Now;
                if (!ModelState.IsValid)
                {
                    foreach (KeyValuePair<string, ModelState> model in ModelState)
                    {
                        foreach (ModelError error in model.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                }
                if (!DateTime.TryParse(req.FechaNacimiento, out fechaNacimiento))
                    errors.Add("La fecha de nacimiento no es válida");
                if (errors.Any())
                    return Json(new { msj = errors.ToHtmlList(), code = HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);

                int userId = ((Usuario)HttpContext.Session[Usuario.USUARIO]).ID;

                PersonaFisica persona = new PersonaFisica();
                persona.Nombre = req.Nombre;
                persona.ApellidoPaterno = req.ApellidoPaterno;
                persona.ApellidoMaterno = req.ApellidoMaterno;
                persona.RFC = req.RFC;
                persona.FechaNacimiento = fechaNacimiento;
                persona.UsuarioAgrega = userId;

                PersonaFisica res = persona.Guardar();

                return Json(new { msj = "Registro creado", code = HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msj = ex.Message, code = HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpDelete]
        public JsonResult Desactivar(int id)
        {
            try
            {
                if (id <= 0) throw new Exception("Identificador no válido.");
                PersonaFisica.Desactivar(id);

                return Json(new { msj = "Registro desactivado", code = HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { msj = ex.Message, code = HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}