using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace SL_WebApi.Controllers
{
    [RoutePrefix("api/libro")]
    public class LibroController : ApiController
    {
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll([FromUri]ML.Libro libroBusqueda)
        {
            libroBusqueda = libroBusqueda ?? new ML.Libro();
            ML.Result result = BL.Libro.GetAll(libroBusqueda);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result.ErrorMessage);
            }
        }
        [HttpGet]
        [Route("GetById/{IdLibro}")]
        public IHttpActionResult GetById(int IdLibro)
        {
            ML.Result result = BL.Libro.GetById(IdLibro);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result.ErrorMessage);
            }
        }
        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody] ML.Libro libro)
        {
            ML.Result result = BL.Libro.Add(libro);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.ErrorMessage);
            }
        }
        [HttpDelete]
        [Route("DeleteLibroByIdAutor/{IdAutor}")]
        public IHttpActionResult DeleteLibroByIdAutor(int IdAutor)
        {
            ML.Result result = BL.Libro.DeleteLibroByAutor(IdAutor);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.ErrorMessage);
            }
        }
        [HttpDelete]
        [Route("DeleteLibroByIdEditorial/{IdEditorial}")]
        public IHttpActionResult DeleteLibroByIdEditorial(int IdEditorial)
        {
            ML.Result result = BL.Libro.DeleteLibroByEditorial(IdEditorial);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.ErrorMessage);
            }
        }
        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Update([FromBody] ML.Libro libro)
        {
            ML.Result result = BL.Libro.Update(libro);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result.ErrorMessage);
            }
        }
        [AcceptVerbs("OPTIONS")]
        [Route("{*any}")]
        public HttpResponseMessage Options()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");
            return response;
        }
    }
}
