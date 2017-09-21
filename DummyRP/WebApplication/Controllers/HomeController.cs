using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TestTimeout;
using Microsoft.Rest;
using Microsoft.Rest.Azure;
using System.Net;

namespace WebApplication1.Controllers
{
    public class HomeController : ApiController
    {
        private Uri BackendServiceRootUri = new Uri(@"http://dummybackendservice.cloudapp.net/");

        [Route("~/ok")]
        [HttpGet]
        public IHttpActionResult GetOK()
        {
            using (var client = CreateClient())
            {
                return Ok(client.Service.OK());
            }
        }

        [Route("~/badrequest")]
        [HttpGet]
        public IHttpActionResult GetBadRequest()
        {
            using (var client = CreateClient())
            {
                try
                {
                    client.Service.BadRequest();
                }
                catch (CloudException ex)
                {
                    if (ex.Response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        return BadRequest();   
                    }
                }

                return Ok();
            }
        }

        [Route("~/internalerror")]
        [HttpGet]
        public IHttpActionResult GetInternalError()
        {
            using (var client = CreateClient())
            {
                try
                {
                    client.Service.InternalError();
                }
                catch (CloudException ex)
                {
                    if (ex.Response.StatusCode == HttpStatusCode.InternalServerError)
                    {
                        return InternalServerError();
                    }
                }

                return Ok();
            }
        }

        private DummyBackendServiceClient CreateClient()
        {
            return new DummyBackendServiceClient(BackendServiceRootUri, AnonymousCredential.GetInstance());
        }
    }

    public sealed class AnonymousCredential : ServiceClientCredentials 
    { 
        private static AnonymousCredential _instance; 
        public static AnonymousCredential GetInstance()
        { 
            if (_instance == null) 
            { 
                _instance = new AnonymousCredential(); 
            } 
            return _instance; 
        } 
        private AnonymousCredential() { } 
    } 
}