using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TEST_DEV.Handlers;
using TEST_DEV.Helpers;
using TEST_DEV.Helpers.Tabla;
using TEST_DEV.Models;
using TEST_DEV.Requests;
using TEST_DEV.Responses;

namespace TEST_DEV.Controllers
{
    [SesionIniciada]
    public class ReportesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Reportes";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ObtenerReportes()
        {
            try
            {
                string token = await TokenRequest.ObtenerToken();
                List<Viaje> viajes = await ViajeRequest.ObtenerViajes(token);
                Tabla tabla = new Tabla();
                tabla.Columnas = new List<Columna>()
                {
                    new Columna() { Posicion = 1, Nombre = "Viaje" },
                    new Columna() { Posicion = 2, Nombre = "Cliente" },
                    new Columna() { Posicion = 3, Nombre = "Empleado" },
                    new Columna() { Posicion = 4, Nombre = "Razón social" },
                    new Columna() { Posicion = 5, Nombre = "RFC" },
                    new Columna() { Posicion = 6, Nombre = "Nombre" },
                    new Columna() { Posicion = 7, Nombre = "Apellido paterno" },
                    new Columna() { Posicion = 8, Nombre = "Apellido materno" },
                    new Columna() { Posicion = 9, Nombre = "Sucursal" },
                    new Columna() { Posicion = 10, Nombre = "Fecha registro" }
                };

                List<Fila> filas = new List<Fila>();
                foreach (Viaje viaje in viajes)
                {
                    Fila fila = new Fila();
                    fila.Propiedades = new
                    {
                        id = viaje.IdViaje,
                        empleado = viaje.IdEmpleado,
                        cliente = viaje.IdCliente
                    };
                    fila.Celdas = new List<Celda>()
                    {
                        new Celda() { Posicion = 1, Valor = viaje.IdViaje.ToString() },
                        new Celda() { Posicion = 2, Valor = viaje.IdCliente.ToString() },
                        new Celda() { Posicion = 3, Valor = viaje.IdEmpleado.ToString() },
                        new Celda() { Posicion = 4, Valor = viaje.RazonSocial },
                        new Celda() { Posicion = 5, Valor = viaje.RFC },
                        new Celda() { Posicion = 6, Valor = viaje.Nombre },
                        new Celda() { Posicion = 7, Valor = viaje.Paterno },
                        new Celda() { Posicion = 8, Valor = viaje.Materno },
                        new Celda() { Posicion = 9, Valor = viaje.Sucursal },
                        new Celda() { Posicion = 10, Valor = viaje.FechaRegistroEmpresa.ToDateFormat() }
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