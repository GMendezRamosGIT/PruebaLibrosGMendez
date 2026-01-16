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
        public static ML.Result GetById(int IdLibro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaLibrosEntities context = new DL.PruebaLibrosEntities())
                {
                    var registro = context.LibroGetById(IdLibro).SingleOrDefault();

                    if (registro != null)
                    {
                        ML.Libro libro = new ML.Libro();
                        libro.IdLibro = registro.IdLibro;
                        libro.Titulo = registro.Titulo;
                        libro.Autor = new ML.Autor();
                        libro.Autor.Nombre = registro.Autor;
                        libro.AñoPublicacion = registro.AñoPublicacion;
                        libro.Editorial = new ML.Editorial();
                        libro.Editorial.Nombre = registro.Editorial;

                        result.Object = libro;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el registro.";
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
        public static ML.Result Add(ML.Libro libro)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaLibrosEntities context = new DL.PruebaLibrosEntities())
                {
                    var filasAfectadas = context.LibroAdd(libro.Titulo, libro.Autor.IdAutor, libro.AñoPublicacion, libro.Editorial.IdEditorial);

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agrega el libro.";
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
        public static ML.Result DeleteLibroByAutor(int IdAutor)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaLibrosEntities context = new DL.PruebaLibrosEntities())
                {
                    var filasAfectadas = context.LibroDeleteByAutor(IdAutor);

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar los libros del autor.";
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
        public static ML.Result DeleteLibroByEditorial(int IdEditorial)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaLibrosEntities context = new DL.PruebaLibrosEntities())
                {
                    var filasAfectadas = context.LibroDeleteByEditorial(IdEditorial);

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar los libros del autor.";
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
