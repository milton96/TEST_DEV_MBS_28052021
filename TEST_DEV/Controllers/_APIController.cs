using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Routing;
using TEST_DEV.Helpers;
using TEST_DEV.Models;
using TEST_DEV.Requests;

namespace TEST_DEV.Controllers
{
    [Authorize]
    [RoutePrefix("api/usuario")]
    public class _APIController : ApiController
    {
        [HttpPost]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok("ping");
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Crear(PersonaRequest req)
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
                    return Content(HttpStatusCode.BadRequest, errors);

                int userId = User.Identity.GetId();

                PersonaFisica persona = new PersonaFisica();
                persona.Nombre = req.Nombre;
                persona.ApellidoPaterno = req.ApellidoPaterno;
                persona.ApellidoMaterno = req.ApellidoMaterno;
                persona.RFC = req.RFC;
                persona.FechaNacimiento = fechaNacimiento;
                persona.UsuarioAgrega = userId;

                PersonaFisica res = null;
                await Task.Run(() => { res = persona.Guardar(); });

                return Ok(new { response = res, code = HttpStatusCode.OK });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Obtener()
        {
            System.Diagnostics.Debug.WriteLine("login");
            return Ok(new { token = "token" });
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Obtener(int id)
        {
            System.Diagnostics.Debug.WriteLine("login");
            return Ok(new { token = "token" });
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Actualizar(int id)
        {
            System.Diagnostics.Debug.WriteLine("login");
            return Ok(new { token = "token" });
        }

        [HttpDelete]
        [Route("")]
        public async Task<IHttpActionResult> Desactivar(int id)
        {
            System.Diagnostics.Debug.WriteLine("login");
            return Ok(new { token = "token" });
        }
    }
}
