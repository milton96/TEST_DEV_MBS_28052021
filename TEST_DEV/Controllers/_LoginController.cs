using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using TEST_DEV.Helpers;
using TEST_DEV.Models;
using TEST_DEV.Requests;

namespace TEST_DEV.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class _LoginController : ApiController
    {
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Login(LoginRequest login)
        {
            try
            {
                if (login == null) throw new Exception("No se recibieron datos.");
                //Validate(form);
                if (!ModelState.IsValid)
                {
                    List<string> errors = new List<string>();
                    foreach (KeyValuePair<string, ModelState> model in ModelState)
                    {
                        foreach (ModelError error in model.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                    return Content(HttpStatusCode.Forbidden, errors);
                }

                Usuario u = null;
                await Task.Run( () => { u = Usuario.Login(login); });
                if (u == null)
                {
                    return Unauthorized();
                }

                string token = await JWTHelper.GenerarToken(u);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPost]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok("ping");
        }
    }
}
