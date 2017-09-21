using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class HomeController : ApiController
    {
        [Route("~/ok")]
        [HttpGet]
        public IHttpActionResult GetOK()
        {
            return Ok(200);
        }

        [Route("~/badrequest")]
        [HttpGet]
        public IHttpActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [Route("~/internalerror")]
        [HttpGet]
        public IHttpActionResult GetInternalError()
        {
            return InternalServerError();    
        }
    }
}