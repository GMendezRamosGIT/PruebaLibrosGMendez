using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class LibroController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Libro libro = new ML.Libro();
            var result = ApiGetAll();
            if (result.Correct)
            {
                libro.Libros = result.Objects;
            }
            else
            {
                libro.Libros = new List<object>();

            }
            return View(libro);
        }
        [HttpGet]
        public ActionResult Form(int? IdLibro)
        {
            ML.Libro libro = new ML.Libro();
            libro.Autor = new ML.Autor();
            libro.Editorial = new ML.Editorial();
            if (IdLibro == null)
            {

            }
            else
            {

            }
            var resultAutor = ApiAutorGetAll();
            if (resultAutor.Correct)
            {
                libro.Autor.Autores = resultAutor.Objects;
            }
            else
            {
                libro.Autor.Autores = new List<object>();
            }
            var resultEditorial = ApiEditorialGetAll();
            if (resultEditorial.Correct)
            {
                libro.Editorial.Editoriales = resultEditorial.Objects;
            }
            else
            {
                libro.Editorial.Editoriales = new List<object>();
            }
            return View(libro);
        }
        [HttpPost]
        public ActionResult Form(ML.Libro libro)
        {
            if (ModelState.IsValid)
            {                
                if (libro.IdLibro > 0)
                {
                    //Actualizar
                }
                else
                {
                    var result = ApiAdd(libro);
                    if (result.Correct)
                    {
                        ViewBag.Message = "El libro se ha registrado correctamente.";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrió un error al registrar el libro." + result.ErrorMessage;
                    }
                }
                return PartialView("_Modal");
            }
            else
            {
                var resultAutor = ApiAutorGetAll();
                if (resultAutor.Correct)
                {
                    libro.Autor.Autores = resultAutor.Objects;
                }
                else
                {
                    libro.Autor.Autores = new List<object>();
                }
                var resultEditorial = ApiEditorialGetAll();
                if (resultEditorial.Correct)
                {
                    libro.Editorial.Editoriales = resultEditorial.Objects;
                }
                else
                {
                    libro.Editorial.Editoriales = new List<object>();
                }
                ViewBag.Message = "Corrige los errores del formulario";
                return View(libro);
            }
        }
        [NonAction]
        private ML.Result ApiGetAll(ML.Libro libroBusqueda = null)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var client = new System.Net.Http.HttpClient())
                {
                    string endpoint = ConfigurationManager.AppSettings["ApiLibroGetAll"];
                    client.BaseAddress = new Uri(endpoint);

                    libroBusqueda = libroBusqueda ?? new ML.Libro
                    {
                        Titulo = "",
                        AñoPublicacion = 0,
                        Autor = new ML.Autor
                        {
                            IdAutor = 0
                        },
                        Editorial = new ML.Editorial
                        {
                            IdEditorial = 0
                        }
                    };

                    string queryString = $"?Titulo={libroBusqueda.Titulo}&AñoPublicacion={libroBusqueda.AñoPublicacion}&IdAutor={libroBusqueda.Autor.IdAutor}&IdEditorial={libroBusqueda.Editorial.IdEditorial}";

                    var responseTask = client.GetAsync(queryString);
                    responseTask.Wait();
                    var resultApi = responseTask.Result;
                    if (resultApi.IsSuccessStatusCode)
                    {
                        var readTask = resultApi.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result.Objects = new List<object>();
                        foreach (var libroItem in readTask.Result.Objects)
                        {
                            ML.Libro libroLista = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Libro>(libroItem.ToString());
                            result.Objects.Add(libroLista);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        [NonAction]
        private ML.Result ApiAdd(ML.Libro libro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var client = new HttpClient())
                {
                    string endpoint = ConfigurationManager.AppSettings["ApiLibroAdd"];
                    client.BaseAddress = new Uri(endpoint);
                    var posTask = client.PostAsJsonAsync<ML.Libro>("", libro);
                    posTask.Wait();

                    var resultApi = posTask.Result;
                    if (resultApi.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        [NonAction]
        private ML.Result ApiAutorGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var client = new HttpClient())
                {
                    string endpoint = ConfigurationManager.AppSettings["ApiAutorGetAll"];
                    client.BaseAddress = new Uri(endpoint);
                    var responseTask = client.GetAsync("");
                    responseTask.Wait();
                    var resultApi = responseTask.Result;
                    if (resultApi.IsSuccessStatusCode)
                    {
                        var readTask = resultApi.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result.Objects = new List<object>();

                        foreach (var autorItem in readTask.Result.Objects)
                        {
                            ML.Autor autorLista = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Autor>(autorItem.ToString());
                            result.Objects.Add(autorLista);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros de autores.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        [NonAction]
        private ML.Result ApiEditorialGetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var client = new HttpClient())
                {
                    string endpoint = ConfigurationManager.AppSettings["ApiEditorialGetAll"];
                    client.BaseAddress = new Uri(endpoint);
                    var responseTask = client.GetAsync("");
                    responseTask.Wait();
                    var resultApi = responseTask.Result;
                    if (resultApi.IsSuccessStatusCode)
                    {
                        var readTask = resultApi.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();
                        result.Objects = new List<object>();
                        foreach (var editorialItem in readTask.Result.Objects)
                        {
                            ML.Editorial editorialLista = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Editorial>(editorialItem.ToString());
                            result.Objects.Add(editorialLista);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros de editoriales.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}