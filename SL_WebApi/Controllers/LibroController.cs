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
        public IHttpActionResult GetAll([FromBody]ML.Libro libroBusqueda)
        {
            libroBusqueda = libroBusqueda ?? new ML.Libro();
            ML.Result result = BL.Libro.GetAll(libroBusqueda);
            if (result.Correct)
            {
                return Ok(result.Objects);
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
                return Ok(result.Object);
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
    }
}
