using CursoNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoNetCore.Repository.Repository
{
    public interface IUsuarioRepository
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int IdUsuario);
        bool ExisteUsuario(string UsuarioCliente);
        Usuario Login(string usuario, string password);
        int Registrar(Usuario usuario, string password);
    }
}
