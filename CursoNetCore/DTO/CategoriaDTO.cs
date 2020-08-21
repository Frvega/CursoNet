using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoNetCore.DTO
{
    public class CategoriaDTO
    {
        public Guid IdCategoria { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreo { get; set; }
    }
}
