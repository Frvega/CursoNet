using CursoNetCore.Infrestuctura;
using CursoNetCore.Model;
using CursoNetCore.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoNetCore.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly CatalogoContext _catalogoContext;

        public CategoriaRepository(CatalogoContext catalogoContext)
        {
            _catalogoContext = catalogoContext;

        }

        public Guid CreateCategoria(Categoria categoria)
        {
            _catalogoContext.Categoria.Add(categoria);
            _catalogoContext.SaveChanges();

            return categoria.IdCategoria;
        }

        public ICollection<Guid> CreateCategoria(ICollection<Categoria> categorias) 
        {
            _catalogoContext.Categoria.AddRange(categorias);
            _catalogoContext.SaveChanges();

            return categorias.Select(x => x.IdCategoria).ToList();
        }

        public Categoria GetCategoria(Guid Id)
        {
            return _catalogoContext.Categoria.Where(cat => cat.IdCategoria == Id).FirstOrDefault();
        }

        public bool DeleteCategoria(Guid Id)
        {
            var categoria = _catalogoContext.Categoria.Where(cat => cat.IdCategoria == Id).FirstOrDefault();

            _catalogoContext.Categoria.Remove(categoria);
            _catalogoContext.SaveChanges();

            return true;
        }

        public Categoria UpdateCategoria(Categoria categoria)
        {
            var categoriaToUpdate = _catalogoContext.Categoria.Where(cat => cat.IdCategoria == categoria.IdCategoria).FirstOrDefault();
            categoriaToUpdate.Nombre = categoria.Nombre;
            _catalogoContext.SaveChanges();

            return categoriaToUpdate;
        }

        public ICollection<Categoria> UpdateCategoria(ICollection<Categoria> categorias)
        {
            var ListItem = _catalogoContext.Categoria.Where(x => categorias.Select(i => i.IdCategoria).ToList().Contains(x.IdCategoria)).ToList();

            ListItem.ForEach(cat =>
            {
                cat.Nombre = categorias.Where(x => x.IdCategoria == cat.IdCategoria).Select(n => n.Nombre).FirstOrDefault();
            }
            );
            _catalogoContext.SaveChanges();

            return categorias;
        }

        public ICollection<Categoria> GetCategoria()
        {
            return _catalogoContext.Set<Categoria>().Select(c => c).ToList();
        }

        ICollection<Categoria> ICategoriaRepository.GetCategoria()
        {
            throw new NotImplementedException();
        }

        public bool ExisteCategori(Guid Id)
        {
            throw new NotImplementedException();
        }

        public bool ExisteCategoria(string Nombre)
        {
            throw new NotImplementedException();
        }

        public bool ExisteCategoria(Guid Nombre)
        {
            throw new NotImplementedException();
        }
    }
}
