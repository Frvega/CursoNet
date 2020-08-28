using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursoNetCore.Model
{
    public class Marca
    {
        [Key]
        public Guid IdMarca { get; set; }
        public string Nombre { get; set; }
    }
}
