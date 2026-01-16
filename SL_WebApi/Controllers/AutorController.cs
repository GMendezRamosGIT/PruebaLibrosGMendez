using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    [RoutePrefix("api/autor")]
    public class AutorController : ApiController
    {
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Autor.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result.ErrorMessage);
            }
        }
    }
}
