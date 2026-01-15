using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Libro
    {
        public static ML.Result GetAll(ML.Libro libroBusqueda)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaLibrosEntities context = new DL.PruebaLibrosEntities())
                {
                    var registros = context.LibroGetAll(libroBusqueda.Autor.IdAutor, libroBusqueda.Editorial.IdEditorial, libroBusqueda.Titulo, libroBusqueda.AñoPublicacion).ToList();

                    if (registros.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var registro in registros)
                        {
                            ML.Libro libro = new ML.Libro();
                            libro.IdLibro = registro.IdLibro;
                            libro.Titulo = registro.Titulo;
                            libro.Autor = new ML.Autor();
                            libro.Autor.Nombre = registro.Autor;
                            libro.AñoPublicacion = registro.AñoPublicacion;
                            libro.Editorial = new ML.Editorial();
                            libro.Editorial.Nombre = registro.Editorial;

                            result.Objects.Add(libro);
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
                result.Ex = ex;
            }
            return result;
        }
    }
}
