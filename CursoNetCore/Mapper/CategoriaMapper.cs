using AutoMapper;
using CursoNetCore.DTO;
using CursoNetCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoNetCore.Mapper
{
    public class CategoriaMapper: Profile
    {
        public CategoriaMapper()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        }
    }
}
