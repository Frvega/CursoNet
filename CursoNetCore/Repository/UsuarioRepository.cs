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
            return _catalogoContext.Set<Usuario>().Any(u => u.ClientId == UsuarioCliente);
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
            var usuarioCredencial = _catalogoContext.Set<Usuario>().FirstOrDefault(u => u.ClientId == usuario);
            if (usuario == null)
            {
                return null;
            }
            if (!Criptography.ValidacionPassword(password, usuarioCredencial.HashPassword, usuarioCredencial.SaltPassword)){
                return null;
            }

            return usuarioCredencial;
        }

        public int Registrar(Usuario usuario, string password)
        {
            byte[] HashPassword, SaltPassword;
            Criptography.CrearPasswordEncriptado(password, out HashPassword, out SaltPassword);
            usuario.HashPassword = HashPassword;
            usuario.SaltPassword = SaltPassword;

            _catalogoContext.Set<Usuario>().Add(usuario);
            _catalogoContext.SaveChanges();

            return usuario.IdUsuario;

            throw new NotImplementedException();
        }
    }
}
