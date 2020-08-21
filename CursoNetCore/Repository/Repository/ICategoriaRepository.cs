using CursoNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoNetCore.Repository.Repository
{
    public interface ICategoriaRepository
    {
        Guid CreateCategoria(Categoria categoria);
        ICollection<Guid> CreateCategoria(ICollection<Categoria> categorias);
        ICollection<Categoria> GetCategoria();
        Categoria GetCategoria(Guid Id);
        bool DeleteCategoria(Guid Id);
        bool ExisteCategoria(string Nombre);
        bool ExisteCategoria(Guid Nombre);
        Categoria UpdateCategoria(Categoria categoria);
        ICollection<Categoria> UpdateCategoria(ICollection<Categoria> categorias);
    }
}
