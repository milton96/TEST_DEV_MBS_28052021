using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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
        public async Task<IHttpActionResult> Crear()
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
