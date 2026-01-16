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
                libro.Libros = result.Objects.ToList();
            }
            else
            {
                libro.Libros = new List<object>();

            }
                return View(libro);
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
    }
}