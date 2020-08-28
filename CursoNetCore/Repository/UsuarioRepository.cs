using CursoNetCore.Infrestuctura;
using CursoNetCore.Model;
using CursoNetCore.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoNetCore.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CatalogoContext _catalogoContext;
        public UsuarioRepository(CatalogoContext catalogoContext)
        {
            _catalogoContext = catalogoContext;
        }

        public bool ExisteUsuario(string UsuarioCliente)
        {
            return _catalogoContext.Set<Usuario>().Any(u => u.Correo == UsuarioCliente);
        }

        public Usuario GetUsuario(int IdUsuario)
        {
            return _catalogoContext.Set<Usuario>().FirstOrDefault(u => u.IdUsuario == IdUsuario);
        }

        public ICollection<Usuario> GetUsuarios()
        {
            return _catalogoContext.Set<Usuario>().ToList();
        }

        public Usuario Login(string usuario, string password)
        {
            var usuarioCredencial = _catalogoContext.Set<Usuario>().FirstOrDefault(u => u.Correo == usuario);
            if (usuario == null)
            {
                return null;
            }
        }

        public int Registrar(Usuario usuario, string password)
        {
            throw new NotImplementedException();
        }
    }
}
