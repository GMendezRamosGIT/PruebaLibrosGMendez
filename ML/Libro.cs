using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Libro
    {
        public int IdLibro { get; set; }
        [Required(ErrorMessage = "El campo 'Titulo' es obligatorio")]
        [RegularExpression(@"^.{2,150}$", ErrorMessage = "Solo se permiten letras, números y símbolos comunes")]
        public string Titulo { get; set; }
        public ML.Autor Autor { get; set; }
        [Required(ErrorMessage = "El campo 'Año de Publicación' es obligatorio")]
        [Range(1450, 2100, ErrorMessage = "El año de publicación debe estar entre 1450 y el año actual")]
        public int? AñoPublicacion { get; set; }
        public ML.Editorial Editorial { get; set; }
        public List<object> Libros { get; set; }
    }
}
