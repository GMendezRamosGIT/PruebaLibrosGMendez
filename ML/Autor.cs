using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Autor
    {
        [Required(ErrorMessage = "El campo 'Autor' es obligatorio")]
        public int IdAutor { get; set; }
        public string Nombre { get; set; }
        public List<object> Autores { get; set; }
    }
}
