using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Editorial
    {
        [Required(ErrorMessage = "El campo 'Editorial' es obligatorio")]
        public int IdEditorial { get; set; }
        public string Nombre { get; set; }
        public List<object> Editoriales { get; set; }
    }
}
