using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoNetCore.DTO
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        //public string Nombre { get; set; }
        //public string Apellidos { get; set; }
        //public string Correo { get; set; }
        public string ClienteId { get; set; }
        //public Direcciones Direccion { get; set; }
        //public DateTime FechaCreacion { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] SaltPassword { get; set; }
    }
}