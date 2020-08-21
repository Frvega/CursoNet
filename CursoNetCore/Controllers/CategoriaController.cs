using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CursoNetCore.DTO;
using CursoNetCore.Model;
using CursoNetCore.Repository.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CursoNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;
        public CategoriaController( ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCategoria()
        {
            var ListaCategoriaa = _categoriaRepository.GetCategoria();
            var LsCategoriaDTO = new List<CategoriaDTO>();
            foreach (var i  in ListaCategoriaa)
            {
                LsCategoriaDTO.Add(_mapper.Map<CategoriaDTO>(i));
            }
            return Ok(LsCategoriaDTO);
        }

        [HttpGet("{id:Guid}",Name ="GetCategoria")]
        public IActionResult GetCategoria(Guid id)
        {
            var categoria = _categoriaRepository.GetCategoria(id);
            if(categoria == null)
            {
                return NotFound();
            }
            var CategoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            return Ok(CategoriaDTO);
        }
        
        [HttpPost]
        public IActionResult CrearMarca([FromBody]CategoriaDTO categoriaDTO)
        {
            if(categoriaDTO == null)
            {
                return BadRequest(ModelState);
            }else if(_categoriaRepository.ExisteCategoria(categoriaDTO.Nombre))
            {
                ModelState.AddModelError("", "La Categoria" + categoriaDTO.Nombre + "ya existe");
                return StatusCode(404, ModelState);
            }
            var Categoria = _mapper.Map<Model.Categoria>(categoriaDTO);
            Guid IdCategoria = _categoriaRepository.CreateCategoria(Categoria);
            if (IdCategoria == null)
            {
                ModelState.AddModelError("", "La categoria no se pudo guardar");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }

        [HttpPatch("{IdCategoria:int}",Name ="UpdateCategoria")]
        public IActionResult UpdateCategoria(int IdCategoria,[FromBody]CategoriaDTO categoriaDTO)
        {
            if(categoriaDTO == null)
            {
                return BadRequest(ModelState);
            }

            var Categoria = _mapper.Map<Model.Categoria>(categoriaDTO);
            var categoriaUpdated = _categoriaRepository.UpdateCategoria(Categoria);
            if (categoriaUpdated == null)
            {
                ModelState.AddModelError("", "Lacategoria no se pudo actualizar ");
                return StatusCode(500, ModelState);
            }
            return Ok(Categoria);
        }
        [HttpDelete("{Id:int}",Name ="DeleteCategoria")]
        public IActionResult DeleteCategoria(Guid Id)
        {
            if (!_categoriaRepository.ExisteCategoria(Id))
            {
                return NotFound();
            }
            if (!_categoriaRepository.DeleteCategoria(Id))
            {
                ModelState.AddModelError("", "La Categoria no se pudo Eliminar");
                return StatusCode(500, ModelState); 
            }
            return NoContent();
        }
    }
}
